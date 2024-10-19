using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MidTest.Models;

namespace LeiChenMidTermTest.Data
{
  public class MidTerm8945274Context : DbContext
  {
    public MidTerm8945274Context(DbContextOptions<MidTerm8945274Context> options)
        : base(options)
    {
    }

    public DbSet<MidTest.Models.Book> Books { get; set; } = default!;
    public DbSet<MidTest.Models.Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      /**
      Non-Fiction: "Autobiography", "Biography", "Business", "History", "Politics", "Science", "War"
      Fiction: "Adventure", "Classics", "Mystery", "Novel", "Poetry", "Plays", "Romance"
      */
      modelBuilder.Entity<Category>().HasData(
             //  Non-Fiction
             new Category { ID = 1, Name = "Autobiography" },
             new Category { ID = 2, Name = "Biography" },
             new Category { ID = 3, Name = "Business" },
             new Category { ID = 4, Name = "History" },
             new Category { ID = 5, Name = "Politics" },
             new Category { ID = 6, Name = "Science" },
             new Category { ID = 7, Name = "War" },
             // Fiction
             new Category { ID = 8, Name = "Adventure" },
             new Category { ID = 9, Name = "Classics" },
             new Category { ID = 10, Name = "Mystery" },
             new Category { ID = 11, Name = "Novel" },
             new Category { ID = 12, Name = "Poetry" },
             new Category { ID = 13, Name = "Plays" },
             new Category { ID = 14, Name = "Romance" }
         );
    }
  }
}
