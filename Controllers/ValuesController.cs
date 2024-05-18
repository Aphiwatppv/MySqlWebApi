using MySqlAccessAuthen.Model;
using MySqlAccessAuthen.MySqlServicesAuthen;
using MySqlAccessAuthen.MySqlAccessAuthen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Configuration;

namespace MySqlWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IMySqlServicesAuthen _mySqlService;

        public ValuesController(IMySqlServicesAuthen mySqlService)
        {
            _mySqlService = mySqlService;
        }


        [HttpGet]
        [Route("api/values/getUsers")]
        public async Task<IHttpActionResult> GetUsers()
        {
            var result = await _mySqlService.getAllUserAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("api/values/Register")]
        public async Task<IHttpActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mySqlService.RegisteringAsync(registerModel);
            if (result == "Success")
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("api/values/Login")]
        public async Task<IHttpActionResult> Login([FromBody] UserInput userInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mySqlService.LoginAsync(userInput);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }

}

