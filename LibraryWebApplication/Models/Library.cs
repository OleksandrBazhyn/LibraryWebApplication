using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models;

public partial class Library
{
    [Key]
    [Display(Name = "#")]
    public string Id { get; set; } = null!;

    [Display(Name = "Адреса")]
    public string? Address { get; set; }

    [Display(Name = "Номер телефону")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Робочі години")]
    public string? WorkHours { get; set; }

    public virtual ICollection<BooksIssue> BooksIssues { get; } = new List<BooksIssue>();
}
