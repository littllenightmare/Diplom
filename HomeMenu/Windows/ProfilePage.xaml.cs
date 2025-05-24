using HomeMenu.Database;
using HomeMenu.Functions;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HomeMenu.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Window
    {
        private string _selectedImagePath;
        private bool _isEditingName = false;
        private bool _isEditingDesc = false;
        HomeMenuContext context = new();
        public ProfilePage()
        {
            InitializeComponent();           
            if (Data.profile == context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id))
            {
                Returnbtn.Visibility = Visibility.Visible;
                Exitbtn.Visibility = Visibility.Visible;
                Buybtn.Visibility = Visibility.Visible;
                EditNameBtn.Visibility = Visibility.Visible;
                EditDescBtn.Visibility = Visibility.Visible;
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

        private async void BuyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string orderId = Guid.NewGuid().ToString();
                int amount = 1000;
                string description = "Оплата подписки";
                var user = context.Users.FirstOrDefault(u => u.Email == Data.email);

                var paymentResult = await Main.CreatePay(amount, orderId, description, user.Id);
                Process.Start(new ProcessStartInfo
                {
                    FileName = paymentResult,
                    UseShellExecute = true
                });

                await Task.Delay(60000);

                user.Role = 2;
                await context.SaveChangesAsync();
                MessageBox.Show("Оплата прошла успешно! Ваш аккаунт обновлен до премиум-статуса.");
            }
            catch { }
        }

        private void LoadImageClick(object sender, MouseButtonEventArgs e)
        {
            if (Data.profile == context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id))
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                    Title = "Выберите изображение блюда"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    _selectedImagePath = openFileDialog.FileName;
                    PhotoImage.Source = new BitmapImage(new Uri(_selectedImagePath));
                }
            }
        }
        private void EditName_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditingName)
            {
                EditNameBtn.Content = "✓";
                _isEditingName = true;
            }
            else
            {
                var profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
                if (profile != null)
                {
                    profile.Name = NameTextBlock.Text;
                    context.SaveChanges();
                }

                EditNameBtn.Content = "✏️";
                _isEditingName = false;
            }
        }

        private void EditDesc_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditingDesc)
            {
                EditDescBtn.Content = "✓";
                _isEditingDesc = true;
            }
            else
            {
                var profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
                if (profile != null)
                {
                    profile.Description = DescTextBlock.Text;
                    context.SaveChanges();
                }

                EditDescBtn.Content = "✏️";
                _isEditingDesc = false;
            }
        }

    }
}
