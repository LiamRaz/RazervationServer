using RazervationServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazervationServer.DTO
{
    public class MainUserDTO
    {

        public User User { get; set; }

        public Client Client { get; set; }

        public Business Business { get; set; }

    }
}
