using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Language
{
    [Key]
    [Display(Name = "#")]
    public string Id { get; set; } = null!;

    [Display(Name = "Мова")]
    public string? Language1 { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
