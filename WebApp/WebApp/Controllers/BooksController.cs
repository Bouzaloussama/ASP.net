using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly WebAppContext _context;


        public BooksController(WebAppContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }


       

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String Author,String Title, DateTime ReleaseDate,String Genre,int Price,String Adress, String email , String password)
        {

            Book book = new Book(Author, Title, Adress, ReleaseDate, Genre, Price);

            if (ModelState.IsValid && _context.User.Any(user => user.Email == email && user.Password == password ))
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,Title,ReleaseDate,Genre,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        /*/ GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/


        // Authontification
        [HttpPost]
        public async Task<IActionResult> Auth( String email , String password)
        {

            User user = _context.User.Single(u => u.Email.ToLower() == email.ToLower() && u.Password == password);

            if (user != null)
            {               
                Session["user"] = user.Email.ToString();
               
                return RedirectToAction(nameof(Profile));
            }

            return RedirectToAction(nameof(Index));

        }

        //profile
        public async Task<IActionResult> Profile()
        {
            if(Session["user"] != null)
             {
                 User user = (User)(Session["user"]);

                 return View(user);
             }
           
            return View();
        }

        // sign up
        public async Task<IActionResult> Deconnect()
        {
            Session.Remove["user"];

            return RedirectToAction(nameof(Profile));
        }
        // searsh
        public async Task<IActionResult> FindBook(String searsh)
        {
            return View(await _context.Book.Where(m => (m.Author + m.Title + m.Genre).ToLower().Contains(searsh.ToLower()) || searsh == null).ToListAsync());
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        private bool PanierTitleExists(String title)
        {
            return _context.Panier.Any(e => e.Title == title);
        }


        //+++++++++++++++++++++++  Panier  +++++++++++++++++++++++++++++++++++++++++

        // GET: Paniers
        public async Task<IActionResult> PanierIndex()
        {
            return View(await _context.Panier.ToListAsync());
        }

        // GET: Paniers/Details/5
        public async Task<IActionResult> PanierDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }


        // GET: Paniers/Create
        public async Task<IActionResult> AddPanier(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Book.FindAsync(id);
            Panier panier1 = new Panier(panier.Author, panier.Title, panier.Adress, panier.ReleaseDate, panier.Genre, panier.Price);
            if (panier1 == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (PanierTitleExists(panier.Title))
                {
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(panier1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }          
            return RedirectToAction(nameof(Index));
            
        }


        // GET: Paniers/Delete/5
        public async Task<IActionResult> PanierDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // POST: Paniers/Delete/5
        [HttpPost, ActionName("PanierDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PanierDeleteConfirmed(int id)
        {
            var panier = await _context.Panier.FindAsync(id);
            _context.Panier.Remove(panier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PanierIndex));
        }

        



       
    }
}
