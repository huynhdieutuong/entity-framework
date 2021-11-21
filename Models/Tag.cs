using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework
{
    [Table("Tag")]
    public class Tag
    {
        // 1. [Key]
        // [StringLength(20)]
        // public string TagId { get; set; }

        // 4. [Key]
        // public int NewTagId { get; set; }
        [Key]
        public int TagId { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }
    }
}