using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Users;

public sealed class UserPaginationGetRequest : IRequest<PaginationResponse<UserPaginationGetResponse>>
{
    [DefaultValue(20)]
    public required int Limit { get; set; } = 20;

    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public required int Page { get; set; } = 1;
}