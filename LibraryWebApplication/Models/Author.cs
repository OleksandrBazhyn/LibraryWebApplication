using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Author
{
    public string Id { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Birth { get; set; }

    public string? Citizenship { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
