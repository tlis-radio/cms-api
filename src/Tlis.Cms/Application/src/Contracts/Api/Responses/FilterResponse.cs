using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses;

public sealed class FilterResponse<T>
{
    public required List<T> Results { get; set; } = [];
}