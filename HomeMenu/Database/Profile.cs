using System;
using System.Collections.Generic;

namespace HomeMenu.Database;

public partial class Profile
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int? UserId { get; set; }

    public string Photo { get; set; }

    public string Description { get; set; }

    public string Banner { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

    public virtual User User { get; set; }
}
