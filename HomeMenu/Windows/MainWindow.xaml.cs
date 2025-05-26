using HomeMenu.Database;
using System.Linq;
using System.Windows;
using HomeMenu.Windows;
using HomeMenu.Functions;

namespace HomeMenu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomeMenuContext context = new();
        public MainWindow()
        {
            InitializeComponent();
            LoadDBToListView();
        }

        void LoadDBToListView()
        {
            using (HomeMenuContext context = new())
            {
                int SelectedIndex = Disheslv.SelectedIndex;
                Disheslv.ItemsSource = context.Dishes.ToList();
                if (SelectedIndex != -1)
                {
                    if (SelectedIndex == Disheslv.Items.Count) SelectedIndex--;
                    Disheslv.SelectedIndex = SelectedIndex;
                    Disheslv.ScrollIntoView(Disheslv.SelectedItem);
                }
                Disheslv.Focus();
            }
        }

        private async void FindClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var finder = await Main.Find(FindTb.Text);
                Disheslv.ItemsSource = finder;
                if (finder.Count == 0)
                {
                    MessageBox.Show("Результатов не найдено. Попробуйте другой запрос.");
                }
            }
            catch
            {

            }
        }

        private void AddClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Data.profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
            AddPage add = new();
            this.Close();
        }

        private void ProfileClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProfilePage profile = new();
            profile.Show();
            this.Close();
        }

        private void NoClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FindTb.Clear();
            LoadDBToListView();
        }

        private void DishChoosed(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Data.dish = (Dish)Disheslv.SelectedItem;
            DishInfoPage dishinfo = new();
            dishinfo.Show();
            this.Close();
        }
    }
}
