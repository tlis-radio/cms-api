using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Queries.Show.GetDetailsQuery;

public sealed class GetDetailsQuery : IRequest<GetDetailsQueryResponse>
{
    public required Guid Id { get; set; }
}