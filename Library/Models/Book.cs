using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int PublishedYear { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
