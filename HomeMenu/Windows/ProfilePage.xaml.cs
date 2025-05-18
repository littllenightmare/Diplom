using HomeMenu.Database;
using HomeMenu.Functions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HomeMenu.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Window
    {
        public ProfilePage()
        {
            InitializeComponent();
            HomeMenuContext context = new();
            if (Data.profile == context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id))
            {
                Returnbtn.Visibility = Visibility.Visible;
                Exitbtn.Visibility = Visibility.Visible;
                Buybtn.Visibility = Visibility.Visible;
                Editbtn.Visibility = Visibility.Visible;
            }
        }

        private void DishChoosed(object sender, SelectionChangedEventArgs e)
        {
            Data.dish = (Dish)Disheslv.SelectedItem;
            DishInfoPage dishinfo = new();
            dishinfo.Show();
            this.Close();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Data.email = null;
            Authorization authorization = new();
            authorization.Show();
            this.Close();
        }

        private void BuyClick(object sender, RoutedEventArgs e)
        {

        }

        private void EditClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
