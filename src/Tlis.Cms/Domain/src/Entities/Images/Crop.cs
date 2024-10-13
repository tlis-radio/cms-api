using System;
using Tlis.Cms.Domain.Entities.Base;

namespace Tlis.Cms.Domain.Entities.Images;

public class Crop : BaseEntity
{
    public int Width { get; set; }

    public int Height { get; set; }

    public long Size { get; set; }

    public string FileName { get; set; } = null!;

    public Guid ImageId { get; set; }
}