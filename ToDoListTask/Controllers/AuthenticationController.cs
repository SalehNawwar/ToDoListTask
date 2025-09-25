using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ToDoListTask.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        //[HttpPost,Authorize,Route("login")]
        //public IActionResult Login([FromBody]identitymodel)
        //{

        //}
    }
}
