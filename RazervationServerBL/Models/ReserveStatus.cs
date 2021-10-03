using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class ReserveStatus
    {
        public ReserveStatus()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
