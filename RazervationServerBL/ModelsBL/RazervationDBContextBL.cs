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

        // Login!!
        public User Login(string emailOrUName, string pswd)//hi
        {
            User user = this.Users.Where(u => (u.Email == emailOrUName || u.UserName == emailOrUName )&& u.UserPassword == pswd).FirstOrDefault();
         
            return user;
        }


        // Sign Up for Client

        public bool ClientSignUp(Client c, User u)
        {
            u.UserType = true;//the user is a client

            this.Users.Add(u);
            this.Clients.Add(c);
            this.SaveChanges();
            return true;

        }


        // Sign Up for Business

        public bool BusinessSignUp(Business b, User u)
        {
            u.UserType = false;//the user is a client

            this.Users.Add(u);
            this.Businesses.Add(b);
            this.SaveChanges();
            return true;

        }


        // a function that checks that the inserted email and user name are unique

        public bool CheckUniqueness(string email, string userName, string phoneNum)
        {
            User user = this.Users.Where(u => u.Email == email || u.UserName == userName || u.PhoneNumber == phoneNum).FirstOrDefault();
            
            if(user == null)//the email and the user name are unique
            {
                return true;
            }
            else//one or both are not unique
            {
                return false;
            }
        }



        // Change Reservation's Status!!!

        public bool ChangeReservationStatus(Reservation reservation, int statusId)
        {
            Reservation chosenReservation = this.Reservations.Where(r => r.ReservationId == reservation.ReservationId).FirstOrDefault();
            if(chosenReservation == null)
            {
                return false;
            }
            else 
            {
                TimeSpan timeDifference = chosenReservation.StartDateTime - DateTime.Now;
                if(timeDifference <= TimeSpan.FromHours(2))
                {
                    return false;
                }


                chosenReservation.Status = this.ReserveStatuses.Where(s => s.StatusId == statusId).FirstOrDefault();
                if(chosenReservation.Status == null)
                {
                    return false;
                }
                else
                {
                    this.SaveChanges();
                    return true;
                }
            }
        }


        // The Search Function

        public List<Business> Search(string searchInput)
        {
            List<Business> businesses = this.Businesses.Where(b => b.BusinessName.Contains(searchInput) || b.BusinessAddress.Contains(searchInput)
                                        || b.Category.CategoryName.Contains(searchInput) || b.Bio.Contains(searchInput)).ToList<Business>();
            return businesses;
            
        }


        // The Search By Category Function

        public List<Business> SearchByCategory(string strCategoryId)
        {
            int categoryId = int.Parse(strCategoryId);
            List<Business> businesses = this.Businesses.Where(b => b.CategoryId == categoryId).ToList<Business>();
            return businesses;

        }

        public string Test()
        {
            return "test";
        }
    }
}
