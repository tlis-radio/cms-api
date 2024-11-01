using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Queries.Shows.GetDetailsQuery;

public sealed class GetDetailsQuery : IRequest<GetDetailsQueryResponse?>
{
    public required Guid Id { get; set; }
}