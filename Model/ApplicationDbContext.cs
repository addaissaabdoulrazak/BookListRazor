using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_BookListRazor.Model
{
    public class ApplicationDbContext : DbContext
    {
        //constructeur
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {

        }
        //propriété
         public DbSet<Book> Book { get; set; }

    }
}
