using Library.Data;
using Library.Models;
using Library.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
			//Uses Appsettings.json to get connectionstring
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var options = new DbContextOptionsBuilder<LibraryContext>()
				.UseSqlServer(config.GetConnectionString("DefaultConnection"))
				.Options;

			using var context = new LibraryContext(options);

			Menu.Show(context);
		}
	}
}
