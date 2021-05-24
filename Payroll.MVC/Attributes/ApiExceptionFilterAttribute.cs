using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Payroll.MVC.Dtos.Responses;
using System;
using System.Net;

namespace Payroll.MVC.Attributes
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ExceptionContext _context;

        public override void OnException(ExceptionContext context)
        {
            _context = context;

            switch (context.Exception)
            {
                case ArgumentException br:
                    ToErrorResponse(HttpStatusCode.BadRequest, br.Message);
                    break;

                default:
                    ToErrorResponse(HttpStatusCode.BadRequest, "Something bad happened. Please contact customer support.");
                    break;
            }
        }

        public void ToErrorResponse(HttpStatusCode code, string message)
        {
            _context.HttpContext.Response.StatusCode = (int)code;
            _context.Result = new JsonResult(ApiResponse.ToError(message));
            _context.ExceptionHandled = true;
        }
    }
}