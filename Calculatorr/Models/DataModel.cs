using System.ComponentModel.DataAnnotations;

namespace Calculatorr.Models
{
    public class DataModel
    {
        [Required]
        [Range(1, 5_000_000)]
        public double LoanAmount { get; set; }
        [Required]
        [Range(1, 60)]
        public int LoanTerm { get; set; }
        [Required]
        public double LoanRate { get; set; }
    }
}
