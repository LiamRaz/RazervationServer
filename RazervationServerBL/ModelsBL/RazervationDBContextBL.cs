using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace RazervationServerBL.Models
{
    public partial class RazervationDBContext : DbContext
    {

        // Login!!
        public User Login(string emailOrUName, string pswd)//hi
        {
            User user = this.Users.Where(u => (u.Email == emailOrUName || u.UserName == emailOrUName) && u.UserPassword == pswd).FirstOrDefault();

            return user;
        }


        // Sign Up for Client

        public bool ClientSignUp(Client c, User u)
        {

            if (c != null && u != null)
            {
                u.UserType = true;//the user is a client
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
                                   //  this.Users.Update(u);
                this.Businesses.Add(b);
                //this.Entry(b.Category).State = EntityState.Unchanged;
                this.SaveChanges();
                return true;
            }

            return false;
        }


        // a function that checks that the inserted email and user name are unique

        public bool CheckUniqueness(string email, string userName, string phoneNum)
        {
            User user = this.Users.Where(u => u.Email == email || u.UserName == userName || u.PhoneNumber == phoneNum).FirstOrDefault();

            if (user == null)//the email and the user name are unique
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
            if (chosenReservation == null)
            {
                return false;
            }
            else
            {
                if (statusId == 2)
                {
                    TimeSpan timeDifference = chosenReservation.StartDateTime - DateTime.Now;
                    if (timeDifference <= TimeSpan.FromHours(2))
                    {
                        return false;
                    }
                }


                chosenReservation.Status = this.ReserveStatuses.Where(s => s.StatusId == statusId).FirstOrDefault();
                if (chosenReservation.Status == null)
                {
                    return false;
                }
                else
                {
                    chosenReservation.StatusId = statusId;
                    this.Reservations.Update(chosenReservation);
                    this.SaveChanges();
                    return true;
                }
            }
        }


        // The Search Function

        public List<Business> Search(string searchInput)
        {
            List<Business> businesses = this.Businesses.Where(b => b.BusinessName.Contains(searchInput) || b.BusinessAddress.Contains(searchInput)
                                        || b.Category.CategoryName.Contains(searchInput) || b.Bio.Contains(searchInput))
                        .Include(b => b.UserNameNavigation).Include(b => b.Category)
                        .Include(b => b.BusinessDays)
                        .Include(b => b.Comments).ThenInclude(b => b.Client)
                        .Include(b => b.Favorites)
                        .Include(b => b.Histories)
                        .Include(b => b.Reservations)
                        .Include(b => b.SpecialNumberOfWorkers)
                        .Include(b => b.Bservices)
                        .ToList<Business>();


            return businesses;

        }


        // The Search By Category Function

        public List<Business> SearchByCategory(string strCategoryId)
        {
            int categoryId = int.Parse(strCategoryId);
            List<Business> businesses = this.Businesses.Where(b => b.CategoryId == categoryId).Include(b => b.Category)
                .Include(b => b.UserNameNavigation)
                        .Include(b => b.BusinessDays)
                        .Include(b => b.Comments).ThenInclude(b => b.Client)
                        .Include(b => b.Favorites)
                        .Include(b => b.Histories)
                        .Include(b => b.Reservations)
                        .Include(b => b.SpecialNumberOfWorkers)
                        .Include(b => b.Bservices)
                        .ToList<Business>();

            return businesses;

        }


        // Update Details For Client

        public bool UpdateClientDetails(string firstName, string lastName, string userName, string email, string password, string phoneNum, string gender, Client c, User u)
        {

            Client currentClient = this.Clients.Where(cl => cl.ClientId == c.ClientId).FirstOrDefault();
            User currentUser = this.Users.Where(us => us.UserName == currentClient.UserName).FirstOrDefault();
            if (currentClient != null && currentUser != null)
            {
                currentClient.FirstName = firstName;
                currentClient.LastName = lastName;
                currentClient.UserName = userName;
                currentClient.Gender = gender;
                currentUser.Email = email;
                currentUser.UserPassword = password;
                currentUser.PhoneNumber = phoneNum;
                //currentUser.UserName = u.UserName;
                //this.Clients.Update(currentClient);
                //this.Users.Update(currentUser);
                this.Entry(currentClient).State = EntityState.Modified;
                this.Entry(currentUser).State = EntityState.Modified;
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
            if (toDelete != null)
            {
                toDelete.IsActive = false;
                this.Comments.Update(toDelete);
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
                    this.Favorites.Update(existedFavorite);
                    this.SaveChanges();
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
                        this.Entry(newFavorite).State = EntityState.Added;
                        //this.Entry(newFavorite.Business).State = EntityState.Unchanged;
                        //this.Entry(newFavorite.Client).State = EntityState.Unchanged;
                        this.SaveChanges();
                    }
                    else
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }



        }


        // a function that deletes a favorite

        public bool DeleteFavorite(int clientId, int businessId)
        {
            try
            {
                Favorite favoriteToDelete = this.Favorites.Where(f => f.ClientId == clientId && f.BusinessId == businessId).FirstOrDefault();
                if (favoriteToDelete != null)
                {
                    favoriteToDelete.IsActive = false;
                    this.Favorites.Update(favoriteToDelete);
                    this.SaveChanges();
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


        // a function that creates a comment

        public bool AddComment(int clientId, int businessId, int rating, string commentText, DateTime cDate)
        {
            Comment existedComment = this.Comments.Where(c => c.ClientId == clientId && c.BusinessId == businessId).FirstOrDefault();
            if (existedComment != null && existedComment.IsActive)
                return false;

            Client client = this.Clients.Where(c => c.ClientId == clientId).FirstOrDefault();
            Business business = this.Businesses.Where(b => b.BusinessId == businessId).FirstOrDefault();
            if (business != null && client != null)
            {
                Comment newComment = new Comment
                {
                    Business = business,
                    Client = client,
                    Rating = rating,
                    CommentText = commentText,
                    Cdate = cDate,
                    IsActive = true
                };

                this.Entry(newComment).State = EntityState.Added;
                //this.Entry(newComment.Business).State = EntityState.Unchanged;
                //this.Entry(newComment.Client).State = EntityState.Unchanged;
                this.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }


        // get reservations

        public List<Reservation> GetReservations(string businessIdStr, string statusIdStr, string dateStr)
        {
            int businessId = int.Parse(businessIdStr);
            int statusId = int.Parse(statusIdStr);
            DateTime date = DateTime.ParseExact(dateStr, "dd/MM/yyyy", null);

            return this.Reservations.Where(r => r.BusinessId == businessId && r.StatusId == statusId && DateTime.Compare(r.StartDateTime.Date, date.Date) == 0).ToList<Reservation>();
        }

        // get status

        public ReserveStatus GetReserveStatus(string statusIdStr)
        {
            int statusId = int.Parse(statusIdStr);
            ReserveStatus status = this.ReserveStatuses.Where(s => s.StatusId == statusId).FirstOrDefault();
            return status;

        }


        // add reservation

        public bool AddReservation(Reservation reservation)
        {
            //check if there is an empty slot for the reservation

            int numberOfWorkers = reservation.Day.NumberOfWorkers;
            SpecialNumberOfWorker specialNumberOfWorkers = reservation.Business.SpecialNumberOfWorkers.Where(s => s.SpecialDate.ToString("dd/MM/yyyy") == reservation.StartDateTime.ToString("dd/MM/yyyy")).FirstOrDefault();

            if (specialNumberOfWorkers != null)
            {
                numberOfWorkers = specialNumberOfWorkers.NumWorkers;
            }

            List<Reservation> existedReservations = this.GetReservations(reservation.BusinessId.ToString(), "1", reservation.StartDateTime.ToString("dd/MM/yyyy"));

            foreach (Reservation existedResrvation in existedReservations)
            {

                if ((DateTime.Compare(existedResrvation.StartDateTime, reservation.StartDateTime) >= 0 && DateTime.Compare(existedResrvation.StartDateTime, reservation.EndTime) < 0) || (DateTime.Compare(existedResrvation.EndTime, reservation.StartDateTime) > 0 && DateTime.Compare(existedResrvation.EndTime, reservation.EndTime) <= 0))
                {
                    numberOfWorkers--;
                }

            }

            if (numberOfWorkers <= 0)
                return false;

            // check if the client already has a reservation for this time

            List<Reservation> clientReservations = GetReservations(reservation.ClientId, reservation.StatusId, reservation.StartDateTime);

            foreach (Reservation clientReservation in clientReservations)
            {

                if ((DateTime.Compare(clientReservation.StartDateTime, reservation.StartDateTime) >= 0 && DateTime.Compare(clientReservation.StartDateTime, reservation.EndTime) < 0) || (DateTime.Compare(clientReservation.EndTime, reservation.StartDateTime) > 0 && DateTime.Compare(clientReservation.EndTime, reservation.EndTime) <= 0))
                {
                    return false;
                }

            }


            //this.Reservations.Add(reservation);
            this.Entry(reservation).State = EntityState.Added;

            //this.Entry(reservation.Business).State = EntityState.Unchanged;
            //this.Entry(reservation.Client).State = EntityState.Unchanged;
            //this.Entry(reservation.Day).State = EntityState.Unchanged;
            //this.Entry(reservation.Status).State = EntityState.Unchanged;
            //this.Entry(reservation.Service).State = EntityState.Unchanged;
            this.SaveChanges();

            return true;

        }

        // a function that gets a client reservations for a specific date

        public List<Reservation> GetReservations(int clientId, int statusId, DateTime date)
        {
            return this.Reservations.Where(r => r.ClientId == clientId && r.StatusId == statusId && DateTime.Compare(r.StartDateTime.Date, date.Date) == 0).ToList<Reservation>();
        }

        //a function that adds a service

        public bool AddService(Bservice service)
        {
            if (service != null)
            {
                this.Entry(service).State = EntityState.Added;
                this.SaveChanges();
                return true;
            }
            return false;
        }


        // a function that deletes a service

        public bool DeleteService(Bservice service)
        {
            if (service != null)
            {
                Bservice toDelete = this.Bservices.Where(s => s.ServiceId == service.ServiceId).FirstOrDefault();
                if(toDelete != null)
                {
                    toDelete.IsActive = false;
                    this.Bservices.Update(toDelete);
                    this.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
                
        }


        //a function that adds a Special number of workers

        public bool AddSpecialNumberOfWorkers(SpecialNumberOfWorker specialNumberOfWorker)
        {
            if (specialNumberOfWorker != null)
            {
                this.Entry(specialNumberOfWorker).State = EntityState.Added;
                this.SaveChanges();
                return true;
            }
            return false;
        }

        public int GetNumberOfWorkers(string dateStr, string businessIdStr)
        {
            int numberOfWorkers = -1;
            DateTime date = DateTime.ParseExact(dateStr, "dd/MM/yyyy", null);
            int businessId = int.Parse(businessIdStr);

            int dayNum = ((int)date.DayOfWeek) + 1;
            BusinessDay chosenBusinessDay = this.BusinessDays.Where(bd => (bd.DayNum == dayNum && bd.BusinessId == businessId)).FirstOrDefault();

            if(chosenBusinessDay == null)
            {
                return numberOfWorkers;
            }

            numberOfWorkers = chosenBusinessDay.NumberOfWorkers;
            SpecialNumberOfWorker specialNumberOfWorkers = this.SpecialNumberOfWorkers.Where(s => (s.SpecialDate.Date == date.Date && s.BusinessId == businessId)).FirstOrDefault();

            if (specialNumberOfWorkers != null)
            {
                numberOfWorkers = specialNumberOfWorkers.NumWorkers;
            }

            return numberOfWorkers;



        }


        public bool UpdateBusinessDays(List<BusinessDay> businessDays)
        {
            if (businessDays == null)
                return false;

            foreach (BusinessDay businessDay in businessDays)
            {
                BusinessDay toUpdate = this.BusinessDays.Where(bd => bd.DayId == businessDay.DayId).FirstOrDefault();
                toUpdate.StartTime = businessDay.StartTime;
                toUpdate.EndTime = businessDay.EndTime;
                toUpdate.NumberOfWorkers = businessDay.NumberOfWorkers;
                //this.BusinessDays.Update(businessDay);
                this.Entry(toUpdate).State = EntityState.Modified;
            }

            this.SaveChanges();
            return true;

        }


        // Update Details For Business

        public bool UpdateBusinessDetails(Business b, User u)
        {

            Business currentBusiness = this.Businesses.Where(business => business.BusinessId == b.BusinessId).FirstOrDefault(); 
            User currentUser = this.Users.Where(us => us.UserName == currentBusiness.UserName).FirstOrDefault();

            if (currentBusiness != null && currentUser != null)
            {
                currentBusiness.Bio = b.Bio;
                currentBusiness.BusinessAddress = b.BusinessAddress;
                currentBusiness.BusinessName = b.BusinessName;
                currentBusiness.FacebookUrl = b.FacebookUrl;
                currentBusiness.InstagramUrl = b.InstagramUrl;
                currentBusiness.InternetUrl = b.InternetUrl;

                currentUser.Email = u.Email;
                currentUser.UserPassword = u.UserPassword;
                currentUser.PhoneNumber = u.PhoneNumber;
                


                this.Entry(currentBusiness).State = EntityState.Modified;
                this.Entry(currentUser).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            return false;
        }


        //a function that adds a history

        public bool AddHistory(History history)
        {
            if (history != null)
            {
                History existsHistory = this.Histories.Where(h => (h.ClientId == history.Client.ClientId && h.BusinessId == history.Business.BusinessId && h.IsActive)).FirstOrDefault();

                if(existsHistory != null)
                {
                    existsHistory.IsActive = false;
                    this.Entry(existsHistory).State = EntityState.Modified;
                }

                this.Entry(history).State = EntityState.Added;
                this.SaveChanges();
                return true;
            }
            return false;
        }



        public string Test()
        {
            return "test";
        }
    }
}
