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
                    Client client = context.Clients.Where(c => c.UserName == user.UserName)
                        .Include(cl => cl.Favorites)
                        .Include(cl => cl.Histories)
                        .Include(cl => cl.Comments)
                        .Include(cl => cl.Reservations)
                        .FirstOrDefault();
                    mUser = new MainUserDTO { Business = null, Client = client, User = user };

                }
                else//business
                {
                    Business business = context.Businesses.Where(b => b.UserName == user.UserName)
                        .Include(b => b.BusinessDays)
                        .Include(b => b.Comments)
                        .Include(b => b.Favorites)
                        .Include(b => b.Histories)
                        .Include(b => b.Reservations)
                        .Include(b => b.SpecialNumberOfWorkers)
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

        // a function that checks that the inserted email and user name are unique


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
            

            //Check user name and password
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


        // test func


        [Route("Test")]
        [HttpGet]
        public string Test()
        {
            string str = context.Test();

            if (str != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return str;
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return null;
        }

    }
}
