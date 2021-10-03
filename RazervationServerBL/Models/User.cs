using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class User
    {
        public User()
        {
            Businesses = new HashSet<Business>();
            Clients = new HashSet<Client>();
        }

        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool UserType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
