using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Client
    {
        public Client()
        {
            Comments = new HashSet<Comment>();
            Favorites = new HashSet<Favorite>();
            Histories = new HashSet<History>();
            Reservations = new HashSet<Reservation>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }

        public virtual User UserNameNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
