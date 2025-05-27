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
        /// <summary>
        /// Выгрузка данных о блюдах на форму
        /// </summary>
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
        /// <summary>
        /// Поиск блюд по введённому тексту
        /// </summary>
        /// <param name="sender">кнопка Найти</param>
        /// <param name="e">событие нажатия на кнопку</param>
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
        /// <summary>
        /// Переход на форму добавления блюда
        /// </summary>
        /// <param name="sender">кнопка круглая с плюсиком</param>
        /// <param name="e">событие нажатия на кнопку</param>
        private void AddClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Data.profile = context.Profiles.FirstOrDefault(p => p.UserId == context.Users.FirstOrDefault(u => u.Email == Data.email).Id);
            AddPage add = new();
            add.Show();
            this.Close();
        }
        /// <summary>
        /// Переход на профиль пользвателя
        /// </summary>
        /// <param name="sender">иконка аватарки</param>
        /// <param name="e">нажатие на аватарку</param>
        private void ProfileClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProfilePage profile = new();
            profile.Show();
            this.Close();
        }
        /// <summary>
        /// Отмена поиска
        /// </summary>
        /// <param name="sender">иконка крестика</param>
        /// <param name="e">нажатие на крестик</param>
        private void NoClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FindTb.Clear();
            LoadDBToListView();
        }
        /// <summary>
        /// Выбор блюда из списка
        /// </summary>
        /// <param name="sender">блочок с блюдом из списка</param>
        /// <param name="e">нажатие на элемент</param>
        private void DishChoosed(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Data.dish = (Dish)Disheslv.SelectedItem;
            DishInfoPage dishinfo = new();
            dishinfo.Show();
            this.Close();
        }
    }
}
