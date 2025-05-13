using System;
using System.Collections.Generic;

namespace HomeMenu.Database;

public partial class Dish
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Photo { get; set; }

    public int? Time { get; set; }

    public int? Portion { get; set; }

    public int? Category { get; set; }

    public string Recipe { get; set; }

    public double? Calories { get; set; }

    public double? Proteins { get; set; }

    public double? Fats { get; set; }

    public double? Carbohydrates { get; set; }

    public int? Author { get; set; }

    public int? Difficult { get; set; }

    public string Ingridients { get; set; }

    public virtual Profile AuthorNavigation { get; set; }

    public virtual Category CategoryNavigation { get; set; }
}
