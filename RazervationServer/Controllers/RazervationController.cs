using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazervationServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public User Login([FromQuery] string email, [FromQuery] string pass)
        {
            User user = context.Login(email, pass);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return user;
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
