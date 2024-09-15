using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для HelperCalories.xaml
    /// </summary>
    public partial class HelperCalories : Window
    {
        public HelperCalories()
        {
            InitializeComponent();
            Sex.Items.Add("Ж");
            Sex.Items.Add("М");
        }

        private void CaloriesClick(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(Weighttb.Text, out int weight);
            Int32.TryParse(Agetb.Text, out int age);
            Int32.TryParse(Heighttb.Text, out int height);
            if (weight >= 10 & weight< 645)
            {
                if (height>= 60 & height< 251)
                {
                    if (age>= 5 & age< 125)
                    {
                        if (Sex.SelectedItem == "Ж")
                        {
                            GetCalories(weight, height, age, "Ж");
                            this.Close();
                        }
                        else if (Sex.SelectedItem == "М")
                        {
                            GetCalories(weight, height, age, "М");
                            this.Close();
                        }
                        else MessageBox.Show("Ты транс?");
                    }
                    else MessageBox.Show("Ты вампир?");
                }
                else MessageBox.Show("Ты лилипут?");
            }
            else MessageBox.Show("Ты мегалодон?");
        }
        public static void GetCalories(int weight, int height, int age, string sex)
        {
            if (sex == "Ж")
            {
                Test t = new Test();
                t.Calories.Text = Convert.ToString(Convert.ToInt32((height * 1.8) - (age * 4.7) + (weight * 9.6) + 655));
                t.Calories.Focus();
                t.Show();
            }
            else if (sex == "М")
            {
                Test t = new Test();
                t.Calories.Text = Convert.ToString(Convert.ToInt32((height * 5) - (age * 6.8) + (weight * 13.7) + 66));
                t.Calories.Focus();
                t.Show();
            }
        }
    }
}
