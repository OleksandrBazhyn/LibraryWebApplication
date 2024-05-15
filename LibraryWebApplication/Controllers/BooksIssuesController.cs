using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApplication.Models;

namespace LibraryWebApplication.Controllers
{
    public class BooksIssuesController : Controller
    {
        private readonly DbAndInformationSystemsContext _context;

        public BooksIssuesController(DbAndInformationSystemsContext context)
        {
            _context = context;
        }

        // GET: BooksIssues
        public async Task<IActionResult> Index()
        {
            var dbAndInformationSystemsContext = _context.BooksIssues.Include(b => b.Books).Include(b => b.BorrowerLibrary).Include(b => b.Reader);
            return View(await dbAndInformationSystemsContext.ToListAsync());
        }

        public async Task<IActionResult> BookIndex(int? bookId, string? bookName)
        {
            if (bookId == null) return RedirectToAction("Index", "Books");
            // Found the books issues by book
            ViewBag.BookId = bookId;
            ViewBag.BookName = bookName;
            var booksIssuesByBook = _context.BooksIssues.Where(b => b.BooksId == bookId.ToString());

            return View(await booksIssuesByBook.ToListAsync());
        }

        // GET: BooksIssues/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.BooksIssues == null)
            {
                return NotFound();
            }

            var booksIssue = await _context.BooksIssues
                .Include(b => b.Books)
                .Include(b => b.BorrowerLibrary)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booksIssue == null)
            {
                return NotFound();
            }

            return View(booksIssue);
        }

        // GET: BooksIssues/Create
        public IActionResult Create()
        {
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["BorrowerLibraryId"] = new SelectList(_context.Libraries, "Id", "Id");
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Id");
            return View();
        }

        public IActionResult BookCreate(int bookId)
        {
            ViewBag.BookId = bookId;
            ViewBag.BookName = _context.Books.Where(c => c.Id == bookId.ToString()).FirstOrDefault();
            return View();
        }

        // POST: BooksIssues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowerLibraryId,ReaderId,BooksId,IssuedDate,HomeTaken")] BooksIssue booksIssue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booksIssue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Id", booksIssue.BooksId);
            ViewData["BorrowerLibraryId"] = new SelectList(_context.Libraries, "Id", "Id", booksIssue.BorrowerLibraryId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Id", booksIssue.ReaderId);
            return View(booksIssue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookCreate(int bookId, [Bind("Id,BorrowerLibraryId,ReaderId,IssuedDate,HomeTaken")] BooksIssue booksIssue)
        {
            booksIssue.BooksId = bookId.ToString();
            if (ModelState.IsValid)
            {
                _context.Add(booksIssue);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("BookIndex", "BooksIssues", new { id = bookId, name = _context.Books.Where(c => c.Id == bookId.ToString()).FirstOrDefault().Name });
            }
            // ViewBag["genreId"] = new SelectList(_context.Genres, "Id", "Name", );
            // return View(book);
            return RedirectToAction("BookIndex", "BooksIssues", new { id = bookId, name = _context.Books.Where(c => c.Id == bookId.ToString()).FirstOrDefault().Name });
        }

        // GET: BooksIssues/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BooksIssues == null)
            {
                return NotFound();
            }

            var booksIssue = await _context.BooksIssues.FindAsync(id);
            if (booksIssue == null)
            {
                return NotFound();
            }
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Id", booksIssue.BooksId);
            ViewData["BorrowerLibraryId"] = new SelectList(_context.Libraries, "Id", "Id", booksIssue.BorrowerLibraryId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Id", booksIssue.ReaderId);
            return View(booksIssue);
        }

        // POST: BooksIssues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,BorrowerLibraryId,ReaderId,BooksId,IssuedDate,HomeTaken")] BooksIssue booksIssue)
        {
            if (id != booksIssue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booksIssue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksIssueExists(booksIssue.Id))
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
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Id", booksIssue.BooksId);
            ViewData["BorrowerLibraryId"] = new SelectList(_context.Libraries, "Id", "Id", booksIssue.BorrowerLibraryId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Id", booksIssue.ReaderId);
            return View(booksIssue);
        }

        // GET: BooksIssues/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.BooksIssues == null)
            {
                return NotFound();
            }

            var booksIssue = await _context.BooksIssues
                .Include(b => b.Books)
                .Include(b => b.BorrowerLibrary)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booksIssue == null)
            {
                return NotFound();
            }

            return View(booksIssue);
        }

        // POST: BooksIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.BooksIssues == null)
            {
                return Problem("Entity set 'DbAndInformationSystemsContext.BooksIssues'  is null.");
            }
            var booksIssue = await _context.BooksIssues.FindAsync(id);
            if (booksIssue != null)
            {
                _context.BooksIssues.Remove(booksIssue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksIssueExists(string id)
        {
          return (_context.BooksIssues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
