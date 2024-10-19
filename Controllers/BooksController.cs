using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeiChenMidTermTest.Data;
using MidTest.Models;
using MidTest.Utilities;
using MidTest.Services;
using System.Net;
using MidTest;

namespace LeiChenMidTermTest.Controllers
{
  public class BooksController : Controller
  {
    private readonly MidTerm8945274Context _context;
    private readonly IBook _librarian;

    public BooksController(MidTerm8945274Context context, IBook librarian)
    {
      _context = context;
      _librarian = librarian;
    }

    public void setCookies()
    {
      var userName = System.Environment.MachineName;
      Console.WriteLine("UserName: " + userName);
      HttpContext.Response.Cookies.Append("User", userName);
    }

    public void setCategoryList()
    {
      var categories = _context.Categories.ToListAsync().Result;
      categories.Sort((x, y) => string.Compare(y.Name, x.Name)); // Sort in descending order
      categories.Insert(0, new Category { Name = "Select an area", ID = -1 });
      var CategoryList = categories.Select(c => new SelectListItem { Value = c.Name, Text = c.Name }).ToList();
      ViewBag.CategoryList = CategoryList;
    }

    public async Task<IActionResult> FilterBooks(string filter)
    {
      Console.WriteLine("FilterBooks: " + filter);
      IEnumerable<Book> books;
      // All, Fiction, Bio, Modern, English, and French
      switch (filter)
      {
        case "Fiction":
          books = _librarian.AllFiction().Where(b => b.Genre == "Fiction" || b.Area == null);
          ViewBag.Title = "Fiction";
          break;
        case "Bio":
          books = _librarian.AllBios();
          ViewBag.Title = "Bio";
          break;
        case "Modern":
          books = _librarian.AllModernFiction();
          books = books.Where(b => b.Genre == "Fiction" && b.Area != "Novel" && b.Area != "Classics" && b.Area != "Poetry");
          ViewBag.Title = "Modern";
          break;
        case "English":
          books = _librarian.AllEnglishBooks();
          ViewBag.Title = "English";
          break;
        case "French":
          books = _librarian.AllFrenchBooks();
          ViewBag.Title = "French";
          break;
        default:
          books = _librarian.AllBooks();
          ViewBag.Title = "All Books";
          break;
      }
      return View("Index", books);
    }

    // GET: BooksController
    public async Task<IActionResult> Index(IEnumerable<Book> books)
    {
      setCookies();
      if (books == null || books.Count() == 0)
      {
        Console.WriteLine(123);
        return View(await _context.Books.ToListAsync());
      }
      Console.WriteLine(223);
      return View(books.ToList());
    }

    // GET: BooksController/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var book = await _context.Books
          .FirstOrDefaultAsync(m => m.PubID == id);
      if (book == null)
      {
        return NotFound();
      }

      return View(book);
    }

    // GET: BooksController/Create
    public IActionResult Create()
    {
      setCategoryList();
      return View();
    }

    // POST: BooksController/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create2([Bind("PubID,ISBN,Title,Author,Area")] Book book)
    {
      if (ModelState.IsValid)
      {
        _context.Add(book);
        await _context.SaveChangesAsync();

        HttpContext.Session.SetInt32("NumRecordsCreated", HttpContext.Session.GetInt32("NumRecordsCreated") ?? 0 + 1);
        return RedirectToAction(nameof(Index));
      }
      return View(book);
    }

    // GET: BooksController/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var book = await _context.Books.FindAsync(id);
      if (book == null)
      {
        return NotFound();
      }
      setCategoryList();
      return View(book);
    }

    // POST: BooksController/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit2(int id, [Bind("PubID,ISBN,Title,Author,Area")] Book book)
    {
      if (id != book.PubID)
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
          if (!BookExists(book.PubID))
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


    // POST: BooksController/Create new
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string ISBN, string Title, string Author, string Area)
    {
      if (!BookUtilities.ValidISBN(ISBN))
      {
        ModelState.AddModelError("ISBN", "Invalid ISBN format.");
        return View();
      }
      var book = new Book
      {
        ISBN = ISBN,
        Title = Title,
        Author = Author,
        Area = Area,

        CreatedBy = HttpContext.Request.Cookies["User"],
        CreatedOn = DateTime.Now,
      };

      if (ModelState.IsValid)
      {
        _context.Add(book);
        await _context.SaveChangesAsync();

        HttpContext.Session.SetInt32("NumRecordsCreated", HttpContext.Session.GetInt32("NumRecordsCreated") ?? 0 + 1);
        return RedirectToAction(nameof(Index));
      }
      return View(book);
    }

    // POST: BooksController/Edit new
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, string ISBN, string Title, string Author, string Area)
    {
      if (id != 0) // Check for missing ID
      {
        if (!BookUtilities.ValidISBN(ISBN))
        {
          ModelState.AddModelError("ISBN", "Invalid ISBN format.");
          return View();
        }
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
          return NotFound();
        }

        book.ISBN = ISBN;
        book.Title = Title;
        book.Author = Author;
        book.Area = Area;
        book.CreatedBy = HttpContext.Request.Cookies["User"];

        if (ModelState.IsValid)
        {
          try
          {
            _context.Update(book);
            await _context.SaveChangesAsync();
          }
          catch (DbUpdateConcurrencyException)
          {
            if (!BookExists(book.PubID))

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
      }
      return View();
    }

    // GET: BooksController/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var book = await _context.Books
          .FirstOrDefaultAsync(m => m.PubID == id);
      if (book == null)
      {
        return NotFound();
      }

      return View(book);
    }

    // POST: BooksController/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var book = await _context.Books.FindAsync(id);
      if (book != null)
      {
        _context.Books.Remove(book);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id)
    {
      return _context.Books.Any(e => e.PubID == id);
    }
  }
}
