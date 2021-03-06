using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework
{
    // 1. [Table("Product")]
    public class Product
    {
        // 2. [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ProductName", TypeName = "ntext")]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? CateId { get; set; } // Create CateId property, (int = not null) int? = null => Delete Rule: No Action
        // 4. [ForeignKey("CateId")] // Rename CategoryId to CateId
        // 5. [Required] // required = not null => Delete Rule: Cascade
        public virtual Category Category { get; set; } // Foreign key

        // To create one product can belong to two categories
        public int? CateId2 { get; set; }
        // [ForeignKey("CateId2")]
        // 7. [InverseProperty("Products")] // to Category know, use this FK
        public virtual Category Category2 { get; set; } // The 2nd Foreign key

        public void PrintInfo() => System.Console.WriteLine($"{ProductId} - {Name} - {Price} - {CateId}");
    }
}