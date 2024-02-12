using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Book
{
    public string Id { get; set; } = null!;

    public string? Isbn { get; set; }

    public string? Name { get; set; }

    public string? Author { get; set; }

    public string? Genre { get; set; }

    public DateTime? PublicationYear { get; set; }

    public string? Language { get; set; }

    public virtual Author? AuthorNavigation { get; set; }

    public virtual ICollection<BooksIssue> BooksIssues { get; } = new List<BooksIssue>();

    public virtual Genre? GenreNavigation { get; set; }

    public virtual Language? LanguageNavigation { get; set; }
}
