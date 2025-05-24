using System;
using System.Collections.Generic;

namespace HomeMenu.Database;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int? Role { get; set; }

    public int? Code { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual Role RoleNavigation { get; set; }
}
