using RazervationServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazervationServer.DTO
{
    public class AllLists
    {
        public List<Category> Categories { get; set; }

        public List<Business> Businesses { get; set; }
    }
}
