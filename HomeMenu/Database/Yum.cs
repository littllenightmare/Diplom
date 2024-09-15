using System;
using System.Collections.Generic;

namespace HomeMenu.Database;

public partial class Yum
{
    public string Название { get; set; }

    public int? Порции { get; set; }

    public int? КалорииФулл { get; set; }

    public string ИнгридиентыФулл { get; set; }

    public int? БелкиФулл { get; set; }

    public int? ЖирыФулл { get; set; }

    public int? УглеводыФулл { get; set; }

    public int? Граммы { get; set; }

    public DateOnly? Давность { get; set; }

    public int? Хранение { get; set; }

    public string Рецепт { get; set; }

    public string Категория { get; set; }
}
