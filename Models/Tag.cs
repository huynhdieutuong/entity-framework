using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework
{
    [Table("Tag")]
    public class Tag
    {
        [Key]
        [StringLength(20)]
        public string TagId { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }
    }
}