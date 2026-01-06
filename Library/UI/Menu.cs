using Library.Data;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UI
{
	internal class Menu
	{
		public static void Show(LibraryContext context)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Bibliotekssystem");
				Console.WriteLine("1. Registrera ny bok");
				Console.WriteLine("2. Registrera ny medlem");
				Console.WriteLine("3. Registrera lån");
				Console.WriteLine("4. Registrera återlämning");
				Console.WriteLine("5. Visa aktiva lån");
				Console.WriteLine("6. Sök efter bok");
				Console.WriteLine("7. Visa alla medlemmar");
				Console.WriteLine("0. Avsluta");

				switch (Console.ReadLine())
				{
					case "1": BookService.AddBook(context); break;
					case "2": MemberService.AddMember(context); break;
					case "3": LoanService.RegisterLoan(context); break;
					case "4": LoanService.RegisterReturn(context); break;
					case "5": LoanService.ShowActiveLoans(context); break;
					case "6": BookService.SearchBooks(context); break;
					case "7": MemberService.ShowAllMembers(context); break;
					case "0": return;
				}

				Console.WriteLine("\nTryck på valfri tangent...");
				Console.ReadKey();
			}
		}
	}
}
