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

 //Upsert a la possiblité d'effectuer un création ou une Edition 
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }


        [HttpGet]

     //l'identifiant a la possiblité d'être nullable 
        public async Task<IActionResult> OnGet(int? id)
        {
            //
            Book = new Book();
            //
            if (id==null)
            {
                //Créer
                return Page();
            }
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(Book==null)
            {
                return NotFound();
            }
            return Page();

        }
    //pour ce qui est du gestion(post) on peut soumettre un formulaire de modification ou celui de la creation
        public async Task<IActionResult> OnPost()
        {
            if(Book.Id==null)
            {
                //soumission d'un formilaire de Création
                 _db.Book.Add(Book);
            }
            else
            {
                //soumission d'un formilaire de Modification
/************** Lorsque vous avez 1 ou 2 propriété a modifier vous pouvez appelez la methode Update **************/

                _db.Book.Update(Book);
            }
          
            //var BookFromDb = await _db.Book.FindAsync(Book.Id);
            //BookFromDb.Name = Book.Name;
            //BookFromDb.Author = Book.Author;
            //BookFromDb.ISBN = Book.ISBN;

            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }

    }
}
