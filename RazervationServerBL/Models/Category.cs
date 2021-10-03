using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Category
    {
        public Category()
        {
            Businesses = new HashSet<Business>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
    }
}
