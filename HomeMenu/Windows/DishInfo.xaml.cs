using HomeMenu.Database;
using Microsoft.EntityFrameworkCore;
using System;
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

        public void GetDish(object dish)
        {
            Yum yum = (Yum)dish;
            string dishId = yum.Название;
            Yum dbDish = _db.Yums.Find(dishId);
            if (dbDish != null)
            {
                name.Content = dbDish.Название;
                tbcalories.Text = Convert.ToString(dbDish.КалорииФулл);
                tbportion.Text = Convert.ToString(dbDish.Порции);
                tbbelki.Text = Convert.ToString(dbDish.БелкиФулл);
                tbgrams.Text = Convert.ToString(dbDish.Граммы);
                tbzhiri.Text = Convert.ToString(dbDish.ЖирыФулл);
                tbrecipy.Text = Convert.ToString(dbDish.Рецепт);
                tbuglevodi.Text = Convert.ToString(dbDish.УглеводыФулл);
                var filteredArray = dbDish.ИнгридиентыФулл.Where(c => c != '"' && c != '{' && c != '}').ToArray();
                tbingridients.Text = string.Join("", filteredArray);
            }
        }
        private void ReturnMain(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}
