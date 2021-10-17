using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
 

namespace RazervationServerBL.Models
{
    public partial class RazervationDBContext: DbContext
    {

        public User Login(string email, string pswd)//hi
        {
            User user = this.Users.Where(u => u.Email == email && u.UserPassword == pswd).FirstOrDefault();
            

            return user;
        }

        public string Test()
        {
            return "test";
        }
    }
}
