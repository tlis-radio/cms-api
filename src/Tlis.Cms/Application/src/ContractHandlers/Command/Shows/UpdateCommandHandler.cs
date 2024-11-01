using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Shows.UpdateCommand;
using Tlis.Cms.Domain.Entities.JoinTables;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Shows;

internal sealed class UpdateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommand, bool>
{
    public async Task<bool> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var toUpdate = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.Name = request.Name;
        toUpdate.Description = request.Description;

        toUpdate.ShowsUsers.Clear();
        toUpdate.ShowsUsers = request.ModeratorIds.Select(userId => new ShowsUsers { UserId = userId }).ToList();

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}