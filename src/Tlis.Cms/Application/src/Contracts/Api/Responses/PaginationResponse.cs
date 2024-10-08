using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses;

public sealed class PaginationResponse<T>
{
    public int Total { get; set; }

    public int Limit { get; set; }

    public int Page { get; set; }

    public int TotalPages { get; set; }

    public List<T> Results { get; set; } = [];
}