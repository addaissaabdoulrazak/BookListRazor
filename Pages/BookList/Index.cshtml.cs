using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication_BookListRazor.Model;

namespace WebApplication_BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //constructeur
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;     
        }
       
        // variable liste
        public IEnumerable<Book> Books;

        public async Task OnGet()
        {
            //récupération d'une Liste des données au niveau de la database.
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Book = await _db.Book.FindAsync(id);
          //A chaque fois que nous recuperons Des Données au niveau de la DataBase il faut effectuer une Verification
          if( Book==null)
          {
                return NotFound();
          }
          else
          {
                _db.Book.Remove(Book);

                //await _db.Book.SaveChange

                // 1- un livre ne se charge pas ce qui se charge est plutot la dataBase

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
          }
        }
    }
}
