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
            name.Content = dish;
            tbcalories.Text = Convert.ToString(_db.Yums.Find(dish).КалорииФулл);
            tbportion.Text = Convert.ToString(_db.Yums.Find(dish).Порции);
            tbbelki.Text = Convert.ToString(_db.Yums.Find(dish).БелкиФулл);
            tbgrams.Text = Convert.ToString(_db.Yums.Find(dish).Граммы);
            tbdate.Text = Convert.ToString(_db.Yums.Find(dish).Давность);
            tbzhiri.Text = Convert.ToString(_db.Yums.Find(dish).ЖирыФулл);
            tbingridients.Text = Convert.ToString(_db.Yums.Find(dish).ИнгридиентыФулл);
            tbcategory.Text = Convert.ToString(_db.Yums.Find(dish).Категория);
            tbrecipy.Text = Convert.ToString(_db.Yums.Find(dish).Рецепт);
            tbuglevodi.Text = Convert.ToString(_db.Yums.Find(dish).УглеводыФулл);
        }
        private void ReturnMain(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}
