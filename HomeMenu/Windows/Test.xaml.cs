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
        int checker = 0, calories = 0, belki = 0, zhiri = 0, uglevodi = 0;

        private void AClick(object sender, MouseButtonEventArgs e)
        {

            var query = _context.Yums.AsEnumerable();
            try
            {
                switch (checker)
                {
                    case 0:
                        if (Int32.TryParse(Calories.Text, out calories) == true)
                        {
                            belki = 30 * calories / 100; zhiri = belki; uglevodi = calories - 2 * belki;
                            var caloriesCondition = query.Where(y => (int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 100) >= (calories / 3) && int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= (calories / 3 + 100));
                            var belkiCondition = query.Where(y => int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= belki / 3 && int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.ToString()) <= belki / 3 + 200);
                            var zhiriCondition = query.Where(y => int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= zhiri && int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= zhiri + 200);
                            var uglevodiCondition = query.Where(y => int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= uglevodi && int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= uglevodi + 200);

                            Console.WriteLine("Calories condition count: " + caloriesCondition.Count());
                            Console.WriteLine("Belki condition count: " + belkiCondition.Count());
                            Console.WriteLine("Zhiri condition count: " + zhiriCondition.Count());
                            Console.WriteLine("Uglevodi condition count: " + uglevodiCondition.Count()); 
                            
                            _filteredData = query.ToList();
                            Calories.Visibility = Visibility.Hidden;
                            AnswerA.Visibility = Visibility.Visible;
                            Question.Content = "Выбери приём пищи";
                            AnswerA.Content = "Завтрак"; 
                            AnswerB.Content = "Обед";
                            AnswerC.Content = "Ужин";
                            AnswerD.Visibility=Visibility.Hidden;
                            checker++;
                        }
                        break;
                    case 1:
                        query = query.Where(y => yum.Категория == "Завтрак" || yum.Категория == "Закуска" || yum.Категория == "Десерт");
                        _filteredData = query.ToList();
                        DishInfo di = new DishInfo();
                        di.Show();
                        Random rnd = new Random();
                        var dish = query.ElementAt(rnd.Next(query.Count()));
                        this.Close();
                        di.GetDish(dish);
                        di.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }
        private void AClick(object sender, KeyEventArgs e)
        {
            var query = _context.Yums.AsEnumerable();
            try
            {
                switch (checker)
                {
                    case 0:
                        if (Int32.TryParse(Calories.Text, out calories) == true)
                        {
                            belki = 30 * calories / 100; zhiri = belki; uglevodi = calories-2*belki;
                            var caloriesCondition = query.Where(y => (int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 100) >= (calories / 3) && int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= (calories / 3 + 100));
                            var belkiCondition = query.Where(y => int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= belki / 3 && int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.ToString()) <= belki / 3 + 200);
                            var zhiriCondition = query.Where(y => int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= zhiri && int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= zhiri + 200);
                            var uglevodiCondition = query.Where(y => int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= uglevodi && int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= uglevodi + 200);

                            Console.WriteLine("Calories condition count: " + caloriesCondition.Count());
                            Console.WriteLine("Belki condition count: " + belkiCondition.Count());
                            Console.WriteLine("Zhiri condition count: " + zhiriCondition.Count());
                            Console.WriteLine("Uglevodi condition count: " + uglevodiCondition.Count());
                            
                            _filteredData = query.ToList();
                            Calories.Visibility = Visibility.Hidden;
                            AnswerA.Visibility = Visibility.Visible;
                            Question.Content = "Выбери приём пищи";
                            AnswerA.Content = "Завтрак";
                            AnswerB.Content = "Обед";
                            AnswerC.Content = "Ужин";
                            AnswerD.Visibility=Visibility.Hidden;
                            checker++;
                        }
                        else MessageBox.Show("Я знаю, что такое валидация");
                        break;
                    case 1:
                        query = query.Where(y => yum.Категория == "Завтрак" || yum.Категория == "Закуска" || yum.Категория == "Десерт");
                        _filteredData = query.ToList();
                        DishInfo di = new DishInfo();
                        di.Show();
                        Random rnd = new Random();
                        var dish = query.ElementAt(rnd.Next(query.Count()));
                        this.Close();
                        di.GetDish(dish);
                        di.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }

        private void BClick(object sender, MouseButtonEventArgs e)
        {
            var query = _context.Yums.AsEnumerable();
            try
            {
                switch (checker)
                {
                    case 0:
                        calories = 2600;
                        belki = 780; zhiri = 780; uglevodi=1040;
                        var caloriesCondition = query.Where(y => (int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 100) >= (calories / 3) && int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= (calories / 3 + 100));
                        var belkiCondition = query.Where(y => int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= belki / 3 && int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.ToString()) <= belki / 3 + 200);
                        var zhiriCondition = query.Where(y => int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= zhiri && int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= zhiri + 200);
                        var uglevodiCondition = query.Where(y => int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= uglevodi && int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= uglevodi + 200);

                        Console.WriteLine("Calories condition count: " + caloriesCondition.Count());
                        Console.WriteLine("Belki condition count: " + belkiCondition.Count());
                        Console.WriteLine("Zhiri condition count: " + zhiriCondition.Count());
                        Console.WriteLine("Uglevodi condition count: " + uglevodiCondition.Count());
                        
                        _filteredData = query.ToList();
                        Calories.Visibility = Visibility.Hidden;
                        AnswerA.Visibility = Visibility.Visible;
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Visibility=Visibility.Hidden;
                        checker++;
                        break;
                    case 1:
                        query = query.Where(y => (yum.Категория == "Суп" || yum.Категория == "Второе" || yum.Категория == "Салат" || yum.Категория == "Закуска"));
                        _filteredData = query.ToList();
                        DishInfo di = new DishInfo();
                        di.Show();
                        Random rnd = new Random();
                        var dish = query.ElementAt(rnd.Next(query.Count()));
                        this.Close();
                        di.GetDish(dish);
                        di.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }


        private void CClick(object sender, MouseButtonEventArgs e)
        {
            var query = _context.Yums.AsEnumerable();
            try
            {
                switch (checker)
                {
                    case 0:
                        calories = 1500;
                        belki = 450; zhiri = 450; uglevodi = 600;
                        var caloriesCondition = query.Where(y => (int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 100) >= (calories / 3) && int.Parse(y.КалорииФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= (calories / 3 + 100));
                        var belkiCondition = query.Where(y => int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= belki / 3 && int.Parse(y.БелкиФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.ToString()) <= belki / 3 + 200);
                        var zhiriCondition = query.Where(y => int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= zhiri && int.Parse(y.ЖирыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= zhiri + 200);
                        var uglevodiCondition = query.Where(y => int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) + 200 >= uglevodi && int.Parse(y.УглеводыФулл.GetValueOrDefault().ToString()) / int.Parse(y.Порции.GetValueOrDefault().ToString()) <= uglevodi + 200);

                        Console.WriteLine("Calories condition count: " + caloriesCondition.Count());
                        Console.WriteLine("Belki condition count: " + belkiCondition.Count());
                        Console.WriteLine("Zhiri condition count: " + zhiriCondition.Count());
                        Console.WriteLine("Uglevodi condition count: " + uglevodiCondition.Count());
                        
                        _filteredData = query.ToList();
                        Calories.Visibility = Visibility.Hidden;
                        AnswerA.Visibility = Visibility.Visible;
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Visibility = Visibility.Hidden;
                        checker++;
                        break;
                    case 1:
                        query = query.Where(y => yum.Категория == "Второе" || yum.Категория == "Салат" || yum.Категория == "Закуска");
                        _filteredData = query.ToList();
                        DishInfo di = new DishInfo();
                        di.Show();
                        Random rnd = new Random();
                        var dish = query.ElementAt(rnd.Next(query.Count()));
                        this.Close();
                        di.GetDish(dish);
                        di.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }

        private void DClick(object sender, MouseButtonEventArgs e)
        {
            var query = _context.Yums.AsEnumerable();
            try
            {
                switch (checker)
                {
                    case 0:
                        HelperCalories hp = new HelperCalories();
                        hp.Show();
                        this.Close();
                        break;
                }
            }
            catch { MessageBox.Show("ass"); }
        }
    }
}
