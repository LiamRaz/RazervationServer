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

        public User Login(string email, string pswd)
        {
            User user = this.Users.Where(u => u.Email == email && u.UserPassword == pswd).FirstOrDefault();
            if (user != null)
            {
                if (user.UserType == true)//client
                {
                    Client client = this.Clients.Where(c => c.UserName == user.UserName).Include(cl => cl.)
                        .FirstOrDefault();
                    MainUserDTO
                }
                else//business
                {

                }
            }
            return user;
        }

        public string Test()
        {
            return "test";
        }
    }
}
