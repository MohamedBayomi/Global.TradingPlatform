namespace Authentication_Service.Interfaces
{
    public interface IMailingService
    {
        Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null);
        public string GenerateCode();
    }
}
