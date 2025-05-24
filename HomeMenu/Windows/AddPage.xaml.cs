using HomeMenu.Database;
using HomeMenu.Functions;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static HomeMenu.Functions.Forms;

namespace HomeMenu.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Window
    {
        HomeMenuContext context = new();
        private string _selectedImagePath;
        public AddPage()
        {
            InitializeComponent();
            if (context.Users.FirstOrDefault(u => u.Email == Data.email).Role != 2)
            {
                MessageBox.Show("Создать блюдо можно только оплатив подписку!");
                ProfilePage profile = new ProfilePage();
                profile.Show();
                this.Close();
            }
            DifficultComboBox.ItemsSource = new List<string>
    {
        "1",
        "2",
        "3",
        "4",
        "5"
    };
        }
        private void AddIngridientClick(object sender, MouseButtonEventArgs e)
        {
            if (SearchListBox.SelectedItem != null)
            {
                var selected = (Ingridient)SearchListBox.SelectedItem;
                if (!IngredientsList.Items.Cast<IngredientItem>().Any(i => i.Name == selected.Name))
                {
                    IngredientsList.Items.Add(new IngredientItem
                    {
                        Name = selected.Name,
                        Amount = 100
                    });
                    SearchTextBox.Clear();
                    SearchListBox.ItemsSource = null;
                    SearchListBox.SelectedItem = null;
                }
            }
            else if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                var ingredientName = SearchTextBox.Text.Trim();
                if (!IngredientsList.Items.Cast<IngredientItem>().Any(i => i.Name == ingredientName))
                {
                    IngredientsList.Items.Add(new IngredientItem
                    {
                        Name = ingredientName,
                        Amount = 100
                    });
                    SearchTextBox.Clear();
                    SearchListBox.ItemsSource = null;
                    SearchListBox.SelectedItem = null;
                }
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var searchText = SearchTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    SearchListBox.ItemsSource = null;
                    return;
                }

                var results = context.Ingridients
                    .Where(i => i.Name.Contains(searchText))
                    .Take(10)
                    .ToList();

                SearchListBox.ItemsSource = results;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка поиска: {ex.Message}");
            }
        }
        private void RecipeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                var currentText = RecipeTextBox.Text;
                var selectionStart = RecipeTextBox.SelectionStart;

                int lastNumber = 1;
                var lines = currentText.Split('\n');
                if (lines.Length > 0)
                {
                    var lastLine = lines.LastOrDefault(l => l.Trim().Length > 0);
                    if (lastLine != null && lastLine.Contains("."))
                    {
                        var numStr = lastLine.Split('.')[0];
                        if (int.TryParse(numStr, out int num))
                        {
                            lastNumber = num + 1;
                        }
                    }
                }
                RecipeTextBox.Text = currentText.Insert(selectionStart, $"\n{lastNumber}. ");
                RecipeTextBox.SelectionStart = selectionStart + $"\n{lastNumber}. ".Length;
            }
        }
        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (IngredientItem item in IngredientsList.Items)
                {
                    if (item.Amount <= 0)
                    {
                        MessageBox.Show($"Укажите количество для ингредиента {item.Name}");
                        return;
                    }
                }
                if (!Main.ValidateDish(NameTextBox.Text, PhotoImage.Source.ToString(), CaloriesTextBox.Text, ProteinsTextBox.Text, FatsTextBox.Text, CarbohydratesTextBox.Text,
                    PortionsTextBox.Text, HoursTextBox.Text, MinutesTextBox.Text, RecipeTextBox.Text, IngredientsList.ItemsSource)) return;
                var ingredientsJson = JsonConvert.SerializeObject(IngredientsList.Items.Cast<IngredientItem>().ToList());

                var dish = new Dish
                {
                    Name = NameTextBox.Text,
                    Calories = double.Parse(CaloriesTextBox.Text),
                    Proteins = double.Parse(ProteinsTextBox.Text),
                    Fats = double.Parse(FatsTextBox.Text),
                    Carbohydrates = double.Parse(CarbohydratesTextBox.Text),
                    Portion = int.Parse(PortionsTextBox.Text),
                    Difficult = Int32.Parse(DifficultComboBox.SelectedItem.ToString()),
                    Time = 60*int.Parse(HoursTextBox.Text)+int.Parse(MinutesTextBox.Text),
                    Ingridients = ingredientsJson,
                    Photo = _selectedImagePath,
                    Author = context.Users.FirstOrDefault(u=> u.Email == Data.email).Id,
                    Recipe = RecipeTextBox.Text
                };

                context.Dishes.Add(dish);
                context.SaveChanges();
                MessageBox.Show("Блюдо успешно добавлено!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private void LoadImageClick(object sender, MouseButtonEventArgs e)
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

        private void SearchListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddIngridientClick(sender, e);
        }

        private void DeleteIngridientClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var ingredient = (IngredientItem)button.DataContext;
            IngredientsList.Items.Remove(ingredient);
        }
    }
}
