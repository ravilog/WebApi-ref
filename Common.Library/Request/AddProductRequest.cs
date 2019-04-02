namespace Common.Library
{
    using System.ComponentModel.DataAnnotations;
    public class AddProductRequest
    {
        [Required(ErrorMessage = "ProductName is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public decimal? Quantity { get; set; }

        [Required(ErrorMessage = "Composition is required")]
        public int? Composition { get; set; }

    }
}
