using Application.Core.CQRS.Commands;
using Application.Core.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ApiControllerBase
    {
        [HttpPost]
        public async Task<long> Save(string json) =>
            await Mediator.Send(new SavePersonCommand { JsonData = json });


        [HttpPost]
        public async Task<string> GetAll(GetAllRequest request) =>
            await Mediator.Send(request);

    }
}
