using HomeMenu.Database;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        Yum yum = new Yum();
        MenuContext _context = new MenuContext();
        public Test()
        {
            InitializeComponent();
        }
        int checker = 0, calories = 0;
        string saver;
        private void AClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (checker)
                {
                    case 0:
                        if (Int32.TryParse(Calories.Text, out calories) == true)
                        {
                            Calories.Visibility = Visibility.Hidden;
                            AnswerA.Visibility = Visibility.Visible;
                            Question.Content = "Выбери срок хранения";
                            AnswerA.Content = "Съесть сегодня";
                            AnswerB.Content = "1-2 дня";
                            AnswerC.Content = "3-4 дня";
                            AnswerD.Content = "Около недели";
                            checker++;
                        }
                        break;
                    case 1:
                        saver = "01";
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Content = "Полный день";
                        checker++;
                        break;
                    case 2:
                        Menu m = new Menu();
                        m.MenuItems.Items.Add(_context.Yums.All(y =>
                        yum.КалорииФулл >= calories / 3 && yum.КалорииФулл <= calories / 3 + 200
                        && (yum.Категория == "Каша" || yum.Категория == "НеБлюдо" || yum.Категория == "Десерт")
                        && (yum.Хранение == Convert.ToInt32(saver.Substring(0, 1)) || yum.Хранение == Convert.ToInt32(saver.Substring(1, 1)))));
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("ass"); }
        }

        private void BClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (checker)
                {
                    case 0:
                        Calories.Visibility = Visibility.Hidden;
                        AnswerA.Visibility = Visibility.Visible;
                        Question.Content = "Выбери срок хранения";
                        AnswerA.Content = "Съесть сегодня";
                        AnswerB.Content = "1-2 дня";
                        AnswerC.Content = "3-4 дня";
                        AnswerD.Content = "Около недели";
                        calories = 2600;
                        checker++;
                        break;
                    case 1:
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Content = "Полный день";
                        saver = "12";
                        checker++;
                        break;
                    case 2:
                        Menu m = new Menu();
                        m.MenuItems.Items.Add(_context.Yums.All(y =>
                        yum.КалорииФулл >= calories / 3 && yum.КалорииФулл <= calories / 3 + 200
                        && (yum.Категория == "Суп" || yum.Категория == "НеБлюдо" || yum.Категория == "Мяскаф" || yum.Категория == "Гарнир" || yum.Категория == "Второе")
                        && (yum.Хранение == Convert.ToInt32(saver.Substring(0, 1)) || yum.Хранение == Convert.ToInt32(saver.Substring(1, 1)))));
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("ass"); }
        }


        private void CClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                switch (checker)
                {
                    case 0:
                        Calories.Visibility = Visibility.Hidden;
                        AnswerA.Visibility = Visibility.Visible;
                        Question.Content = "Выбери срок хранения";
                        AnswerA.Content = "Съесть сегодня";
                        AnswerB.Content = "1-2 дня";
                        AnswerC.Content = "3-4 дня";
                        AnswerD.Content = "Около недели";
                        calories = 1500;
                        checker++;
                        break;
                    case 1:
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Content = "Полный день";
                        saver = "34";
                        checker++;
                        break;
                    case 2:
                        Menu m = new Menu();
                        m.MenuItems.Items.Add(_context.Yums.All(y =>
                        yum.КалорииФулл >= calories / 3 && yum.КалорииФулл <= calories / 3 + 200
                        && (yum.Категория == "НеБлюдо" || yum.Категория == "Мяскаф" || yum.Категория == "Гарнир" || yum.Категория == "Второе")
                        && (yum.Хранение == Convert.ToInt32(saver.Substring(0, 1)) || yum.Хранение == Convert.ToInt32(saver.Substring(1, 1)))));
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("ass"); }
        }

        private void AClick(object sender, KeyEventArgs e)
        {
            try
            {
                switch (checker)
                {
                    case 0:
                        if (Int32.TryParse(Calories.Text, out calories) == true)
                        {
                            Calories.Visibility = Visibility.Hidden;
                            AnswerA.Visibility = Visibility.Visible;
                            Question.Content = "Выбери срок хранения";
                            AnswerA.Content = "Съесть сегодня";
                            AnswerB.Content = "1-2 дня";
                            AnswerC.Content = "3-4 дня";
                            AnswerD.Content = "Около недели";
                            checker++;
                        }
                        else MessageBox.Show("Я знаю, что такое валидация");
                        break;
                    case 1:
                        saver = "01";
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Content = "Полный день";
                        checker++;
                        break;
                    case 2:
                        Menu m = new Menu();
                        m.MenuItems.Items.Add(_context.Yums.All(y =>
                        yum.КалорииФулл >= calories / 3 && yum.КалорииФулл <= calories / 3 + 200
                        && (yum.Категория == "Каша" || yum.Категория == "НеБлюдо" || yum.Категория == "Десерт")
                        && (yum.Хранение == Convert.ToInt32(saver.Substring(0, 1)) || yum.Хранение == Convert.ToInt32(saver.Substring(1, 1)))));
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("ass"); }
        }

        private void DClick(object sender, MouseButtonEventArgs e)
        {
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
                        Question.Content = "Выбери приём пищи";
                        AnswerA.Content = "Завтрак";
                        AnswerB.Content = "Обед";
                        AnswerC.Content = "Ужин";
                        AnswerD.Content = "Полный день";
                        saver = "Около недели";
                        checker++;
                        break;
                    case 2:
                        Menu m = new Menu();
                        m.MenuItems.Items.Add(_context.Yums.All(y =>
                        yum.КалорииФулл >= calories && yum.КалорииФулл <= calories + 200
                        && (yum.Хранение == Convert.ToInt32(saver.Substring(0, 1)) || yum.Хранение == Convert.ToInt32(saver.Substring(1, 1)))));
                        m.Show();
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("ass"); }
        }
    }
}
