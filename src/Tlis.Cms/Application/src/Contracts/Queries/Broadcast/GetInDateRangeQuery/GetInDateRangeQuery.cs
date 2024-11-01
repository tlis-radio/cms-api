using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcast.GetInDateRangeQuery;
public class GetInDateRangeQuery : IRequest<GetInDateRangeQueryResponse>
{
    public required DateTime From { get; set; }

    public required DateTime To { get; set; }
}