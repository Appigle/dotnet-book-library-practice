namespace MidTest.Utilities
{
  public static class StringUtilities
  {
    public static string ShortBookName(string bookName)
    {
      if (string.IsNullOrEmpty(bookName))
      {
        return string.Empty;
      }

      return bookName.Substring(0, Math.Min(bookName.Length, 10));
    }

    public static string ShortAuthorName(string authorName)
    {
      if (string.IsNullOrEmpty(authorName))
      {
        return string.Empty;
      }

      var words = authorName.Split(' ');
      var initials = words.Take(2).Select(w => w.Substring(0, 1).ToUpper()).ToArray();
      var lastName = words.Last().ToUpper();

      return string.Join(". ", initials) + " " + lastName;
    }
  }
}