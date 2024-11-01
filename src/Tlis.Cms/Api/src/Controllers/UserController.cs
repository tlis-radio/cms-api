using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.Api.Constants;
using Tlis.Cms.Application.Contracts.Commands.Base;
using Tlis.Cms.Application.Contracts.Commands.Users.CreateCommand;
using Tlis.Cms.Application.Contracts.Commands.Users.DeleteCommand;
using Tlis.Cms.Application.Contracts.Commands.Users.UpdateCommand;
using Tlis.Cms.Application.Contracts.Commands.Users.UpdateProfileImageCommand;
using Tlis.Cms.Application.Contracts.Queries.Users.GetDetailsQuery;
using Tlis.Cms.Application.Contracts.Queries.Users.PaginationQuery;

namespace Tlis.Cms.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [SwaggerOperation("Get user's details")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GetDetailsQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<GetDetailsQueryResponse>> GetUserDetails([FromRoute] Guid id)
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
    [SwaggerOperation("Paging users")]
    public async ValueTask<ActionResult<PaginationQueryResponse>> Pagination([FromQuery] PaginationQuery request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create new user.", "Crate new user")]
    public async ValueTask<ActionResult<BaseCreateResponse>> Create([FromBody, Required] CreateCommand request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? BadRequest()
            : CreatedAtAction(nameof(GetUserDetails), new { response.Id } , response);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy.UserWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update user")]
    public async ValueTask<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody, Required] UpdateCommand request)
    {
        request.Id = id;

        var response = await mediator.Send(request);

        return response ? NoContent() : BadRequest();
    }

    [HttpPut("{id:guid}/profile-image")]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update profile picture to existing user.")]
    public async ValueTask<ActionResult<BaseCreateResponse>> UpdateProfileImage([FromRoute] Guid id, [Required] IFormFile profileImage)
    {
        var response = await mediator.Send(new UpdateProfileImageCommand
        {
            Id = id,
            ProfileImage = profileImage
        });

        return response ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy.UserDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Delete user")]
    public async ValueTask<ActionResult> DeleteUser([FromRoute] Guid id)
    {
        var response = await mediator.Send(new DeleteCommand { Id = id });

        return response ? NoContent() : NotFound();
    }
}