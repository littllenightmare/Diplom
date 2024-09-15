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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetPhrase();
        }

        private void TestClick(object sender, MouseButtonEventArgs e)
        {
            Test t = new Test();
            this.Close();
            t.Show();
        }

        private void ChooseClick(object sender, MouseButtonEventArgs e)
        {
            Menu m = new Menu();
            this.Close();
            m.Show();
        }
        private void SetPhrase()
        {
            string[] compliment = new string[6];
            compliment[0] = "Я не перестану тебя любить, если сегодня ты съешь на 500 калорий больше";
            compliment[1] = "Моё настроение поднимается, когда тебе вкусно";
            compliment[2] = "Еда, приготовленная вместе, вкуснее в 1000 раз";
            compliment[3] = "Готовить приятнее под любимую музыку";
            compliment[4] = "Во время еды разрешено восхищаться её вкусом";
            compliment[5] = "Нет плохой и хорошей еды, кушать можно всё";
            Random rnd = new Random();
            Fact.Text = Fact.Text + compliment[rnd.Next(compliment.Length)];
        }
        
    }
}
