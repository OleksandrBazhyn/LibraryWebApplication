﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApplication.Models;

namespace LibraryWebApplication.Controllers
{
    public class ReadersController : Controller
    {
        private readonly DbAndInformationSystemsContext _context;

        public ReadersController(DbAndInformationSystemsContext context)
        {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index()
        {
              return _context.Readers != null ? 
                          View(await _context.Readers.ToListAsync()) :
                          Problem("Entity set 'DbAndInformationSystemsContext.Readers'  is null.");
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return RedirectToAction("ReaderIndex", "BooksIssues", new { id = reader.Id, lastname = reader.LastName, firstname = reader.FirstName });
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhoneNumber,Email,Address,FirstName,LastName")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PhoneNumber,Email,Address,FirstName,LastName")] Reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaderExists(reader.Id))
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
            return View(reader);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Readers == null)
            {
                return Problem("Entity set 'DbAndInformationSystemsContext.Readers'  is null.");
            }
            var reader = await _context.Readers.FindAsync(id);
            if (reader != null)
            {
                var dependedbooksissues = await _context.BooksIssues.Where(c => c.ReaderId == reader.Id).ToListAsync();
                _context.BooksIssues.RemoveRange(dependedbooksissues);
                _context.Readers.Remove(reader);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaderExists(string id)
        {
          return (_context.Readers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
