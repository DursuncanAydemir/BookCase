using BookCase.Core.Entities.Concrete;
using BookCase.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.DataAccess.Concrete.EntityFramework;

public class BookCaseContext: DbContext 
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(@"Server=DESKTOP-R3KR5K9;Database=BookCase;TrustServerCertificate=True");
        optionsBuilder.UseSqlServer("Server=DESKTOP-R3KR5K9;Database=BookCase;TrustServerCertificate=True;Trusted_Connection=true", option =>
        {
            option.EnableRetryOnFailure();
        }) ;
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Note> Notes { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<SubCategory> SubCategories { get; set; }

    public DbSet<OperationClaim> OperationClaims { get; set; }

    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
}
