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
            LoadProfileData();
        }
        public void LoadProfileData()
        {
            var profile = Data.profile;
            var user = context.Users.FirstOrDefault(u => u.Email == Data.email);
            if (profile.UserId == user.Id)
            {
                Exitbtn.Visibility = Visibility.Visible;
                Buybtn.Visibility = Visibility.Visible;
                EditNameBtn.Visibility = Visibility.Visible;
                EditDescBtn.Visibility = Visibility.Visible;
            }
            DescTextBox.Text = profile.Description;
            NameTextBox.Text = profile.Name;
            if (!string.IsNullOrEmpty(profile.Photo))
            {
                PhotoImage.Source = new BitmapImage(new Uri(profile.Photo, UriKind.RelativeOrAbsolute));
            }
            else
            {
                PhotoImage.Source = new BitmapImage(new Uri("/Images/profile.webp", UriKind.RelativeOrAbsolute));
            }
                Disheslv.ItemsSource = context.Dishes.Where(d => d.Author == profile.Id).ToList();
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
            Data.profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Data.email = null;
            Data.profile = null;
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

                var paymentResult = await Main.CreatePay(amount, orderId, description, user.Id, Privacy.ConfigurationHelper.Configuration);
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
            var profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
            if (Data.profile.Id == profile.Id)
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
            profile.Photo = _selectedImagePath;
            context.Profiles.Update(profile);
            context.SaveChanges();
        }
        private void EditName_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditingName)
            {
                EditNameBtn.Content = "✓";
                NameTextBox.IsReadOnly = false;
                _isEditingName = true;
            }
            else
            {
                var profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
                if (profile != null)
                {
                    profile.Name = NameTextBox.Text;
                    context.Profiles.Update(profile);
                    context.SaveChanges();
                }

                EditNameBtn.Content = "✏️";
                _isEditingName = false;
                NameTextBox.IsReadOnly = true;
            }
        }

        private void EditDesc_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditingDesc)
            {
                EditDescBtn.Content = "✓";
                DescTextBox.IsReadOnly = false;
                _isEditingDesc = true;
            }
            else
            {
                var profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
                if (profile != null)
                {
                    profile.Description = DescTextBox.Text;
                    context.Profiles.Update(profile);
                    context.SaveChanges();
                }

                EditDescBtn.Content = "✏️";
                _isEditingDesc = false;
                DescTextBox.IsReadOnly = true;
            }
        }

    }
}
