using HomeMenu.Database;
using System;
using System.Linq;
using System.Windows;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Filter.xaml
    /// </summary>
    public partial class Filter : Window
    { 
        string[] categories = new string[9];
        string[] dates = new string[5];
        Yum yum = new Yum();
        MenuContext _context = new MenuContext();
        public Filter()
        {
            InitializeComponent();
            categories[0] = "Каша";
            categories[1] = "Суп";
            categories[2] = "Десерт";
            categories[3] = "Мяскаф";
            categories[4] = "Гарнир";
            categories[5] = "Второе";
            categories[6] = "НеБлюдо";
            categories[7] = "Холодное блюдо";
            categories[8] = "Салат";

            dates[0] = "Съестся сейчас";
            dates[0] = "1-2 дня";
            dates[0] = "3-4 дня";
            dates[0] = "5-6 дней";
            dates[0] = "Неделя";
        }

        private void FilterClick(object sender, RoutedEventArgs e)
        {
            int calory=0, belki=0, zhiri = 0, uglevodi=0, portion = 0, date=0;
            string message="", categ="";
            switch (caloriestb.Text)
            {
                case "":
                    break;
                default:
                    if (Int32.TryParse(caloriestb.Text, out calory) == true)
                    {
                        break;
                    }
                    else
                    {
                        message = message + "Калории это циферки\n";
                        break;
                    }

            }
            switch (belkitb.Text)
            {
                case "":
                    break;
                default:
                    if (Int32.TryParse(belkitb.Text, out belki) == true)
                    {
                        break;
                    }
                    else
                    {
                        message = message + "Белки это не буковки\n";
                        break;
                    }

            }
            switch (zhiritb.Text)
            {
                case "":
                    break;
                default:
                    if (Int32.TryParse(zhiritb.Text, out zhiri) == true)
                    {
                        break;
                    }
                    else
                    {
                        message = message + "Жиры не любят буковки\n";
                        break;
                    }

            }
            switch (uglevoditb.Text)
            {
                case "":
                    break;
                default:
                    if (Int32.TryParse(uglevoditb.Text, out uglevodi) == true)
                    {
                        break;
                    }
                    else
                    {
                        message = message + "Углеводы любят циферки\n";
                        break;
                    }

            }
            switch (portiontb.Text)
            {
                case "":
                    break;
                default:
                    if (Int32.TryParse(portiontb.Text, out portion) == true)
                    {
                        break;
                    }
                    else
                    {
                        message = message + "Как ты считаешь порции?" + " ";
                        break;
                    }

            }
            switch (datecb.Text)
            {
                case null:
                    break;
                case "Съестся сейчас":
                    break;
                case "1-2 дня":
                    date = 2;
                    break;
                case "3-4 дня":
                    date = 4;
                    break;
                case "5-6 дней":
                    date = 6;
                    break;
                case "Неделя":
                    date = 7;
                    break;

            }
            switch (categorycb.Text)
            {
                case null:
                    break;
                case "Каша":
                    categ = "Каша";
                    break;
                case "Суп":
                    categ = "Суп";
                    break;
                case "Десерт":
                    categ = "Десерт";
                    break;
                case "Мяскаф":
                    categ = "Мяскаф";
                    break;
                case "Гарнир":
                    categ = "Гарнир";
                    break;
                case "Второе":
                    categ = "Второе";
                    break;
                case "НеБлюдо":
                    categ = "НеБлюдо";
                    break;
                case "Салат":
                    categ = "Салат";
                    break;
                case "Холодное блюдо":
                    categ = "Хблюдо";
                    break;
            }
            if (message != "")
            {
                MessageBox.Show(message);
            }
            else
            {
                Menu m = new Menu();
                m.MenuItems.Items.Add(_context.Yums.All(
                    y=>yum.БелкиФулл >= belki && yum.БелкиФулл <= belki + 50)
                    && yum.ЖирыФулл >= zhiri && yum.ЖирыФулл <= zhiri + 30
                    && yum.КалорииФулл >= calory && yum.КалорииФулл <= calory +200
                    && yum.Категория == categ
                    && yum.Порции == portion
                    && yum.УглеводыФулл >= uglevodi && yum.УглеводыФулл <= uglevodi + 30
                    && yum.Хранение == date);
                this.Close();
                m.Show();
            }
        }
    }
}
