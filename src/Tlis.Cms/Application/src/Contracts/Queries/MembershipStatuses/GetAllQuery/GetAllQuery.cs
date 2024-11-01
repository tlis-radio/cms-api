using MediatR;

namespace Tlis.Cms.Application.Contracts.Queries.MembershipStatuses.GetAllQuery;

public sealed class GetAllQuery : IRequest<GetAllQueryResponse>;