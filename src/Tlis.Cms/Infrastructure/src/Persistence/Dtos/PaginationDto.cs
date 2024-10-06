using System;
using System.Collections.Generic;
using Tlis.Cms.Domain.Entities.Base;

namespace Tlis.Cms.Infrastructure.Persistence.Dtos;

public class PaginationDto<TEntity> where TEntity : BaseEntity
{
    public required int Page { get; set; }

    public required int Limit { get; set; }
    
    public required int Total { get; set; }
    
    public int TotalPages => Limit > 0 ? (int)Math.Ceiling((double)Total / Limit) : 0;

    public required List<TEntity> Results { get; set; } = [];
}