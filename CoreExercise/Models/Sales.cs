using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreExercise.Models
{
    public class Sales
    {
        public int SalesId { get; set; }
        [StringLength(20)]
        public string ProductId { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int SalesVolume { get; set; }
    }
}
