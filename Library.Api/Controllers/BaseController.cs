using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Api.Utils;

namespace Library.Api.Controllers
{
  
    public class AuthBaseController : ControllerBase
    {

        public int UserId => (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimsConstant.UserId)?.Value).ConvertToInt();
    }
}
