using MediatR;

namespace Tlis.Cms.Application.Contracts.Queries.Role.GetAllQuery;

public sealed class GetAllQuery : IRequest<GetAllQueryResponse>;