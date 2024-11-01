using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Queries.Base;

public abstract class BasePaginationQuery<TResponse> : IRequest<BasePaginationQueryResponse<TResponse>>
{
    [DefaultValue(20)]
    [Range(1, 40)]
    public int Limit { get; set; } = 20;

    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
}