using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Reader
{
    [Key]
    [Display(Name = "#")]
    [Required(ErrorMessage = "Поле не повино бути порожнім")]
    public string Id { get; set; } = null!;

    [Display(Name = "Номер телефону")]
    [Required(ErrorMessage = "Поле не повино бути порожнім")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Електрона пошта")]
    public string? Email { get; set; }

    [Display(Name = "Адреса")]
    public string? Address { get; set; }

    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Поле не повино бути порожнім")]
    public string? FirstName { get; set; }

    [Display(Name = "Прізвище")]
    public string? LastName { get; set; }

    public virtual ICollection<BooksIssue> BooksIssues { get; } = new List<BooksIssue>();
}
