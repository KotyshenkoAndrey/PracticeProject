using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.Api.App
{
    [Route("api/{version:apiVersion}[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public int testcontr(int value)
        {
            return value;
        }


    }
}
