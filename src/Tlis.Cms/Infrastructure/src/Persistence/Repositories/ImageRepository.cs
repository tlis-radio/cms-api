using Tlis.Cms.Domain.Entities.Images;
using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories;

internal sealed class ImageRepository(CmsDbContext context) : GenericRepository<Image>(context), IImageRepository;