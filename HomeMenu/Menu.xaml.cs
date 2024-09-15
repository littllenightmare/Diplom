using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HomeMenu.Database;
using Microsoft.EntityFrameworkCore;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }
        MenuContext _db = new MenuContext();
        Yum _yum;
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadDBInLV();
        }
        void LoadDBInLV()
        {
            using (_db)
            {
                int selectedIndex = MenuItems.SelectedIndex;
                _db.Yums.Load();
                MenuItems.ItemsSource = _db.Yums.ToList();
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
            Filter f = new Filter();
            f.Show();
        }
    }
}
