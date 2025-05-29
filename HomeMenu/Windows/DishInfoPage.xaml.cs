using HomeMenu.Database;
using HomeMenu.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HomeMenu.Windows
{
    /// <summary>
    /// Логика взаимодействия для DishInfoPage.xaml
    /// </summary>
    public partial class DishInfoPage : Window
    {
        HomeMenuContext context = new();
        public DishInfoPage()
        {
            InitializeComponent();
            LoadDishToForm();
        }
        /// <summary>
        /// Выгрузка данных о блюде на форму
        /// </summary>
        void LoadDishToForm()
        {
            var dish = Data.dish;
            PhotoImage.Source = new BitmapImage(new Uri(dish.Photo, UriKind.RelativeOrAbsolute));
            Nametb.Text = dish.Name;

            string time = "";
            var hours = dish.Time / 60;
            if (hours > 0) time += $"{hours} ч. ";
            var minutes = dish.Time % 60;
            TimeRun.Text = time + $"{minutes} мин.";

            DifficultRun.Text = dish.Difficult.ToString();
            PortionRun.Text = dish.Portion.ToString();
            Categorytb.Text = context.Categories.FirstOrDefault(c => c.Id == dish.Category).Name;
            CaloriesRun.Text = dish.Calories.ToString();
            ProteinsRun.Text = dish.Proteins.ToString();
            FatsRun.Text = dish.Fats.ToString();
            CarbohydratesRun.Text = dish.Carbohydrates.ToString();

            if (dish.Author != null)
            {
                AuthorRun.Text = context.Profiles.FirstOrDefault(c => c.Id == dish.Author).Name;
                AuthorImage.Source = new BitmapImage(new Uri(context.Profiles.FirstOrDefault(c => c.Id == dish.Author).Photo, UriKind.RelativeOrAbsolute));
            }

            Recipetb.Text = dish.Recipe;

            try
            {
                var ingredientsList = JsonConvert.DeserializeObject<List<string>>(dish.Ingridients);
                Ingridientstb.Text = string.Join(Environment.NewLine, ingredientsList);
            }
            catch
            {
                var ingredientsDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(dish.Ingridients);
                Ingridientstb.Text = string.Join(Environment.NewLine, ingredientsDict.Select(kvp => $"{kvp.Key} - {kvp.Value}"));
            }
        }
        /// <summary>
        /// Возврат на главную
        /// </summary>
        /// <param name="sender">кнопка Назад</param>
        /// <param name="e">событие нажатия на кнопку</param>
        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();
            this.Close();
        }
        /// <summary>
        /// Выход на профиль автора рецепта
        /// </summary>
        /// <param name="sender">аватарка автора</param>
        /// <param name="e">событие нажатия на аватарку</param>
        private void GoToProfile(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (context.Profiles.FirstOrDefault(p => p.Name == AuthorRun.Text) != null)
            {
                Data.profile = context.Profiles.FirstOrDefault(p => p.Name == AuthorRun.Text);
                ProfilePage profilePage = new();
                profilePage.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Вывод аллергенов на форму
        /// </summary>
        /// <param name="sender">кнопка Аллергены</param>
        /// <param name="e">нажатие на кнопку</param>
        private void AllergenClick(object sender, RoutedEventArgs e)
        {
            var dish = Data.dish;
            List<string> allergenList = new();
            try
            {
                var ingredientsList = JsonConvert.DeserializeObject<List<string>>(dish.Ingridients);
                foreach (var ingridient in ingredientsList)
                {
                    var dash = ingridient.IndexOf('-');
                    var ingridientName = ingridient.Remove(dash - 1);
                    if (context.Ingridients.FirstOrDefault(i => i.Name == ingridientName).Allergen == true)
                    {
                        allergenList.Add(ingridientName);
                    }
                }
            }
            catch
            {
                var ingredientsList = JsonConvert.DeserializeObject<Dictionary<string, string>>(dish.Ingridients);
                foreach (var ingridient in ingredientsList)
                {
                    if (context.Ingridients.FirstOrDefault(i => i.Name == ingridient.Key).Allergen == true)
                    {
                        allergenList.Add(ingridient.Key);
                    }
                }
            }
            string allergens = string.Join(", ", allergenList);
            MessageBox.Show($"Аллергены: {allergens}", "Осторожно!", default, MessageBoxImage.Warning);
        }
    }
}
