using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework
{
    public class ArticleTag
    {
        [Key]
        public int ArticleTagId { get; set; }

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }

        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
    }
}