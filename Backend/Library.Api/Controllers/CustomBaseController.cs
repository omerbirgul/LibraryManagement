using System.Net;
using Library.Core.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CustomActionResult<T>(ResultService<T> result) where T: class
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Unauthorized => Unauthorized(),
                _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
            };
        }

        [NonAction]
        public IActionResult CustomActionResult(ResultService result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Unauthorized => Unauthorized(),
                _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
            };
        }
    }
}
