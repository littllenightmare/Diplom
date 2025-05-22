using HomeMenu.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HomeMenu.Functions
{
    internal class Main
    {
        public static async Task<List<Dish>> Find(string finder)
        {
            HomeMenuContext context = new();
            return await context.Dishes.Where(d => d.CategoryNavigation.Name.Contains(finder) || d.Name.Contains(finder) || d.AuthorNavigation.Name.Contains(finder)).ToListAsync();
        }
        public static bool ValidateDish(string name, string photo, string calories, string proteins, string fats, string carbohydrates, string portions, string hours, string minutes,
            string recipe, System.Collections.IEnumerable ingridients)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите название блюда");
                return false;
            }
            if (photo == null)
            {
                MessageBox.Show("Вставьте изображение блюда");
                return false;
            }
            if (!double.TryParse(calories, out _) ||
                !double.TryParse(proteins, out _) ||
                !double.TryParse(fats, out _) ||
                !double.TryParse(carbohydrates, out _) ||
                !int.TryParse(portions, out _) ||
                !int.TryParse(hours, out _) ||
                !int.TryParse(minutes, out _))
            {
                MessageBox.Show("Проверьте числовые значения (калории, БЖУ, порции, время)");
                return false;
            }
            if (recipe == "")
            {
                MessageBox.Show("Поделитесь рецептом Вашего блюда");
                return false;
            }
            if (ingridients == null)
            {
                MessageBox.Show("Укажите ингредиенты Вашего блюда с граммами");
                return false;
            }
            return true;
        }
    }
}
