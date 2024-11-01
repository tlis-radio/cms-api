using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Commands.Users.UpdateCommand;

public static class UpdateCommandMappings
{
    public static UserRoleHistory MapToEntity(this UpdateCommandRoleHistory data)
        => new()
        {
            RoleId = data.RoleId,
            FunctionEndDate = data.FunctionEndDate,
            FunctionStartDate = data.FunctionStartDate,
            Description = data.Description
        };

    public static UserMembershipHistory MapToEntity(this UpdateCommandMembershipHistory data)
        => new()
        {
            MembershipId = data.MembershipId,
            ChangeDate = data.ChangeDate,
            Description = data.Description
        };

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