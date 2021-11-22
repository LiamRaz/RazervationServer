using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Favorite
    {
        public int FavoriteId { get; set; }
        public int ClientId { get; set; }
        public int BusinessId { get; set; }
        public bool IsActive { get; set; }

        public virtual Business Business { get; set; }
        public virtual Client Client { get; set; }
    }
}
