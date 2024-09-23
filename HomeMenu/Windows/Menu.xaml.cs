using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HomeMenu.Database;
using HomeMenu.Functions;
using Microsoft.EntityFrameworkCore;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private List<Yum> _filteredData;
        public Menu(List<Yum> filteredData)
        {
            InitializeComponent();
            if (filteredData != null)
            {
                MenuItems.ItemsSource = filteredData;
                MenuItems.Items.Refresh();
            }
            else LoadDBInLV();

        }
        MenuContext _db = new MenuContext();
        Yum _yum;
        async void LoadDBInLV()
        {
            MenuContext _db = new MenuContext();
            using (_db)
            {
                int selectedIndex = MenuItems.SelectedIndex;
                await _db.Yums.LoadAsync();
                MenuItems.ItemsSource = null;
                MenuItems.Items.Clear();
                foreach (Yum yum in _db.Yums)
                {
                    MenuItems.Items.Add(yum);
                }
                if (selectedIndex != -1)
                {
                    if (selectedIndex == MenuItems.Items.Count) selectedIndex--;
                    MenuItems.SelectedIndex = selectedIndex;
                    MenuItems.ScrollIntoView(MenuItems.Items[selectedIndex]);
                }
                MenuItems.Focus();
            }
        }

        private void ReturnMain(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }

        private void FilterClick(object sender, MouseButtonEventArgs e)
        {
            var filterWindow = new Filter();
            filterWindow.Show();
            this.Close();
        }

        private void DishInfoClick(object sender, MouseButtonEventArgs e)
        {
            DishInfo di = new DishInfo();
            var dish = MenuItems.Items.CurrentItem;
            di.GetDish(dish);
            di.Show();
        }

        private async void UnFilterClick(object sender, MouseButtonEventArgs e)
        {
            LoadDBInLV();
        }
    }
}
