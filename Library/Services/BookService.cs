using Library.Data;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
	internal class BookService
	{
		public static void AddBook(LibraryContext context)
		{
			Console.Write("Titel: ");
			var title = Console.ReadLine();

			Console.Write("Författare: ");
			var author = Console.ReadLine();

			Console.Write("Utgivningsår: ");
			int year = int.Parse(Console.ReadLine());

			//Adds book
			context.Books.Add(new Book
			{
				Title = title,
				Author = author,
				PublishedYear = year,
				IsAvailable = true
			});

			context.SaveChanges();
			Console.WriteLine("✅ Bok registrerad");
		}

		public static void SearchBooks(LibraryContext context)
		{
			Console.Write("Sök titel eller författare: ");
			var search = Console.ReadLine();

			//Searches for user inputted books
			var books = context.Books
				.Where(b => b.Title.Contains(search) || b.Author.Contains(search))
				.ToList();

			if (!books.Any())
			{
				Console.WriteLine("❌ Inga böcker hittades");
				return;
			}

			foreach (var book in books)
			{
				Console.WriteLine(
					$"{book.BookId}: {book.Title} - {book.Author} " +
					$"({(book.IsAvailable ? "Tillgänglig" : "Utlånad")})");
			}
		}
	}
}
