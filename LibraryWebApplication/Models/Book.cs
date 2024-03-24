using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Book
{
    [Key]
    [Display(Name = "#")]
    public string Id { get; set; } = null!;

    [Display(Name = "ISBN")]
    public string? Isbn { get; set; }

    [Display(Name = "Назва")]
    public string? Name { get; set; }

    [Display(Name = "Автор")]
    public string? Author { get; set; }

    [Display(Name = "Жанр")]
    public string? Genre { get; set; }

    [Display(Name = "Рік публікації")]
    public DateTime? PublicationYear { get; set; }

    [Display(Name = "Мова")]
    public string? Language { get; set; }

    public virtual Author? AuthorNavigation { get; set; }

    public virtual ICollection<BooksIssue> BooksIssues { get; } = new List<BooksIssue>();

    public virtual Genre? GenreNavigation { get; set; }

    public virtual Language? LanguageNavigation { get; set; }
}
