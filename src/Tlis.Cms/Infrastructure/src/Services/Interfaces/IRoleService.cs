using System;
using System.Threading.Tasks;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface IRoleService
{
    Task<Role?> GetByIdAsync(Guid id);
}