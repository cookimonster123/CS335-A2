using Microsoft.AspNetCore.Mvc;
using A2.Data;
using A2.Models;


namespace A2.Controllers
{

    [ApiController]
    [Route("webapi")]
    public class A2Controller : Controller {

        private readonly IA2Repo _repository;

        public A2Controller(IA2Repo repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult<string> Register(User user)
        {
            string username = user.UserName;
            if(_repository.IsUsernameUnique(username) == true)
            {
                
                _repository.AddUser(user);

                return Ok("User successfully registered.");
            }
            else
            { string msg = "UserName " + username + " is not available.";
                return Ok(msg);
            }
        }
    }
}