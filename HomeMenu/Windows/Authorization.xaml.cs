using HomeMenu.Functions;
using System.Windows;
using System.Windows.Input;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
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
                MainWindow mainWindow = new();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
