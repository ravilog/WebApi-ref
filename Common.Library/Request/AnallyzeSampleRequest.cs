
namespace Common.Library
{
    using System.ComponentModel.DataAnnotations;

    public class AnalyserSampleRequest
    {
        [Required(ErrorMessage = "ProductSupplierId is required")]
        public int? ProductSupplierId { get; set; }

        [Required(ErrorMessage = "AnalysedComposition is required")]
        public int? AnalysedComposition { get; set; }

    }
}
