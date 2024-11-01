using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Commands.Users.UpdateCommand;

public static class UpdateCommandMappings
{
    public static UserRoleHistory MapToUserRoleHistory(this UpdateCommandRoleHistory request)
    {
        return new UserRoleHistory
        {
            RoleId = request.RoleId,
            FunctionEndDate = request.FunctionEndDate,
            FunctionStartDate = request.FunctionStartDate,
            Description = request.Description
        };
    }

    public static UserMembershipHistory MapToUserMembershipHistory(this UpdateCommandMembershipHistory request)
    {
        return new UserMembershipHistory
        {
            MembershipId = request.MembershipId,
            ChangeDate = request.ChangeDate,
            Description = request.Description
        };
    }

    public static UserRoleHistory Update(this UserRoleHistory existing, UpdateCommandRoleHistory @new)
    {
        existing.RoleId = @new.RoleId;
        existing.FunctionEndDate = @new.FunctionEndDate;
        existing.FunctionStartDate = @new.FunctionStartDate;
        existing.Description = @new.Description;

        return existing;
    }

    public static UserMembershipHistory Update(this UserMembershipHistory existing, UpdateCommandMembershipHistory @new)
    {
        existing.MembershipId = @new.MembershipId;
        existing.ChangeDate = @new.ChangeDate;
        existing.Description = @new.Description;

        return existing;
    }
}