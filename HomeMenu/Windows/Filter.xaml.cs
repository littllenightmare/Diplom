using HomeMenu.Database;
using HomeMenu.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Filter.xaml
    /// </summary>
    public partial class Filter : Window
    {
        private List<Yum> _filteredData;

        string[] categories = new string[9];
        Yum yum;
        MenuContext _context = new MenuContext();
        public Filter()
        {
            InitializeComponent();
            MessageBox.Show("Обратите внимание: калории рассчитываются на одну порции блюда, а не на все");
            categories[0] = "Завтрак";
            categories[1] = "Суп";
            categories[2] = "Десерт";
            categories[5] = "Второе";
            categories[6] = "Закуска";
            categories[7] = "Салат";
        }

        private void FilterClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int calory = 0, belki = 0, zhiri = 0, uglevodi = 0, portion = 0;
                string message = "", categ = "", name="";
                switch (nametb.Text)
                {
                    case "":
                        break;
                    default:
                        if (Int32.TryParse(nametb.Text, out int a) != true)
                        {
                            name = nametb.Text;
                            break;
                        }
                        else
                        {
                            message = message + "Название это буковки\n";
                            break;
                        }
                }
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
                    case "Закуска":
                        categ = "Закуска";
                        break;
                    case "Салат":
                        categ = "Салат";
                        break;
                    case "Второе":
                        categ = "Второе";
                        break;
                }
                if (message != "")
                {
                    MessageBox.Show(message);
                }
                else
                {
                    var query = _context.Yums.AsQueryable();

                    if (!String.IsNullOrEmpty(name))
                    {
                        query = query.Where(y => y.Название.ToLower().Contains(name.ToLower()));
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(belki)))
                    {
                        if(belki !=0) query = query.Where(y => y.БелкиФулл >= belki && y.БелкиФулл <= belki + 50);
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(zhiri)))
                    {
                       if(zhiri !=0) query = query.Where(y => y.ЖирыФулл >= zhiri && y.ЖирыФулл <= zhiri + 30);
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(calory)))
                    {
                       if(calory != 0) query = query.Where(y => y.КалорииФулл+200 >= calory && y.КалорииФулл <= (calory + 200));
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(categ)))
                    {
                        query = query.Where(y => y.Категория == categ);
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(portion)))
                    {
                        if(portion != 0) query = query.Where(y => y.Порции == portion);
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(uglevodi)))
                    {
                        if(uglevodi != 0) query = query.Where(y => y.УглеводыФулл >= uglevodi && y.УглеводыФулл <= uglevodi + 30);
                    }

                    _filteredData = query.ToList();
                    var menuWindow = new Menu(_filteredData);
                    menuWindow.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Не ломай фильтр");
            }
        }
    }
}
