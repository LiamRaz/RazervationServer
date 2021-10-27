using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Business
    {
        public Business()
        {
            Bservices = new HashSet<Bservice>();
            BusinessDays = new HashSet<BusinessDay>();
            Comments = new HashSet<Comment>();
            Favorites = new HashSet<Favorite>();
            Histories = new HashSet<History>();
            Reservations = new HashSet<Reservation>();
            SpecialNumberOfWorkers = new HashSet<SpecialNumberOfWorker>();
        }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string Bio { get; set; }
        public int CategoryId { get; set; }
        public string InternetUrl { get; set; }
        public string UserName { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }

        public virtual Category Category { get; set; }
        public virtual User UserNameNavigation { get; set; }
        public virtual ICollection<Bservice> Bservices { get; set; }
        public virtual ICollection<BusinessDay> BusinessDays { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<SpecialNumberOfWorker> SpecialNumberOfWorkers { get; set; }
    }
}
