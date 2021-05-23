using System;
using System.Net;
using Battleships.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Battleships.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case InvalidAttackException e:
                    BuildErrorResponse(context, e);
                    break;
                case InvalidShipPositionException e:
                    BuildErrorResponse(context, e);
                    break;
                case NotFoundException _:
                    context.Result = new NotFoundResult();
                    break;
                default:
                    context.Result = new ObjectResult("An error happened with your request. Please try again later")
                    {
                        StatusCode = (int) HttpStatusCode.InternalServerError
                    };
                    break;
            }
        }

        private void BuildErrorResponse(ExceptionContext context, Exception e)
        {
            context.Result = new ObjectResult(e.Message)
            {
                StatusCode = (int) HttpStatusCode.BadRequest
            };
        }
    }
}