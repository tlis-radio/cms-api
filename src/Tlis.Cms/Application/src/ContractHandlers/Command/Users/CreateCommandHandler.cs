using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Exceptions;
using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Services.Interfaces;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Application.Contracts.Commands.Users.CreateCommand;
using Tlis.Cms.Application.Contracts.Commands.Base;

namespace Tlis.Cms.Application.ContractHandlers.Command.Users;

internal sealed class CreateCommandHandler(
    IAuthProviderManagementService authProviderManagementService,
    IUserRoleService roleService,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCommand, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var newUser = request.MapToUser();

        foreach (var membershipHistory in request.MembershipHistory)
        {
            newUser.MembershipHistory.Add(new UserMembershipHistory
            {
                MembershipId = membershipHistory.MembershipId,
                ChangeDate = membershipHistory.ChangeDate,
                Description = membershipHistory.Description
            });
        }

        foreach (var history in request.RoleHistory)
        {
            var role = await roleService.GetByIdAsync(history.RoleId)
                ?? throw new UserRoleNotFoundException(history.RoleId);

            newUser.RoleHistory.Add(
                new UserRoleHistory
                {
                    RoleId = role.Id,
                    FunctionStartDate = history.FunctionStartDate,
                    FunctionEndDate = history.FunctionEndDate,
                    Description = history.Description
                }
            );
        }

        var roles = await unitOfWork.RoleRepository.GetByIdsAsync(
            newUser.RoleHistory.Select(x => x.RoleId).ToList(),
            asTracking: false);

        if (!string.IsNullOrEmpty(request.Email) && request.CmsAdminAccess)
        {
            newUser.ExternalId = await authProviderManagementService.CreateUserAsync(
                request.Email,
                roles.Select(x => x.ExternalId).ToArray());
        }

        await unitOfWork.UserRepository.InsertAsync(newUser);
        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync(cancellationToken);

        return new BaseCreateResponse
        {
            Id = newUser.Id
        };
    }
}