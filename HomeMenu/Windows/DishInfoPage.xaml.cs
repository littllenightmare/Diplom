using HomeMenu.Database;
using HomeMenu.Functions;
using System;
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
        void LoadDishToForm()
        {
            var dish = Data.dish;
            PhotoImage.Source = new BitmapImage(new Uri(dish.Photo, UriKind.RelativeOrAbsolute));
            Nametb.Text = dish.Name;

            string time = "";
            var hours = dish.Time / 60;
            if(hours > 0) time += $"{hours} ч. ";
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
            Ingridientstb.Text = dish.Ingridients.Replace("\"", "").Replace("{", "").Replace("}", "").Replace(":", "- ").Replace(",", "");
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();
            this.Close();
        }

        private void GoToProfile(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (context.Profiles.FirstOrDefault(p => p.Name == AuthorRun.Text) != null)
            {
                ProfilePage profilePage = new();
                profilePage.Show();
                this.Close();
            }
        }
    }
}
