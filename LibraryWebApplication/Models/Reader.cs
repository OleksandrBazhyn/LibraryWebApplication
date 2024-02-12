using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Reader
{
    public string Id { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<BooksIssue> BooksIssues { get; } = new List<BooksIssue>();
}
