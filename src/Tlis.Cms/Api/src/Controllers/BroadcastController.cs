using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.Api.Constants;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;

namespace Tlis.Cms.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class BroadcastController(IMediator mediator) : ControllerBase
{
    [HttpGet("in-date-range/{from:datetime}/{to:datetime}")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BroadcastGetInDateRangeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<BroadcastGetInDateRangeResponse>> GetBroadcastInDateRange([FromRoute] DateTime from, [FromRoute] DateTime to)
    {
        var response = await mediator.Send(new BroadcastGetInDateRangeRequest { From = from, To = to });

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BroadcastDetailsGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Get broadcast's details")]
    public async ValueTask<ActionResult<BroadcastDetailsGetResponse>> GetBroadcastDetails([FromRoute] Guid id)
    {
        var response = await mediator.Send(new BroadcastDetailsGetRequest { Id = id });

        return response is null
            ? NotFound()
            : Ok(response);
    }


    [HttpGet("pagination")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PaginationResponse<BroadcastPaginationGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Paging broadcast's")]
    public async ValueTask<ActionResult<PaginationResponse<BroadcastPaginationGetResponse>>> Pagination([FromQuery] BroadcastPaginationGetRequest request)
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
    public async ValueTask<ActionResult<BaseCreateResponse>> CreateShow([FromBody, Required] BroadcastCreateRequest request)
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
    public async ValueTask<ActionResult> UpdateShow([FromRoute] Guid id, [FromBody, Required] BroadcastUpdateRequest request)
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
        var response = await mediator.Send(new BroadcastUpdateImageRequest { Id = id, Image = image });

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
        var response = await mediator.Send(new BroadcastDeleteRequest { Id = id });

        return response ? NoContent() : NotFound();
    }
}