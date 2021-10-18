﻿using System;
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

        // Login!!
        public User Login(string email, string pswd)//hi
        {
            User user = this.Users.Where(u => u.Email == email && u.UserPassword == pswd).FirstOrDefault();
         
            return user;
        }


        // Sign Up for Client

        public bool ClientSignUp(Client c, User u)
        {
            u.UserType = true;//the user is a client
            
            if(true)//all details had been filled - is needed?
            {
                this.Users.Add(u);
                this.Clients.Add(c);
                this.SaveChanges();
                return true;
            }
            else// one or more details are missing
            {
                return false;
            }
            
        }


        // a function that checks that the inserted email and user name are unique

        public bool CheckUniqueness(string email, string userName)
        {
            User user = this.Users.Where(u => u.Email == email || u.UserName == userName).FirstOrDefault();
            
            if(user == null)//the email and the user name are unique
            {
                return true;
            }
            else//one or both are not unique
            {
                return false;
            }
        }



        public string Test()
        {
            return "test";
        }
    }
}
