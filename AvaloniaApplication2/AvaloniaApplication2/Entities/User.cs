using System;
using System.Collections.Generic;

namespace AvaloniaApplication2.Entities;

public partial class User
{
    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Fname { get; set; }

    public string? Sname { get; set; }

    public string? Lname { get; set; }

    public int Roleid { get; set; }

    public int Id { get; set; }

    public bool Blocked { get; set; }

    public bool Firstlogin { get; set; }

    public virtual Role Role { get; set; } = null!;
}
