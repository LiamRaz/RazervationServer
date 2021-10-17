using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Bservice
    {
        public Bservice()
        {
            Reservations = new HashSet<Reservation>();
            ServicesInBusinesses = new HashSet<ServicesInBusiness>();
        }

        public int ServiceId { get; set; }
        public int ServiceName { get; set; }
        public int DurationMin { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<ServicesInBusiness> ServicesInBusinesses { get; set; }
    }
}
