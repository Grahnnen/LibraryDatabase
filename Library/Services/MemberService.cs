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
	internal class MemberService
	{
		public static void AddMember(LibraryContext context)
		{
			Console.Write("Förnamn: ");
			var firstName = Console.ReadLine();

			Console.Write("Efternamn: ");
			var lastName = Console.ReadLine();

			Console.Write("Email: ");
			var email = Console.ReadLine();

			//Checks if the email already exists
			if (context.Members.Any(m => m.Email == email))
			{
				Console.WriteLine("❌ Email används redan");
				return;
			}

			//Adds the a new member
			context.Members.Add(new Member
			{
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				CreatedDate = DateTime.Now
			});

			try
			{
				//Tries to save the new member to the database
				context.SaveChanges();
				Console.WriteLine("✅ Medlem registrerad");
			}
			catch (DbUpdateException)
			{
				//Failsafe: If duplicate was found clear the save "cache" so it doesnt crash
				Console.WriteLine("❌ En medlem med denna email finns redan.");
				context.ChangeTracker.Clear();
			}
		}
		public static void ShowAllMembers(LibraryContext context)
		{
			//gets all members ordered by lastname then firstname
			var members = context.Members
				.OrderBy(m => m.LastName)
				.ThenBy(m => m.FirstName)
				.ToList();

			//If no members
			if (!members.Any())
			{
				Console.WriteLine("❌ Inga medlemmar finns registrerade");
				return;
			}

			Console.WriteLine("\n📋 Medlemmar:");
			foreach (var member in members)
			{
				Console.WriteLine(
					$"{member.MemberId}: " +
					$"{member.FirstName} {member.LastName} " +
					$"({member.Email})");
			}
		}
	}
}
