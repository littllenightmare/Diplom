using HomeMenu.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для DishInfo.xaml
    /// </summary>
    public partial class DishInfo : Window
    {
        Yum _yum;
        MenuContext _db = new MenuContext();
        public DishInfo()
        {
            InitializeComponent();
        }

        public void GetDish(string dish)
        {
            name.Content = dish;
        }
        private void ReturnMain(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}
