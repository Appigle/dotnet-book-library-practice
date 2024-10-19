using LeiChenMidTermTest.Data;
using Microsoft.EntityFrameworkCore;
using MidTest.Models;

namespace MidTest.Services
{
  public class Librarian : IBook
  {
    private readonly MidTerm8945274Context _context;

    public Librarian(MidTerm8945274Context context)
    {
      _context = context;
    }

    public List<Book> AllBooks()
    {
      return _context.Books.ToList();
    }

    public List<Book> AllBios()
    {
      return _context.Books.Where(b => b.Area == "Autobiography" || b.Area == "Biography").ToList();
    }
    public List<Book> AllEnglishBooks()
    {
      return _context.Books.Where(b => b.ISBN.Substring(4, 1) == "0" || b.ISBN.Substring(4, 1) == "1").ToList();
    }

    public List<Book> AllFrenchBooks()
    {
      return _context.Books.Where(b => b.ISBN.Substring(4, 1) == "2" || b.ISBN.Substring(4, 1) == "8").ToList();
    }

    public List<Book> AllFiction()
    {
      return _context.Books.ToList();
    }

    public List<Book> AllModernFiction()
    {
      return _context.Books.ToList();
    }
  }
}