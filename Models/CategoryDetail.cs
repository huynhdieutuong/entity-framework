using System;

namespace EntityFramework
{
    public class CategoryDetail
    {
        public int CategoryDetailId { get; set; } // to set Foreign Key
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int CountProduct { get; set; }
        public Category category { get; set; } // Relative Category
    }
}