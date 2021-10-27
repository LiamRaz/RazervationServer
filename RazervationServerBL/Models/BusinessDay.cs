using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class BusinessDay
    {
        public BusinessDay()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int BusinessId { get; set; }
        public int DayNum { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumberOfWorkers { get; set; }
        public int DayId { get; set; }

        public virtual Business Business { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
