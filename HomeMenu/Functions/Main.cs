using HomeMenu.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace HomeMenu.Functions
{
    internal class Main
    {
        public static async Task<List<Dish>> Find(string finder)
        {
            HomeMenuContext context = new();
            return await context.Dishes.Where(d => d.CategoryNavigation.Name.Contains(finder) || d.Name.Contains(finder) || d.AuthorNavigation.Name.Contains(finder)).ToListAsync();
        }
        public static bool ValidateDish(string name, string photo, string calories, string proteins, string fats, string carbohydrates, string portions, string hours, string minutes,
            string recipe, System.Collections.IEnumerable ingridients)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите название блюда");
                return false;
            }
            if (photo == null)
            {
                MessageBox.Show("Вставьте изображение блюда");
                return false;
            }
            if (!double.TryParse(calories, out _) ||
                !double.TryParse(proteins, out _) ||
                !double.TryParse(fats, out _) ||
                !double.TryParse(carbohydrates, out _) ||
                !int.TryParse(portions, out _) ||
                !int.TryParse(hours, out _) ||
                !int.TryParse(minutes, out _))
            {
                MessageBox.Show("Проверьте числовые значения (калории, БЖУ, порции, время)");
                return false;
            }
            if (recipe == "")
            {
                MessageBox.Show("Поделитесь рецептом Вашего блюда");
                return false;
            }
            if (ingridients == null)
            {
                MessageBox.Show("Укажите ингредиенты Вашего блюда с граммами");
                return false;
            }
            return true;
        }
        public static async Task<string> CreatePay(int amount, string orderId, string description, int userId, IConfiguration configuration)
        {
            var httpClient = new HttpClient();
            var paymentService = new PaymentService(configuration, httpClient);
            var payment = new Payment
            {
                Amount = amount,
                Id = orderId,
                Description = description,
                UserId = userId,
                SuccessUrl = "https://nekto-z.tech",
                FailUrl = "https://nekto-z.tech"
            };

            var paymentUrl = await paymentService.InitPayment(payment);
            return paymentUrl;
        }
        public class PaymentService
        {
            private readonly HttpClient _httpClient;
            private readonly string _terminalKey;
            private readonly string _secretKey;
            public PaymentService(IConfiguration configuration, HttpClient httpClient)
            {
                _terminalKey = configuration["PaymentSettings:TerminalKey"];
                _secretKey = configuration["PaymentSettings:SecretKey"];
                _httpClient = httpClient;
            }
            public async Task<string> InitPayment(Payment payment)
            {
                var jsonContent = JsonConvert.SerializeObject(payment);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://rest-api-test.tinkoff.ru/v2/init", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(responseContent);
                return paymentResponse.PaymentUrl;
            }
        }
        public class PaymentResponse
        {
            public string PaymentUrl { get; set; }
            public string Status { get; set; }
        }
    }
    public class Privacy
        {
            public class EmailSettings
            {
                public string Email { get; set; }
                public string EmailPassword { get; set; }
                public string TerminalKey { get; set; }
                public string SecretKey { get; set; }
            }
            public static class ConfigurationHelper
            {
                public static IConfigurationRoot Configuration { get; private set; }
                static ConfigurationHelper()
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    Configuration = builder.Build();
                }
                public static EmailSettings GetEmailSettings()
                {
                    var emailSettings = new EmailSettings();
                    Configuration.GetSection("EmailSettings").Bind(emailSettings);
                    return emailSettings;
                }
            }
        }
}
