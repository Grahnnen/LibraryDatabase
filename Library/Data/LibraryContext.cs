using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Library.Data;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Member> Members { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			// Try to read connection string from appsettings.json in the project directory.
			// Directory.GetCurrentDirectory() works reliably when running CLI or VS.
			var basePath = Directory.GetCurrentDirectory();
			var config = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
				.Build();

			var connectionString = config.GetConnectionString("DefaultConnection");
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
			}

			optionsBuilder.UseSqlServer(connectionString);
		}
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C20761F4C997");

            entity.ToTable("Book");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loan__4F5AD45756372D6E");

            entity.ToTable("Loan");

            entity.HasIndex(e => e.BookId, "IX_Loan_BookId");

            entity.HasIndex(e => e.MemberId, "IX_Loan_MemberId");

            entity.HasIndex(e => e.ReturnDate, "IX_Loan_ReturnDate");

            entity.Property(e => e.LoanDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Book).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Book");

            entity.HasOne(d => d.Member).WithMany(p => p.Loans)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Member");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B1837CB6249");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ__Member__A9D105340B504BE3").IsUnique();

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
