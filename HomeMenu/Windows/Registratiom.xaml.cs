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
                Titlelb.Content = "Восстановление пароля";
                action = "forget";
            }
        }
        
        private void NextClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (action == "email" || action == "forget")
                {
                    var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                    if (!emailRegex.IsMatch(tbEmail.Text))
                        MessageBox.Show("Неверный формат адреса электронной почты.");
                    if (action == "email" && context.Users.FirstOrDefault(u => u.Email == tbEmail.Text) != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Пользователь с такой почтой уже пытался зарегистрироваться. Вы хотите удалить старый аккаунт и создать новый?", default, MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            var user = context.Users.First(u => u.Email == tbEmail.Text);
                            context.Users.Remove(user);
                            context.SaveChangesAsync();
                        }
                        else
                        {
                            Authorization authorization = new();
                            authorization.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        if (action == "email") Functions.Authorization.SendEmail(tbEmail.Text);
                        else Functions.Authorization.ForgetPassword(tbEmail.Text);
                        tbEmail.Clear();
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
                        tbEmail.Clear();
                        Emaillb.Content = "ВВЕДИТЕ ПАРОЛЬ";
                        action = "password";
                    }
                }

                else if (action == "password")
                {
                    if (tbEmail.Text.Length < 2 || tbEmail.Text.Length >10)
                        MessageBox.Show("Пароль должен состоять хотя бы из 2 символов и не больше 10.");
                    else
                    {
                        Functions.Authorization.SetPassword(tbEmail.Text);
                        Authorization authorization = new();
                        authorization.Show();
                        this.Close();
                    }
                }
            }
            catch
            {

            }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new();
            authorization.Show();
            this.Close();
        }
    }
}
