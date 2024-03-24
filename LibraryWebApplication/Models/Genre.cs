using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Genre
{
    [Key]
    [Display(Name = "#")]
    public string Id { get; set; } = null!;

    [Required(ErrorMessage ="Поле не повинно бути порожнім.")]
    [Display(Name="Жанр")]
    public string? Genre_ { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
