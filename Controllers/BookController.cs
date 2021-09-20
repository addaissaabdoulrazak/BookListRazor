using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_BookListRazor.Model;

namespace WebApplication_BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        //constructeur
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Book.ToList() } );
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //recuperer un livre de la base de données en function de l'identifiant
            var BookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);

            //Chaque fois que vous effectuer un acces a la database pour une Modification il est capital d'effectuer un Test

            if (BookFromDb == null)
            {
                return Json(new { success =false, Message= "Impossible de supprimer un livre qui n'existe pas dans la base de données" });
            }
            _db.Book.Remove(BookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "suppression effectuer avec succès" });
        }
    }
}
