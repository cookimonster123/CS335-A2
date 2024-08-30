using Microsoft.AspNetCore.Mvc;
using A2.Data;
using A2.Models;
using A2.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;

namespace A2.Controllers
{

    [ApiController]
    [Route("webapi")]
    public class A2Controller : Controller
    {

        private readonly IA2Repo _repository;

        public A2Controller(IA2Repo repository)
        {
            _repository = repository;
        }


        [HttpPost("Register")]
        public ActionResult<string> Register(User user)
        {
            string username = user.UserName;
            if (_repository.IsUsernameUnique(username) == true)
            {

                _repository.AddUser(user);

                return Ok("User successfully registered.");
            }
            else
            {
                string msg = "UserName " + username + " is not available.";
                return Ok(msg);
            }
        }


        [HttpGet("PurchaseSign/{id}")]
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy ="UserOnly")]
        public ActionResult<PurchaseOutput> PurchaseSign(string id)
        {
            Claim claim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (claim == null)
            {
                return Unauthorized(); // ??? or forbid
            }

            // check if the sign is found
            Sign sign = _repository.GetSignById(id);

            if(sign == null)
            {
                string msg = "Sign " + id + " not found";
                return BadRequest(msg);
            }
            string username = claim.Value.ToString();
            User user = _repository.GetUserByUsername(username);

            PurchaseOutput pout = new PurchaseOutput { UserName = user.UserName, SignID = id };
            return Ok(pout);

        }


        [HttpPost("AddEvent")]
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "OrganizerOnly")]
        public ActionResult AddEvent(EventInput eventInput){
            Event e = new Event
            {
                Description = eventInput.Description,
                End = eventInput.End,
                Start = eventInput.Start,
                Location = eventInput.Location,
                Summary = eventInput.Summary
            };

            
            DateTime StartDate;
            DateTime EndDate;
            bool startValid = DateTime.TryParseExact(eventInput.Start,
                "yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out StartDate);

            bool endValid = DateTime.TryParseExact(eventInput.End,
                "yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out EndDate);

            if(!(startValid || endValid))
            {
                return BadRequest("The format of Start and End should be yyyyMMddTHHmmssZ.");
            }
            else if(!startValid && endValid)
            {
                return BadRequest("The format of Start should be yyyyMMddTHHmmssZ.");
            }
            else if(!endValid && startValid)
            {
                return BadRequest("The format of End should be yyyyMMddTHHmmssZ.");
            }
            else
            {
                Event addedEvent = _repository.AddEvent(e);
                return Ok("Success");
            }
        }


        [HttpGet("EventCount")]
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "AuthOnly")]
        public ActionResult<int> EventCount()
        {
            return Ok(_repository.GetEventCount());
        }


        [HttpGet("Event/{id}")]
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "AuthOnly")]
        public ActionResult Event(int id)
        {
            Event e = _repository.GetEventById(id);
            if(e == null)
            {
                string msg = "Event " + id.ToString() + " does not exist";
                return BadRequest(msg);
            }
            Response.Headers.Add("Content-Type", "text/calendar");
            return Ok(e);
        }
    }
}