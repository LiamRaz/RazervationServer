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

            if(c != null && u != null)
            {
                u.UserType = true;//the user is a client
                this.Users.Add(u);
                this.Clients.Add(c);
                this.SaveChanges();
                return true;
            }
            return false;

        }


        // Sign Up for Business

        public bool BusinessSignUp(Business b, User u)
        {
            if (b != null && u != null)
            {
                u.UserType = false;//the user is a business
                this.Users.Add(u);
                this.Businesses.Add(b);
                this.SaveChanges();
                return true;
            }

            return false;
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
                if(statusId == 2)
                {
                    TimeSpan timeDifference = chosenReservation.StartDateTime - DateTime.Now;
                    if (timeDifference <= TimeSpan.FromHours(2))
                    {
                        return false;
                    }
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
                                        || b.Category.CategoryName.Contains(searchInput) || b.Bio.Contains(searchInput)).Include(b => b.Category).Include(b => b.Comments).ToList<Business>();
            return businesses;
            
        }


        // The Search By Category Function

        public List<Business> SearchByCategory(string strCategoryId)
        {
            int categoryId = int.Parse(strCategoryId);
            List<Business> businesses = this.Businesses.Where(b => b.CategoryId == categoryId).Include(b=> b.Category).Include(b => b.Comments).ToList<Business>();

            return businesses;

        }


        // Update Details For Client

        public bool UpdateClientDetails(string firstName, string lastName, string userName, string email, string password, string phoneNum, string gender, Client c, User u)
        {
            
            Client currentClient = this.Clients.Where(cl => cl.ClientId == c.ClientId).FirstOrDefault();
            User currentUser = this.Users.Where(us => us.UserName == u.UserName).FirstOrDefault();
            if (currentClient != null && currentUser != null)
            {
                currentClient.FirstName = firstName;
                currentClient.LastName = lastName;
                currentClient.UserName = userName;
                currentClient.Gender = gender;
                currentUser.Email = email;
                currentUser.UserPassword = password;
                currentUser.PhoneNumber = phoneNum;
                this.SaveChanges();
                return true;
            }
            return false;
        }


        // The Function That Deletes A Comment

        public bool DeleteComment(string strCommentId)
        {
            int commentId = int.Parse(strCommentId);
            Comment toDelete = this.Comments.Where(c => c.AutoCommentId == commentId).FirstOrDefault();
            if(toDelete != null)
            {
                toDelete.IsActive = false;
                this.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        // a function that creates a new favorite or activates one

        public bool AddFavorite(string strClientId, string strBusinessId)
        {
            try
            {
                int clientId = int.Parse(strClientId);
                int businessId = int.Parse(strBusinessId);

                Favorite existedFavorite = this.Favorites.Where(f => f.ClientId == clientId && f.BusinessId == businessId).FirstOrDefault();
                if (existedFavorite != null)
                {
                    existedFavorite.IsActive = true;
                }
                else
                {
                    Business chosenBusiness = this.Businesses.Where(b => b.BusinessId == businessId).FirstOrDefault();
                    Client chosenClient = this.Clients.Where(c => c.ClientId == clientId).FirstOrDefault();

                    if (chosenBusiness != null && chosenClient != null)
                    {
                        Favorite newFavorite = new Favorite
                        {
                            Business = chosenBusiness,
                            Client = chosenClient,
                            IsActive = true
                        };
                    }
                    else
                        return false;
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
           
                

        }


        // a function that deletes a favorite

        public bool DeleteFavorite(string strClientId, string strBusinessId)
        {
            try
            {
                int clientId = int.Parse(strClientId);
                int businessId = int.Parse(strBusinessId);

                Favorite favoriteToDelete = this.Favorites.Where(f => f.ClientId == clientId && f.BusinessId == businessId).FirstOrDefault();
                if (favoriteToDelete != null)
                {
                    favoriteToDelete.IsActive = false;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
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
