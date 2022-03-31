using Application.Core.CQRS.Commands;
using Application.Core.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ApiControllerBase
    {
        [HttpPost]
        public async Task<long> Save(string json) =>
            await Mediator.Send(new SavePersonCommand { JsonData = json });


        [HttpGet("GetAll")]
        public async Task<string> GetAll([FromQuery]GetAllRequest request) =>
            await Mediator.Send(request);

    }
}
