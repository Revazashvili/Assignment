using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

#pragma warning disable CS8603 // Possible null reference return.
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
#pragma warning restore CS8603 // Possible null reference return.
    }
}
