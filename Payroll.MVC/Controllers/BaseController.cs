using Microsoft.AspNetCore.Mvc;
using Payroll.MVC.Attributes;
using Payroll.MVC.Dtos.Responses;

namespace Payroll.MVC.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult OkEmptyApiResponse()
        {
            return Ok(new ApiResponse());
        }

        protected IActionResult OkApiResponse<T>(T payload = null) where T : class
        {
            return Ok(ApiResponse<T>.ToPayload(payload));
        }
    }
}