using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazervationServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazervationServer.DTO;


namespace RazervationServer.Controllers
{
    [Route("RazervationAPI")]
    [ApiController]
    public class RazervationController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        RazervationDBContext context;
        public RazervationController(RazervationDBContext context)
        {
            this.context = context;
        }
        #endregion

        // login


        [Route("Login")]
        [HttpGet]
        public MainUserDTO Login([FromQuery] string emailOrUName, [FromQuery] string pass)
        {
            User user = context.Login(emailOrUName, pass);
            MainUserDTO mUser = null;

            if (user != null)
            {
                if (user.UserType == true)//client
                {
                    Client client = context.Clients.Where(c => c.UserName == user.UserName).Include(cl => cl.UserNameNavigation)
                        .Include(cl => cl.Favorites).ThenInclude(cl => cl.Business).ThenInclude(cl=>cl.Category)
                        .Include(cl => cl.Favorites).ThenInclude(cl => cl.Business).ThenInclude(cl =>cl.UserNameNavigation)
                        .Include(cl => cl.Histories).ThenInclude(cl => cl.Business).ThenInclude(cl => cl.Category)
                        .Include(cl => cl.Comments).ThenInclude(cl => cl.Business).ThenInclude(cl => cl.Category)
                        .Include(cl => cl.Reservations).ThenInclude(cl => cl.Business).ThenInclude(cl => cl.Category)
                        .Include(cl => cl.Reservations).ThenInclude(cl => cl.Service)
                        .FirstOrDefault();
                    mUser = new MainUserDTO { Business = null, Client = client, User = user };

                }
                else//business
                {
                    Business business = context.Businesses.Where(b => b.UserName == user.UserName)
                        .Include(b => b.Category)
                        .Include(b => b.UserNameNavigation)
                        .Include(b => b.BusinessDays)
                        .Include(b => b.Comments).ThenInclude(b => b.Client)
                        .Include(b => b.Favorites)
                        .Include(b => b.Histories)
                        .Include(b => b.Reservations)
                        .Include(b => b.SpecialNumberOfWorkers)
                        .Include(b => b.Bservices)
                        .FirstOrDefault();

                    mUser = new MainUserDTO { Business = business, Client = null, User = user };
                }
            }

            //Check user name and password
            if (mUser != null)
            {
                HttpContext.Session.SetObject("theUser", mUser);
                

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return mUser;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }



        // Sign Up for Client!!!

        [Route("ClientSignUp")]
        [HttpPost]

        public bool ClientSignUp([FromBody] MainUserDTO mUser)
        {
            bool isSuccess = context.ClientSignUp(mUser.Client, mUser.User);

            if(isSuccess)//the sign up worked
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the sign up failed
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }

        // Sign Up for Business!!!

        [Route("BusinessSignUp")]
        [HttpPost]

        public bool BusinessSignUp([FromBody] MainUserDTO mUser)
        {
            bool isSuccess = context.BusinessSignUp(mUser.Business, mUser.User);

            if (isSuccess)//the sign up worked
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the sign up failed
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }

        // a function that checks that the inserted email, phone number and user name are unique


        [Route("CheckUniqueness")]
        [HttpGet]

        public bool CheckUniqueness([FromQuery] string email, [FromQuery] string userName, [FromQuery] string phoneNum)
        {
            bool isUnique = this.context.CheckUniqueness(email, userName, phoneNum);
            
            if (isUnique)//the email and the user name are unique
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isUnique;
            }
            else//one or both are not unique
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isUnique;
            }
        }


        // a function that returns a list of all the categories


        [Route("GetAllCategories")]
        [HttpGet]
        public List<Category> GetAllCategories()
        {

            List<Category> categories = this.context.Categories.ToList<Category>();
            

            //Check if there are any categories
            if (categories != null)
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return categories;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        

        [Route("GetAllBusinesses")]
        [HttpGet]
        public List<Business> GetAllBusinesses()
        {

            List<Business> businesses = this.context.Businesses.Include(b => b.Category)
                        .Include(b => b.UserNameNavigation)
                        .Include(b => b.Comments).ThenInclude(b => b.Client)
                        .Include(b => b.Favorites)
                        .Include(b => b.Histories)
                        .Include(b => b.Reservations)
                        .Include(b => b.SpecialNumberOfWorkers)
                        .Include(b => b.Bservices)
                        .ToList<Business>();


            //Check if there are any categories
            if (businesses != null)
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return businesses;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }



        // a function that changes the reservation status

        [Route("ChangeReservationStatus")]
        [HttpPost]
        public bool ChangeReservationStatus([FromBody] Reservation reservation, [FromQuery] int statusId)
        {
            bool isSuccess = context.ChangeReservationStatus(reservation, statusId);

            if (isSuccess)//the reservation has been changed
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the reservation has not been changed
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }


        // The Search Function

        [Route("Search")]
        [HttpGet]
        public List<Business> Search([FromQuery] string searchInput)
        {

            List<Business> businesses = this.context.Search(searchInput);
            //List<BusinessDTO> businessesDTO = new List<BusinessDTO>();
            //foreach (Business b in businesses)
            //{
            //    businessesDTO.Add(new BusinessDTO(b));
            //}

            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return businesses;

        }


        // The Search By Category Function

        [Route("SearchByCategory")]
        [HttpGet]
        public List<Business> SearchByCategory([FromQuery] string strCategoryId)
        {
            List<Business> businesses = this.context.SearchByCategory(strCategoryId);
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return businesses;
        }


        // The Update Details For Client Function

        [Route("UpdateClientDetails")]
        [HttpGet]

        public bool UpdateClientDetails([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string userName, [FromQuery] string email,
            [FromQuery] string password, [FromQuery] string phoneNum, [FromQuery] string gender)
        {

            MainUserDTO currentMUser = HttpContext.Session.GetObject<MainUserDTO>("theUser");

            bool isSuccess = context.UpdateClientDetails(firstName, lastName, userName, email, password, phoneNum, gender, currentMUser.Client , currentMUser.User);

            if (isSuccess)//the details have been updated
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the details have not been updated
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }


        // The Function That Deletes A Comment


        [Route("DeleteComment")]
        [HttpGet]

        public bool DeleteComment([FromQuery] string commentId)
        {
            bool isSuccess = context.DeleteComment(commentId);

            if (isSuccess)//the comment has been deleted
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the comment has not been deleted
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }


        // a function that creates a new favorite or activates one


        [Route("AddFavorite")]
        [HttpGet]

        public bool AddFavorite([FromQuery] string clientId, [FromQuery] string businessId)
        {
            bool isSuccess = context.AddFavorite(clientId,businessId);

            if (isSuccess)//the favorite has been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the favorite has not been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }

        // a function that deletes a favorite


        [Route("DeleteFavorite")]
        [HttpPost]

        public bool DeleteFavorite([FromBody] Favorite favorite)
        {
            bool isSuccess = context.DeleteFavorite(favorite.ClientId, favorite.BusinessId);

            if (isSuccess)//the favorite has been deleted
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the favorite has not been deleted
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }


        // a function that creates a new comment

        [Route("AddComment")]
        [HttpPost]
        public bool AddComment([FromBody] CommentDTO comment)
        {
            bool isSuccess = context.AddComment(comment.ClientId,comment.BusinessId,comment.Rating,comment.CommentText,comment.Cdate);

            if (isSuccess)//the comment has been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the comment has not been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }


        // get reservations function

        [Route("GetBusinessReservations")]
        [HttpGet]
        public List<Reservation> GetBusinessReservations([FromQuery] string businessId, [FromQuery] string statusId, [FromQuery] string date)
        {

            List<Reservation> reservations = context.GetReservations(businessId, statusId, date);

            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return reservations;

        }


        // get a reservation status that matches the inserted status id

        [Route("GetReserveStatus")]
        [HttpGet]
        public ReserveStatus GetReserveStatus([FromQuery] string statusId)
        {

           ReserveStatus status = context.GetReserveStatus(statusId);

            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return status;

        }


        // a function that adds a new reservation

        [Route("AddReservation")]
        [HttpPost]
        public bool AddReservation([FromBody] Reservation reservation)
        {
            bool isSuccess = context.AddReservation(reservation);

            if (isSuccess)//the reservation has been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the reservation has not been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }

        // a function that adds a service

        [Route("AddService")]
        [HttpPost]
        public bool AddService([FromBody] Bservice service)
        {
            bool isSuccess = context.AddService(service);

            if (isSuccess)//the reservation has been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the reservation has not been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }


        // a function that deletes a service


        [Route("DeleteService")]
        [HttpPost]

        public bool DeleteService([FromBody] Bservice service)
        {
            bool isSuccess = context.DeleteService(service);

            if (isSuccess)//the favorite has been deleted
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the favorite has not been deleted
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }

        // a function that adds a special number of workers

        [Route("AddSpecialNumberOfWorkers")]
        [HttpPost]
        public bool AddSpecialNumberOfWorkers([FromBody] SpecialNumberOfWorker specialNumberOfWorker)
        {
            bool isSuccess = context.AddSpecialNumberOfWorkers(specialNumberOfWorker);

            if (isSuccess)//the reservation has been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return isSuccess;
            }
            else//the reservation has not been added
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return isSuccess;
            }
        }

        // get a reservation status that matches the inserted status id

        [Route("GetNumberOfWorkers")]
        [HttpGet]
        public int GetNumberOfWorkers([FromQuery] string date, [FromQuery] string businessId)
        {

            int numberOfWorkers = context.GetNumberOfWorkers(date, businessId);

            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return numberOfWorkers;

        }



    }
}
