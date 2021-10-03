using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class ServicesInBusiness
    {
        public int BusinessId { get; set; }
        public int ServiceId { get; set; }

        public virtual Business Business { get; set; }
        public virtual Bservice Service { get; set; }
    }
}
