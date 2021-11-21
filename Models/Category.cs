using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        // Collect Navigation
        public virtual List<Product> Products { get; set; }

        // Divide these properties (columns) to another table:
        // public int UserId { get; set; }
        // public DateTime Created { get; set; }
        // public DateTime Updated { get; set; }
        // public int CountProduct { get; set; }
        public CategoryDetail categoryDetail { get; set; } // Relative CategoryDetail
    }
}