using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.Api.Constants;
using Tlis.Cms.Application.Contracts.Api.Requests;
using Tlis.Cms.Application.Contracts.Api.Responses.MembershipStatusGetAllResponses;

namespace Tlis.Cms.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class MembershipController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy.UserRead)]
    [SwaggerOperation("Get all membership statuses")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MembershipStatusGetAllResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<MembershipStatusGetAllResponse>> GetAll()
    {
        var response = await mediator.Send(new MembershipStatusGetAllRequest());

        return response is null
            ? NotFound()
            : Ok(response);
    }
}