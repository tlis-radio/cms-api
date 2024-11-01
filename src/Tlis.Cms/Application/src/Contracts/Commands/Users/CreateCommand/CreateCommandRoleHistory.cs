using System;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.Application.Contracts.Commands.Users.CreateCommand;

public sealed class CreateCommandRoleHistory
{
    [SwaggerSchema(Description = "Id of the role.")]
    public required Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The starting date of when user started this position.")]
    public required DateTime FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public required DateTime? FunctionEndDate { get; set; }

    [SwaggerSchema(Description = "Description why this position was given to user.")]
    public required string? Description { get; set; }
}