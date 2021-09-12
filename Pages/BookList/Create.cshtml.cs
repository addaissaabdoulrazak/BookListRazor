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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public  CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //permet de relier la propri�t� alors Comme �a pas Besoin de pass� un Book en Parametre a notre Fonction OnPost()
        [BindProperty] 
        public Book Book { get; set; }
        public void OnGet()
        {
        }
        //on utilisera Task tant que dans le bloc de l'action Nous Avons " await "
        public async Task<IActionResult> OnPost()
        {
           if(ModelState.IsValid)
            {
                // 1- Avec cette Command Les donn�es ne sont pas ajouter a la  base de donnn�e Mais juste Ajouter a une File D'attente des elemnet qui seront pousser vers la database
                await _db.Book.AddAsync(Book);

                // 2- L'execution de la command A ce niveau Permettra la  transmission des Donn�es charg�(File D'attente) � la DataBase
                await _db.SaveChangesAsync();

                // 3- une fois les Modification effectuer Cot� DataBase returner vers la page D'index(L'element ajouter a la DaataBase devrait s'affichera)  
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
