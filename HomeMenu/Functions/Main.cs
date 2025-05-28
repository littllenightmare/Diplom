using HomeMenu.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System;

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
            try
            {
                HomeMenuContext context = new();
                return await context.Dishes.Where(d => d.CategoryNavigation.Name.Contains(finder) || d.Name.Contains(finder) || d.AuthorNavigation.Name.Contains(finder)).ToListAsync();
            }
            catch { return  new List<Dish>() { }; }
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
        public static bool ValidateDish(string name, string photo, ref string calories, ref string proteins, ref string fats, ref string carbohydrates, string portions, string hours, string minutes,
            string recipe, string ingridients)
        {
            try
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
                if (!int.TryParse(portions, out _) ||
                    !int.TryParse(hours, out _) ||
                    !int.TryParse(minutes, out _))
                {
                    MessageBox.Show("Проверьте числовые значения (порции, время)");
                    return false;
                }
                if (recipe == "Подготовка: ")
                {
                    MessageBox.Show("Поделитесь рецептом Вашего блюда");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ingridients))
                {
                    MessageBox.Show("Укажите ингредиенты Вашего блюда с граммами");
                    return false;
                }
                if (calories == "") calories = Calculations.CalculateCalories(ingridients).ToString();
                if (proteins == "") proteins = Calculations.CalculateProteins(ingridients).ToString();
                if (fats == "") fats = Calculations.CalculateFats(ingridients).ToString();
                if (carbohydrates == "") carbohydrates = Calculations.CalculateCarbohydrates(ingridients).ToString();
                if (!double.TryParse(calories, out _) || !double.TryParse(proteins, out _) ||
                    !double.TryParse(fats, out _) || !double.TryParse(carbohydrates, out _)) return false;
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// класс для расчёта калорий и БЖУ
        /// </summary>
        public class Calculations
        {
            /// <summary>
            /// расчёт калорий по списку ингредиентов
            /// </summary>
            /// <param name="ingridients">список ингредиентов</param>
            /// <returns>сумму калорий</returns>
            public static double CalculateCalories(string ingridients)
            {
                try
                {
                    var ingredientsList = JsonConvert.DeserializeObject<List<string>>(ingridients);
                    HomeMenuContext context = new();
                    double sum = 0;
                    foreach (var ingridient in ingredientsList)
                    {
                        var dash = ingridient.IndexOf('-');
                        var ingridientName = ingridient.Remove(dash - 1);
                        var ingridientAmmount = ingridient.Remove(0, dash + 1);
                        sum += context.Ingridients.FirstOrDefault(i => i.Name == ingridientName).Calories * Double.Parse(ingridientAmmount) / 100;
                    }
                    return Math.Round(sum, 1);
                }
                catch { return 0; }
            }
            /// <summary>
            /// расчёт белков по списку ингредиентов
            /// </summary>
            /// <param name="ingridients">список ингредиентов</param>
            /// <returns>сумму белков</returns>
            public static double CalculateProteins(string ingridients)
            {
                try
                {
                    var ingredientsList = JsonConvert.DeserializeObject<List<string>>(ingridients);
                    HomeMenuContext context = new();
                    double sum = 0;
                    foreach (var ingridient in ingredientsList)
                    {
                        var dash = ingridient.IndexOf('-');
                        var ingridientName = ingridient.Remove(dash - 1);
                        var ingridientAmmount = ingridient.Remove(0, dash + 1);
                        sum += context.Ingridients.FirstOrDefault(i => i.Name == ingridientName).Proteins * Double.Parse(ingridientAmmount) / 100;
                    }
                    return Math.Round(sum, 1);
                }
               catch { return 0; }
            }
            /// <summary>
            /// расчёт жиров по списку ингредиентов
            /// </summary>
            /// <param name="ingridients">список ингредиентов</param>
            /// <returns>сумму жиров</returns>
            public static double CalculateFats(string ingridients)
            {
                try
                {
                    var ingredientsList = JsonConvert.DeserializeObject<List<string>>(ingridients);
                    HomeMenuContext context = new();
                    double sum = 0;
                    foreach (var ingridient in ingredientsList)
                    {
                        var dash = ingridient.IndexOf('-');
                        var ingridientName = ingridient.Remove(dash - 1);
                        var ingridientAmmount = ingridient.Remove(0, dash + 1);
                        sum += context.Ingridients.FirstOrDefault(i => i.Name == ingridientName).Fats * Double.Parse(ingridientAmmount) / 100;
                    }
                    return Math.Round(sum, 1);
                }
                catch { return 0; }
            }
            /// <summary>
            /// расчёт углеводов по списку ингредиентов
            /// </summary>
            /// <param name="ingridients">список ингредиентов</param>
            /// <returns>сумму углеводов</returns>
            public static double CalculateCarbohydrates(string ingridients)
            {
                try
                {
                    var ingredientsList = JsonConvert.DeserializeObject<List<string>>(ingridients);
                    HomeMenuContext context = new();
                    double sum = 0;
                    foreach (var ingridient in ingredientsList)
                    {
                        var dash = ingridient.IndexOf('-');
                        var ingridientName = ingridient.Remove(dash - 1);
                        var ingridientAmmount = ingridient.Remove(0, dash + 1);
                        sum += context.Ingridients.FirstOrDefault(i => i.Name == ingridientName).Carbohydrates * Double.Parse(ingridientAmmount) / 100;
                    }
                    return Math.Round(sum, 1);
                }
                catch { return 0; }
            }
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
            try
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
            catch { return null; }
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
            /// <param name="configuration">конфигурация</param>
            /// <param name="httpClient">сайтик</param>
            public PaymentService(IConfiguration configuration, HttpClient httpClient)
            {
                _terminalKey = configuration["PaymentSettings:TerminalKey"];
                _secretKey = configuration["PaymentSettings:SecretKey"];
                _httpClient = httpClient;
            }
            /// <summary>
            /// Инициализация платежа на стороне ТБанка
            /// </summary>
            /// <param name="payment">платежная система</param>
            /// <returns>ответ ТБанка</returns>
            public async Task<string> InitPayment(Payment payment)
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(payment);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync("https://rest-api-test.tinkoff.ru/v2", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                catch { return null; }
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
            /// <returns>почтовые данные</returns>
            public static EmailSettings GetEmailSettings()
            {
                try
                {
                    var emailSettings = new EmailSettings();
                    Configuration.GetSection("EmailSettings").Bind(emailSettings);
                    return emailSettings;
                }
                catch { return null; }
            }
        }
    }
}
