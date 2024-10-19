using MidTest.Models;

namespace MidTest.Services
{
  public interface IBook
  {
    List<Book> AllBooks();
    List<Book> AllBios();
    List<Book> AllEnglishBooks();
    List<Book> AllFrenchBooks();
    List<Book> AllFiction();
    List<Book> AllModernFiction();
  }
}