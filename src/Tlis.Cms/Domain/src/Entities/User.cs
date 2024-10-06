using System;
using System.Collections.Generic;
using Tlis.Cms.Domain.Entities.Base;
using Tlis.Cms.Domain.Entities.Images;
using Tlis.Cms.Domain.Entities.JoinTables;

namespace Tlis.Cms.Domain.Entities;

public class User : BaseEntity
{
    public bool CmsAdminAccess { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string Abouth { get; set; } = null!;

    public Guid? ProfileImageId { get; set; }

    public bool PreferNicknameOverName { get; set; }

    public string? ExternalId { get; set; }

    public string? Email { get; set; }

    public virtual Image? ProfileImage { get; set; }
    
    public virtual ICollection<UserRoleHistory> RoleHistory { get; set; } = [];

    public virtual ICollection<UserMembershipHistory> MembershipHistory { get; set; } = [];

    public virtual ICollection<User> Shows { get; set; } = [];

    public virtual ICollection<ShowsUsers> ShowsUsers { get; set; } = [];
}