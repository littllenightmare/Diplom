using HomeMenu.Database;
using HomeMenu.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        Yum yum = new Yum();
        private List<Yum> _filteredData;
        MenuContext _context = new MenuContext();

        public Test()
        {
            InitializeComponent();
        }
        int checker = 0, calories = 0;

        private void AClick(object sender, MouseButtonEventArgs e)
        {
            var query = _context.Yums.AsQueryable();
            try
            {
                switch (checker)
                {
                    case 0:
                        if (Int32.TryParse(Calories.Text, out calories) == true)
                        {
                            query = query.Where(y => (yum.КалорииФулл + 200) >= (calories) && yum.КалорииФулл <= (calories + 200));
                            _filteredData = query.ToList();
                            Calories.Visibility = Visibility.Hidden;
                            AnswerA.Visibility = Visibility.Visible;
                            Question.Content = "Выбери приём пищи";
                            AnswerA.Content = "Завтрак";
                            AnswerB.Content = "Обед";
                            AnswerC.Content = "Ужин";
                            AnswerD.Content = "Полный день";
                            checker++;
                        }
                        break;
                    case 1:
                        query = query.Where(y => yum.Категория == "Каша" || yum.Категория == "Закуска" || yum.Категория == "Десерт");
                        _filteredData = query.ToList();
                        Menu m = new Menu(_filteredData);
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }
        private void AClick(object sender, KeyEventArgs e)
        {
            var query = _context.Yums.AsQueryable();
            try
            {
                switch (checker)
                {
                    case 0:
                        if (Int32.TryParse(Calories.Text, out calories) == true)
                        {
                            query = query.Where(y => (yum.КалорииФулл + 200) >= (calories) && yum.КалорииФулл <= (calories + 200));
                            _filteredData = query.ToList();
                            Calories.Visibility = Visibility.Hidden;
                            AnswerA.Visibility = Visibility.Visible;
                            Question.Content = "Выбери приём пищи";
                            AnswerA.Content = "Завтрак";
                            AnswerB.Content = "Обед";
                            AnswerC.Content = "Ужин";
                            AnswerD.Content = "Полный день";
                            checker++;
                        }
                        else MessageBox.Show("Я знаю, что такое валидация");
                        break;
                    case 1:
                        query = query.Where(y => yum.Категория == "Каша" || yum.Категория == "Закуска" || yum.Категория == "Десерт");
                        _filteredData = query.ToList();
                        Menu m = new Menu(_filteredData);
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }

        private void BClick(object sender, MouseButtonEventArgs e)
        {
            var query = _context.Yums.AsQueryable();
            try
            {
                switch (checker)
                {
                    case 0:
                        calories = 2600;
                        query = query.Where(y => (yum.КалорииФулл) >= (calories) && yum.КалорииФулл <= (calories + 200));
                        _filteredData = query.ToList();
                        Calories.Visibility = Visibility.Hidden;
                        AnswerA.Visibility = Visibility.Visible;
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Content = "Полный день";
                        checker++;
                        break;
                    case 1:
                        query = query.Where(y => (yum.Категория == "Суп" || yum.Категория == "Второе" || yum.Категория == "Салат" || yum.Категория == "Закуска"));
                        _filteredData = query.ToList();
                        Menu m = new Menu(_filteredData);
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }


        private void CClick(object sender, MouseButtonEventArgs e)
        {
            var query = _context.Yums.AsQueryable();
            try
            {
                switch (checker)
                {
                    case 0:
                        calories = 1500;
                        query = query.Where(y => (yum.КалорииФулл + 200) >= (calories) && yum.КалорииФулл <= (calories + 200));
                        _filteredData = query.ToList();
                        Calories.Visibility = Visibility.Hidden;
                        AnswerA.Visibility = Visibility.Visible;
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Content = "Полный день";
                        checker++;
                        break;
                    case 1:
                        query = query.Where(y => yum.Категория == "Второе" || yum.Категория == "Салат" || yum.Категория == "Закуска");
                        _filteredData = query.ToList();
                        Menu m = new Menu(_filteredData);
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }

        private void DClick(object sender, MouseButtonEventArgs e)
        {
            var query = _context.Yums.AsQueryable();
            try
            {
                switch (checker)
                {
                    case 0:
                        HelperCalories hp = new HelperCalories();
                        hp.Show();
                        this.Close();
                        break;
                    case 1:
                        _filteredData = query.ToList();
                        Menu m = new Menu(_filteredData);
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }
    }
}
