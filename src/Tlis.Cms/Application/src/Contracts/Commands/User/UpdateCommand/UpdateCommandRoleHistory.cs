using System;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.Application.Contracts.Commands.User.UpdateCommand;

public sealed class UpdateCommandRoleHistory
{
    public required Guid? Id { get; set; }

    [SwaggerSchema(Description = "The user's role or permission level within the service or platform.")]
    public required Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The date on which the user began their current role or position within TLIS.")]
    public required DateTime FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public required DateTime? FunctionEndDate { get; set; }

    [SwaggerSchema(Description = "Description why this position was given to user.")]
    public required string? Description { get; set; }
}