using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}