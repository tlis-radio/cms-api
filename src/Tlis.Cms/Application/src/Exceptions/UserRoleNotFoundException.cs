using System;

namespace Tlis.Cms.Application.Exceptions;

public sealed class UserRoleNotFoundException(Guid roleId)
    : Exception($"Role with id: {roleId} not found in Cache or Db");