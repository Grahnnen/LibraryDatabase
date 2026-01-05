using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
	internal class LoanService
	{
		public static void RegisterLoan(LibraryContext context)
		{
			Console.Write("BookId: ");
			if (!int.TryParse(Console.ReadLine(), out int bookId))
			{
				Console.WriteLine("❌ Ogiltigt BookId");
				return;
			}
			Console.Write("MemberId: ");
			if (!int.TryParse(Console.ReadLine(), out int memberId))
			{
				Console.WriteLine("❌ Ogiltigt MemberId");
				return;
			}
			using var transaction = context.Database.BeginTransaction();

			var book = context.Books.SingleOrDefault(b => b.BookId == bookId);

			if (book == null || !book.IsAvailable)
			{
				transaction.Rollback();
				Console.WriteLine("❌ Boken är inte tillgänglig");
				return;
			}

			context.Loans.Add(new Loan
			{
				BookId = bookId,
				MemberId = memberId,
				LoanDate = DateTime.Now
			});

			book.IsAvailable = false;

			context.SaveChanges();
			transaction.Commit();

			Console.WriteLine("✅ Lån registrerat");
		}

		public static void RegisterReturn(LibraryContext context)
		{
			Console.Write("LoanId: ");
			if (!int.TryParse(Console.ReadLine(), out int loanId))
			{
				Console.WriteLine("❌ Ogiltigt LoanId");
				return;
			}
			//Gets the loan with user inputted ID, also checks if returndate is null for failsafe
			var loan = context.Loans
				.Include(l => l.Book)
				.SingleOrDefault(l => l.LoanId == loanId && l.ReturnDate == null);

			if (loan == null)
			{
				Console.WriteLine("❌ Aktivt lån hittades inte");
				return;
			}

			loan.ReturnDate = DateTime.Now;
			loan.Book.IsAvailable = true;

			context.SaveChanges();
			Console.WriteLine("✅ Bok återlämnad");
		}

		public static void ShowActiveLoans(LibraryContext context)
		{
			//Gets all loans
			var loans = context.Loans
				.Include(l => l.Book)
				.Include(l => l.Member)
				.Where(l => l.ReturnDate == null)
				.ToList();

			foreach (var loan in loans)
			{
				Console.WriteLine(
					$"{loan.LoanId}: {loan.Book.Title} → " +
					$"{loan.Member.FirstName} {loan.Member.LastName}");
			}
		}
	}
}
