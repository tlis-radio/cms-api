using System;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.Application.Contracts.Api.Responses;

public class BaseCreateResponse
{
    [SwaggerSchema(Description = "The unique identifier of the newly created resource.")]
    public required Guid Id { get; set; }
}