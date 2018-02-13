using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
  public class OrderItemViewModel
  {
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }

    [Required]
    public int ProductId { get; set; }

    /// <summary>
    /// automapper convention to fetch related product information
    /// add Product as the prefix to the property. the mapper automatically 
    /// maps it
    /// </summary>
    public string ProductCategory { get; set; }
    public string ProductSize { get; set; }
    public string ProductTitle { get; set; }
    public string ProductArtist { get; set; }
    public string ProductArtId { get; set; }
  }
}