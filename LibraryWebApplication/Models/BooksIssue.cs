using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class BooksIssue
{
    public string Id { get; set; } = null!;

    public string? BorrowerLibraryId { get; set; }

    public string? ReaderId { get; set; }

    public string? BooksId { get; set; }

    public DateTime? IssuedDate { get; set; }

    public bool HomeTaken { get; set; }

    public virtual Book? Books { get; set; }

    public virtual Library? BorrowerLibrary { get; set; }

    public virtual Reader? Reader { get; set; }
}
