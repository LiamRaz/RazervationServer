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
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int DurationMin { get; set; }
        public int Price { get; set; }
        public int BusinessId { get; set; }

        public virtual Business Business { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
