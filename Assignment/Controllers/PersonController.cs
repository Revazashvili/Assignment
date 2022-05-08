using Application.Core.Commands;
using Application.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PersonController : ApiControllerBase
{
    [HttpPost]
    public async Task<long> Save(string json) => await Mediator.Send(new SavePerson.Command(json));
    
    [HttpPost]
    public async Task<string> GetAll(GetAll.Query request) => await Mediator.Send(request);
}