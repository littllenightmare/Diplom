﻿using HomeMenu.Database;
using HomeMenu.Functions;
using Microsoft.Win32;
using System;
using System.Linq;
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
        /// <summary>
        /// данные для редактирования профиля
        /// </summary>
        private string _selectedImagePath;
        private bool _isEditingName = false;
        private bool _isEditingDesc = false;
        HomeMenuContext context = new();
        public ProfilePage()
        {
            InitializeComponent();
            LoadProfileData();
        }
        /// <summary>
        /// выгрузка данных профиля на форму
        /// </summary>
        public void LoadProfileData()
        {
            try
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
            catch { }
        }
        /// <summary>
        /// выбор блюда из всех блюд пользователя
        /// </summary>
        /// <param name="sender">элемент листбокса с блюдами</param>
        /// <param name="e">событие нажатия на элемент</param>
        private void DishChoosed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Data.dish = (Dish)Disheslv.SelectedItem;
                DishInfoPage dishinfo = new();
                dishinfo.Show();
                this.Close();
            }
            catch { }
        }
        /// <summary>
        /// возврат на главный экран
        /// </summary>
        /// <param name="sender">кнопка На главную</param>
        /// <param name="e">событие нажатия на кнопку</param>
        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
                MainWindow mainWindow = new();
                mainWindow.Show();
                this.Close();
            }
            catch { }
        }
        /// <summary>
        /// Выход из аккаунта
        /// </summary>
        /// <param name="sender">кнопка Выход</param>
        /// <param name="e">событие нажатия на кнопку</param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.email = null;
                Data.profile = null;
                Authorization authorization = new();
                authorization.Show();
                this.Close();
            }
            catch { }
        }
        /// <summary>
        /// Покупка подписки, пока тестовая
        /// </summary>
        /// <param name="sender">кнопка Подписка</param>
        /// <param name="e">событие нажатия на кнопку</param>
        private async void BuyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string orderId = Guid.NewGuid().ToString();
                int amount = 1000;
                string description = "Оплата подписки";
                var user = context.Users.FirstOrDefault(u => u.Email == Data.email);

                if (user.Role != 2)
                {
                    var paymentResult = await Main.CreatePay(amount, orderId, description, user.Id, Privacy.ConfigurationHelper.Configuration);
                    user.Role = 2;
                    await context.SaveChangesAsync();
                    MessageBox.Show("Оплата прошла успешно! Ваш аккаунт обновлен до премиум-статуса.");
                }
                else
                {
                    MessageBox.Show("Подписка уже подключена на веки вечные!");
                }
            }
            catch { }
        }
        /// <summary>
        /// Смена аватарки
        /// </summary>
        /// <param name="sender">фото аватарки</param>
        /// <param name="e">событие нажатия на картинку</param>
        private void LoadImageClick(object sender, MouseButtonEventArgs e)
        {
            try
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
            catch { }
        }
        /// <summary>
        /// Редактирование названия канала
        /// </summary>
        /// <param name="sender">кнопка карандашика у названия</param>
        /// <param name="e">нажатие на карандашик</param>
        private void EditName_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch { }
        }
        /// <summary>
        /// Редактирование описания профиля
        /// </summary>
        /// <param name="sender">Кнопка карандашика у описания</param>
        /// <param name="e">нажатие на карандашик</param>
        private void EditDesc_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch { }
        }

    }
}
