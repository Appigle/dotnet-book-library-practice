using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MidTest.Models
{
  public class Book
  {
    [Key]
    public int PubID { get; set; }

    [Required]
    [StringLength(17, MinimumLength = 17)]
    [RegularExpression(@"^97[89]-\d-\d{5}-\d{3}-\d$")]
    public string ISBN { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public string? Area { get; set; }

    [NotMapped]
    public string Genre => Genere;

    private string Genere
    {
      get
      {
        // Determine genre based on Area
        if (Area == "Autobiography" || Area == "Biography" || Area == "Business" || Area == "History" || Area == "Politics" || Area == "Science" || Area == "War")
        {
          return "Non-Fiction";
        }
        else
        {
          return "Fiction";
        }
      }
    }
    [Required]
    [DisplayName("Added By")]
    public string CreatedBy { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [DisplayName("Created")]
    public DateTime CreatedOn { get; set; }
  }
}