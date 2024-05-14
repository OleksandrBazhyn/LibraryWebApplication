using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApplication.Models;
using System.Net;

namespace LibraryWebApplication.Controllers
{
    public class BooksController : Controller
    {
        private readonly DbAndInformationSystemsContext _context;

        public BooksController(DbAndInformationSystemsContext context)
        {
            _context = context;
        }

        // GET: Books

        public async Task<IActionResult> Index()
        {
            return _context.Languages != null ?
                        View(await _context.Languages.ToListAsync()) :
                        Problem("Entity set 'DbAndInformationSystemsContext.Books'  is null.");
        }

        public async Task<IActionResult> GenreIndex(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Genres", "Index");
            // Found the books by genre
            ViewBag.GenreId = id;
            ViewBag.GenreName = name;
            // var bookByGenre = _context.Books.Where(b => b.Id == id.ToString()).Include(b => b.Genre);
            var bookByGenre = _context.Books.Where(b => b.Genre == id.ToString());
            
            return View(await bookByGenre.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> GenreDetails(string id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorNavigation)
                .Include(b => b.GenreNavigation)
                .Include(b => b.LanguageNavigation)
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
            ViewData["Author"] = new SelectList(_context.Authors, "Id", "Id");
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id");
            ViewData["Language"] = new SelectList(_context.Languages, "Id", "Id");
            return View();
        }

        public IActionResult GenreCreate(int genreId)
        {
            // ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre_");
            ViewBag.GenreId = genreId;
            ViewBag.GenreName = _context.Genres.Where(c => c.Id == genreId.ToString()).FirstOrDefault().Genre_;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenreCreate(int genreId, [Bind("Id,Isbn,Name,Author,PublicationYear,Language")] Book book)
        {
            book.Genre = genreId.ToString();
            if(ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Books", new {id = genreId, name = _context.Genres.Where(c => c.Id == genreId.ToString()).FirstOrDefault().Genre_});
            }
            // ViewBag["genreId"] = new SelectList(_context.Genres, "Id", "Name", );
            // return View(book);
            return RedirectToAction("Index", "Books", new {id = genreId, name = _context.Genres.Where(c => c.Id == genreId.ToString()).FirstOrDefault().Genre_});


            //if (ModelState.IsValid)
            //{
            //    _context.Add(book);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["Author"] = new SelectList(_context.Authors, "Id", "Id", book.Author);
            //ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id", book.Genre);
            //ViewData["Language"] = new SelectList(_context.Languages, "Id", "Id", book.Language);
            //return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["Author"] = new SelectList(_context.Authors, "Id", "Id", book.Author);
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id", book.Genre);
            ViewData["Language"] = new SelectList(_context.Languages, "Id", "Id", book.Language);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Isbn,Name,Author,Genre,PublicationYear,Language")] Book book)
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
            ViewData["Author"] = new SelectList(_context.Authors, "Id", "Id", book.Author);
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id", book.Genre);
            ViewData["Language"] = new SelectList(_context.Languages, "Id", "Id", book.Language);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorNavigation)
                .Include(b => b.GenreNavigation)
                .Include(b => b.LanguageNavigation)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'DbAndInformationSystemsContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
