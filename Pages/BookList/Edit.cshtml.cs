using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_BookListRazor.Model;

namespace WebApplication_BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;    
        }
        [BindProperty]
        public Book Book { get; set; }

        //cette fonction OnGet() permet d'indiquer(d'afficher) la pr�sence des donn�es(les Valeurs) Dans les Champs en ce qui Concerne L'Edition(Edit())  
        
        //OnGet() permet egalement de pouvoir recup�rer les Donn�es(les Valeurs)

        [HttpGet]
        public async Task OnGet(int id)
        {
           
            Book = await _db.Book.FindAsync(id);
        }

        //Book devrait etre passer en parametre pour pouvoir modifier les valeur venant directement de la DataBase(BookFromDb.___).  Mais l'Attribut [BindProperty] facilte la liaison
        public async Task<IActionResult> OnPost()
        {
            // pour effectuer une Modification il faut dabord r�cuperer le Livre en fonction de son Id au niveau de la base de donn�es
            
            // 1-on declarer une nouvelle variable qui recupera le livre cot� base de donn�es en fonction de L'Id car il doit etre Modifier
           
          /* La Modification champs par champs est effectuer lorsque vous devriez mettre a jour tout les champs du Livre*/

            //* Par Contre vous pouvez Utiliser _db.Book.Update() lorsque vous avez deux trois champs a Modifier*/
            var BookFromDb = await _db.Book.FindAsync(Book.Id);

            BookFromDb.Name = Book.Name;
            BookFromDb.Author = Book.Author;
            BookFromDb.ISBN = Book.ISBN;

            //une fois les Modifications effectuer On L'applique(on envoi vers) Cot� Base de Donn�es
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }

    }
}
