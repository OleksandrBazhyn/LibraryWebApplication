using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Language
{
    public string Id { get; set; } = null!;

    public string? Language1 { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
