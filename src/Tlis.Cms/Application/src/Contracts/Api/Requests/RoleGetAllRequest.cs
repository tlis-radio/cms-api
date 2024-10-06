using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses.RoleGetAllResponses;

namespace Tlis.Cms.Application.Contracts.Api.Requests;

public sealed class RoleGetAllRequest : IRequest<RoleGetAllResponse>;