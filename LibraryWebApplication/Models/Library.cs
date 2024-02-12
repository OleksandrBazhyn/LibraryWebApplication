using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Library
{
    public string Id { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? WorkHours { get; set; }

    public virtual ICollection<BooksIssue> BooksIssues { get; } = new List<BooksIssue>();
}
