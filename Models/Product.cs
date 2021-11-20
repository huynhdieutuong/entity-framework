using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ProductName", TypeName = "ntext")]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? CateId { get; set; } // Create CateId property, (int = not null) int? = null => Delete Rule: No Action
        [ForeignKey("CateId")] // Rename CategoryId to CateId
        [Required] // required = not null => Delete Rule: Cascade
        public virtual Category Category { get; set; } // Foreign key

        public void PrintInfo() => System.Console.WriteLine($"{ProductId} - {Name} - {Price} - {CateId}");
    }
}