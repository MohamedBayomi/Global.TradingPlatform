using Authentication_Service.Const;
using Authentication_Service.DTOs;
using Authentication_Service.Interfaces;
using Authentication_Service.Services;
using AutoMapper;
using EmployeesPortal.Models;
using EmployeesPortal.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Authentication_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IMailingService _mailingService;
        private readonly IMapper _mapper;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _webHostEnvironment; // Inject IWebHostEnvironment

        public AuthenticationController(IAuthService authService, UserManager<ApplicationUser> userManager, IEmployeeService employeeService,
            IMailingService mailingService, IMapper mapper, Microsoft.AspNetCore.Hosting.IHostingEnvironment webHostEnvironment)
        {
            _authService = authService;
            _userManager = userManager;
            _employeeService = employeeService;
            _mailingService = mailingService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!EmailValidatorService.IsValidEmailProvider(model.Email))
                return BadRequest("Invalid Email");

            model.UserName = model.Email;
            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
           

                var employee = new Employee
                {
                    ApplicationUserId = _userManager.FindByEmailAsync(model.Email).Result.Id,
                   DepartmentID=model.DepartmentId,
                   ManagerID=model.ManagerId,
                };
                try
                {
                    await _employeeService.AddAsync(employee);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            
            return Ok("Successfull Register.....Please Go To Login");
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("UserId and token must be supplied for email confirmation.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest($"Unable to load user with ID '{userId}'.");
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest("Email is already confirmed");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }

            return BadRequest("Error confirming email.");
        }
        [HttpPost("login")]// Login for teacher or parent or admin
        public async Task<IActionResult> LoginAsync(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.LoginAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (!user.EmailConfirmed)
                return BadRequest("Email not confirmed");
            var empid = (await _employeeService.GetEmployeeIdByAppId(user.Id)).EmployeeID;

            return Ok(new
            {
                token = result.Token,
                expiresOn = result.ExpiresOn,
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                EmployeeId =empid,
                Role = result.Roles.Contains(Roles.Manager.ToString()) ? Roles.Manager.ToString() : Roles.Employee.ToString()
            });
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("لم يتم العثور علي الإيميل");

            // Generate the reset code
            string resetCode = _mailingService.GenerateCode();

            // Set the reset code and expiration time
            user.ResetPasswordCode = resetCode;
            user.ResetCodeExpiry = DateTime.UtcNow.AddMinutes(30); // Expiry time of 30 minutes

            // Save the reset code and expiration time in the database
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return StatusCode(500, "An error occurred while updating the user record.");

            // Load the email template
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "ResetPassword.html");
            string emailBody = await System.IO.File.ReadAllTextAsync(filePath);

            // Customize email body with reset code if needed
            emailBody = emailBody.Replace("{ResetCode}", resetCode);

            // Send the reset code via email
            await _mailingService.SendEmailAsync(
                model.Email,
                "Code For Reset Password",
                emailBody // Using the modified template with the reset code
            );

            return Ok("تم إرسال رمز التأكيد الي ايميلك");
        }
        [HttpPost("check-reset-code")]
        public async Task<IActionResult> CheckResetCode(CheckResetCodeDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("لم يتم العثور علي الإيميل");

            // Check if the reset code matches and has not expired
            if (user.ResetPasswordCode != model.ResetCode || user.ResetCodeExpiry < DateTime.UtcNow)
                return BadRequest("The reset code is invalid or has expired.");

            // Code is valid
            return Ok(new
            {
                Message = "Reset code is valid."
            ,
                Token =(await _authService.CheakResetPassword(model)).Token
            });
        }
        [Authorize(Roles="Employee")]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("لم يتم العثور علي الإيميل");


            // Reset the password
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Clear the reset code after successful password reset
            user.ResetPasswordCode = null;
            user.ResetCodeExpiry = null;
            await _userManager.UpdateAsync(user);

            return Ok("تم تغير كلمة السر بنجاح.");
        }

    }
}
