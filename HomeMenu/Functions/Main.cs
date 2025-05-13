using HomeMenu.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMenu.Functions
{
    internal class Main
    {
        public static async Task<List<Dish>> Find(string finder)
        {
            HomeMenuContext context = new();
            return await context.Dishes.Where(d => d.CategoryNavigation.Name.Contains(finder) || d.Name.Contains(finder) || d.AuthorNavigation.Name.Contains(finder)).ToListAsync();
        }
    }
}
