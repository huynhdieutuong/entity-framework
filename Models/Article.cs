using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework
{
    [Table("Article")]
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}