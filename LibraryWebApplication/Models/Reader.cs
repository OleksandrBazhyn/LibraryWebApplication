using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Reader
{
    [Key]
    [Display(Name = "#")]
    public string Id { get; set; } = null!;

    [Display(Name = "Номер телефону")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Електрона пошта")]
    public string? Email { get; set; }

    [Display(Name = "Адреса")]
    public string? Address { get; set; }

    [Display(Name = "Ім'я")]
    public string? FirstName { get; set; }

    [Display(Name = "Прізвище")]
    public string? LastName { get; set; }

    public virtual ICollection<BooksIssue> BooksIssues { get; } = new List<BooksIssue>();
}
