using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
