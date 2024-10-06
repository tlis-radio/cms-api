using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses.MembershipStatusGetAllResponses;

namespace Tlis.Cms.Application.Contracts.Api.Requests;

public sealed class MembershipStatusGetAllRequest : IRequest<MembershipStatusGetAllResponse>;