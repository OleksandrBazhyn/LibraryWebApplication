using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Genre
{
    [Key]
    [Display(Name = "#")]
    [Required(ErrorMessage = "Поле не повино бути порожнім")]
    public string Id { get; set; } = null!;

    [Display(Name="Жанр")]
    [Required(ErrorMessage = "Поле не повино бути порожнім")]
    public string? Genre_ { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
