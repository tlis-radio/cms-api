using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.Api.Constants;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;

namespace Tlis.Cms.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ShowController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ShowDetailsGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Get show's details")]
    public async ValueTask<ActionResult<ShowDetailsGetResponse>> GetDetails([FromRoute] Guid id)
    {
        var response = await mediator.Send(new ShowDetailsGetRequest { Id = id });

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpGet("pagination")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PaginationResponse<ShowPaginationGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Paging show's")]
    public async ValueTask<ActionResult<PaginationResponse<ShowPaginationGetResponse>>> Pagination([FromQuery] ShowPaginationGetRequest request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    // [Authorize(Policy.ShowWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create show")]
    public async ValueTask<ActionResult<BaseCreateResponse>> Create([FromBody, Required] ShowCreateRequest request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? BadRequest()
            : CreatedAtAction(nameof(GetDetails), new { response.Id } , response);
    }

    [HttpPut("{id:guid}")]
    // [Authorize(Policy.ShowWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update show's details")]
    public async ValueTask<ActionResult> Update([FromRoute] Guid id, [FromBody, Required] ShowUpdateRequest request)
    {
        request.Id = id;

        var response = await mediator.Send(request);

        return response ? NoContent() : BadRequest();
    }

    [HttpPut("{id:guid}/profile-image")]
    // [Authorize(Policy.ShowWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update show's profile image")]
    public async ValueTask<ActionResult> UpdateProfileImage([FromRoute] Guid id, [Required] IFormFile profileImage)
    {
        var response = await mediator.Send(new ShowUpdateProfileImageRequest { Id = id, ProfileImage = profileImage });

        return response ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    // [Authorize(Policy.ShowDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Delete show")]
    public async ValueTask<ActionResult> Delete([FromRoute] Guid id)
    {
        var response = await mediator.Send(new ShowDeleteRequest { Id = id });

        return response ? NoContent() : NotFound();
    }
}