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
        /// <summary>
        /// Поиск блюда
        /// </summary>
        /// <param name="finder">строка с символами поиска</param>
        /// <returns>список подходящих блюд</returns>
        public static async Task<List<Dish>> Find(string finder)
        {
            HomeMenuContext context = new();
            return await context.Dishes.Where(d => d.CategoryNavigation.Name.Contains(finder) || d.Name.Contains(finder) || d.AuthorNavigation.Name.Contains(finder)).ToListAsync();
        }
        /// <summary>
        /// Валидация блюда при его добавлении
        /// </summary>
        /// <param name="name">название</param>
        /// <param name="photo">фото</param>
        /// <param name="calories">калории</param>
        /// <param name="proteins">белки</param>
        /// <param name="fats">жиры</param>
        /// <param name="carbohydrates">углеводы</param>
        /// <param name="portions">порции</param>
        /// <param name="hours">часы</param>
        /// <param name="minutes">минуты</param>
        /// <param name="recipe">рецепт</param>
        /// <param name="ingridients">список ингредиентов</param>
        /// <returns>правильность заполнения формы</returns>
        public static bool ValidateDish(string name, string photo, string calories, string proteins, string fats, string carbohydrates, string portions, string hours, string minutes,
            string recipe, System.Collections.IEnumerable ingridients)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите название блюда");
                return false;
            }
            if (photo == "pack://application:,,,/Images/Icon.png")
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
            if (recipe == "Подготовка: ")
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
        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <param name="amount">сумма</param>
        /// <param name="orderId">номер заказа</param>
        /// <param name="description">описание</param>
        /// <param name="userId">айди пользователя</param>
        /// <param name="configuration">конфигурация платёжки</param>
        /// <returns>успешность создания платежа</returns>
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
        /// <summary>
        /// платёжный сервис
        /// </summary>
        public class PaymentService
        {
            private readonly HttpClient _httpClient;
            private readonly string _terminalKey;
            private readonly string _secretKey;
            /// <summary>
            /// Наполнение конфигурационными данными
            /// </summary>
            /// <param name="configuration"></param>
            /// <param name="httpClient"></param>
            public PaymentService(IConfiguration configuration, HttpClient httpClient)
            {
                _terminalKey = configuration["PaymentSettings:TerminalKey"];
                _secretKey = configuration["PaymentSettings:SecretKey"];
                _httpClient = httpClient;
            }
            /// <summary>
            /// Инициализация платежа на стороне ТБанка
            /// </summary>
            /// <param name="payment"></param>
            /// <returns></returns>
            public async Task<string> InitPayment(Payment payment)
            {
                var jsonContent = JsonConvert.SerializeObject(payment);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://rest-api-test.tinkoff.ru/v2", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
    }
    /// <summary>
    /// Класс для безопасной отправки сообщения на почту при помощи секретных данных
    /// </summary>
    public class Privacy
    {
        public class EmailSettings
        {
            public string Email { get; set; }
            public string EmailPassword { get; set; }
        }
        /// <summary>
        /// класс конфигурации
        /// </summary>
        public static class ConfigurationHelper
        {
            public static IConfigurationRoot Configuration { get; private set; }
            /// <summary>
            /// Возврат секретных данных
            /// </summary>
            /// <returns></returns>
            public static EmailSettings GetEmailSettings()
            {
                var emailSettings = new EmailSettings();
                Configuration.GetSection("EmailSettings").Bind(emailSettings);
                return emailSettings;
            }
        }
    }
}
