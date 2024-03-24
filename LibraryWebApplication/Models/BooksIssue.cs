using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class BooksIssue
{
    [Key]
    [Display(Name = "#")]
    public string Id { get; set; } = null!;

    [Display(Name = "Бібліотека-позичувач")]
    public string? BorrowerLibraryId { get; set; }

    [Display(Name = "ID читача")]
    public string? ReaderId { get; set; }

    [Display(Name = "ID книжки")]
    public string? BooksId { get; set; }

    [Display(Name = "Коли позичено")]
    public DateTime? IssuedDate { get; set; }

    [Display(Name = "Чи взято додому")]
    public bool HomeTaken { get; set; }

    public virtual Book? Books { get; set; }

    public virtual Library? BorrowerLibrary { get; set; }

    public virtual Reader? Reader { get; set; }
}
