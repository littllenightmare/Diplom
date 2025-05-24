using System;
using System.Collections.Generic;

namespace HomeMenu.Database;

public partial class Payment
{
    public string Id { get; set; }

    public int? Amount { get; set; }

    public string Description { get; set; }

    public int? UserId { get; set; }

    public string SuccessUrl { get; set; }

    public string FailUrl { get; set; }

    public virtual User User { get; set; }
}
