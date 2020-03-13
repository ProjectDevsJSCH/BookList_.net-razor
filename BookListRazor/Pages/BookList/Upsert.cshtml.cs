using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty] /*permite usar la variable en el onGet, onPost, etc*/
        public Book Book_element { get; set; }

        public async Task<ActionResult> OnGet(int? id)
        {
            Book_element = new Book();
            if(id == null)
            {
                return Page();
            }

            Book_element = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(Book_element == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book_element.Id == 0)
                {
                    _db.Book.Add(Book_element);
                }
                else
                {
                    _db.Book.Update(Book_element);
                }

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage(); /*Recarga la página*/
        }
    }
}