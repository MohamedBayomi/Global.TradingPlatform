using Microsoft.Extensions.Configuration;

using System.Net.Http.Json;

namespace Global.TradingPlatform.DesktopApp
{
    internal class SubmissionProxy
    {
        private static string? _SubmissionUrl;
        static SubmissionProxy()
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

            _SubmissionUrl = configuration["ApiSettings:Submission"];
        }
        
        public static async Task<Order> SubmitAsync(OrderRequest order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_SubmissionUrl);

                // Use asynchronous methods for HTTP requests
                var response = await client.PostAsJsonAsync("orders/submit", order);
                Console.WriteLine($"Response Status: {response.StatusCode}");
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {responseContent}");

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Deserialize the response content into an Order object
                var result = await response.Content.ReadFromJsonAsync<Order>();

                return result;
            }
        }
    }
}
