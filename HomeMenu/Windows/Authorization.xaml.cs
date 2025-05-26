using HomeMenu.Database;
using HomeMenu.Functions;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        HomeMenuContext context = new();
        public Authorization()
        {
            InitializeComponent();
        }

        private void ForgotPasswordClick(object sender, MouseButtonEventArgs e)
        {
            Data.forget = true;
            Registration registration = new();
            registration.Show();
            this.Close();
        }

        private void RegistrationClick(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (!Functions.Authorization.Authorize(tbLogin.Text, tbPassword.Password))
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
            else
            {
                Data.email = tbLogin.Text;
                Data.profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == tbLogin.Text).Id);
                MainWindow mainWindow = new();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
