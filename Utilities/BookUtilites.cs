namespace MidTest
{
  public static class BookUtilities
  {
    public static bool ValidISBN(string isbn)
    {
      if (string.IsNullOrEmpty(isbn))
      {
        return false;
      }
      isbn = isbn.Replace("-", "").Replace(" ", "");

      if (isbn.Length != 13 || !long.TryParse(isbn, out _))
      {
        return false;
      }

      if (isbn.Substring(0, 3) != "978" && isbn.Substring(0, 3) != "979")
      {
        return false;
      }

      int registrationGroup = int.Parse(isbn[3].ToString());
      if (registrationGroup != 0 && registrationGroup != 1 && registrationGroup != 2 && registrationGroup != 8)
      {
        return false;
      }

      int sum = 0;
      for (int i = 0; i < 12; i++)
      {
        int digit = int.Parse(isbn[i].ToString());
        sum += (i % 2 == 0) ? digit : digit * 3;
      }

      int remainder = sum % 10;
      int checkDigit = (remainder == 0) ? 0 : 10 - remainder;

      return checkDigit == int.Parse(isbn[12].ToString());
    }
  }
}