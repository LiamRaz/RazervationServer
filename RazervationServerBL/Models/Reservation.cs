using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Reservation
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndTime { get; set; }
        public int BusinessId { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public int ReservationId { get; set; }
        public int DayId { get; set; }
        public int StatusId { get; set; }
        public DateTime DateOfCreation { get; set; }

        public virtual Business Business { get; set; }
        public virtual Client Client { get; set; }
        public virtual BusinessDay Day { get; set; }
        public virtual Bservice Service { get; set; }
        public virtual ReserveStatus Status { get; set; }
    }
}
