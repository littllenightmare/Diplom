using HomeMenu.Database;
using HomeMenu.Functions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        string action = "email";
        HomeMenuContext context = new();
        public Registration()
        {
            InitializeComponent();
            if (Data.forget == true)
            {
                Titlelb.Content = "ВОССТАНОВЛЕНИЕ ПАРОЛЯ";
                action = "forget";
            }
        }
        
        private async void NextClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (action == "email" || action == "forget")
                {
                    var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                    if (!emailRegex.IsMatch(tbEmail.Text))
                        MessageBox.Show("Неверный формат адреса электронной почты.");
                    else
                    {
                        if (action == "email") Functions.Authorization.SendEmail(tbEmail.Text);
                        else Functions.Authorization.ForgetPassword(tbEmail.Text);
                        Emaillb.Content = "ВВЕДИТЕ КОД С ПОЧТЫ";
                        action = "check";
                        Data.email = tbEmail.Text;
                    }
                }
                else if (action == "check")
                {
                    var code = context.Users.FirstOrDefault(u => u.Email == Data.email).Code;
                    if (code != Int32.Parse(tbEmail.Text))
                        MessageBox.Show("Код введён неверно!");
                    else
                    {
                        Emaillb.Content = "ВВЕДИТЕ ПАРОЛЬ";
                        action = "password";
                    }
                }

                else if (action == "password")
                {
                    Functions.Authorization.SetPassword(tbEmail.Text);
                    Authorization authorization = new();
                    authorization.Show();
                    this.Close();
                }
            }
            catch
            {

            }
        }
    }
}
