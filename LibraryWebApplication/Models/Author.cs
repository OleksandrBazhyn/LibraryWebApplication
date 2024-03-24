using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Author
{
    [Key]
    [Display(Name = "#")]
    public string Id { get; set; } = null!;

    [Display(Name = "Ім'я")]
    public string? FirstName { get; set; }

    [Display(Name = "Прізвище")]
    public string? LastName { get; set; }

    [Display(Name = "Дата народження")]
    public DateTime? Birth { get; set; }

    [Display(Name = "Громадянство")]
    public string? Citizenship { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
