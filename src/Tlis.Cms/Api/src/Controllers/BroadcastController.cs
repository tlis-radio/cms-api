using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.Api.Constants;
using Tlis.Cms.Application.Contracts.Commands.Base;
using Tlis.Cms.Application.Contracts.Commands.Broadcasts.CreateCommand;
using Tlis.Cms.Application.Contracts.Commands.Broadcasts.DeleteCommand;
using Tlis.Cms.Application.Contracts.Commands.Broadcasts.UpdateCommand;
using Tlis.Cms.Application.Contracts.Commands.Broadcasts.UpdateImageCommand;
using Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetDetailsQuery;
using Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetInDateRangeQuery;
using Tlis.Cms.Application.Contracts.Queries.Broadcasts.PaginationQuery;

namespace Tlis.Cms.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class BroadcastController(IMediator mediator) : ControllerBase
{
    [HttpGet("in-date-range/{from:datetime}/{to:datetime}")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GetInDateRangeQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<GetInDateRangeQueryResponse>> GetBroadcastInDateRange([FromRoute] DateTime from, [FromRoute] DateTime to)
    {
        var response = await mediator.Send(new GetInDateRangeQuery { From = from, To = to });

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GetDetailsQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Get broadcast's details")]
    public async ValueTask<ActionResult<GetDetailsQueryResponse>> GetBroadcastDetails([FromRoute] Guid id)
    {
        var response = await mediator.Send(new GetDetailsQuery { Id = id });

        return response is null
            ? NotFound()
            : Ok(response);
    }


    [HttpGet("pagination")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PaginationQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Paging broadcast's")]
    public async ValueTask<ActionResult<PaginationQueryResponse>> Pagination([FromQuery] PaginationQuery request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    [Authorize(Policy.BroadcastWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create broadcast")]
    public async ValueTask<ActionResult<BaseCreateResponse>> CreateShow([FromBody, Required] CreateCommand request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? BadRequest()
            : CreatedAtAction(nameof(GetBroadcastDetails), new { response.Id } , response);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy.BroadcastWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update broadcast's details")]
    public async ValueTask<ActionResult> UpdateShow([FromRoute] Guid id, [FromBody, Required] UpdateCommand request)
    {
        request.Id = id;

        var response = await mediator.Send(request);

        return response ? NoContent() : BadRequest();
    }

    [HttpPut("{id:guid}/image")]
    [Authorize(Policy.BroadcastWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update broadcast's image")]
    public async ValueTask<ActionResult> UpdateImage([FromRoute] Guid id, [Required] IFormFile image)
    {
        var response = await mediator.Send(new UpdateImageCommand { Id = id, Image = image });

        return response ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy.BroadcastDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Delete program")]
    public async ValueTask<ActionResult> Delete([FromRoute] Guid id)
    {
        var response = await mediator.Send(new DeleteCommand { Id = id });

        return response ? NoContent() : NotFound();
    }
}