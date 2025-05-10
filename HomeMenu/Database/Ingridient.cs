﻿using System;
using System.Collections.Generic;

namespace HomeMenu.Database;

public partial class Ingridient
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double? Calories { get; set; }

    public double? Proteins { get; set; }

    public double? Fats { get; set; }

    public double? Carbohydrates { get; set; }

    public bool? Allergen { get; set; }
}
