using System.Threading.Tasks;
using Contracts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
public class EchoWordEndpoint : ControllerBase
{
    private readonly ISender _mediator;

    public EchoWordEndpoint(ISender mediator) { _mediator = mediator; }

    [HttpPost("{word}/echo")]
    public async Task<IActionResult> Post([FromRoute] string word)
    {
        await _mediator.Send(new EchoWord(word));
        return Ok();
    }
}


