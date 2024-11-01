using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Broadcasts.UpdateCommand;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Broadcasts;

internal sealed class UpdateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommand, bool>
{
    public async Task<bool> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var toUpdate = await unitOfWork.BroadcastRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.Name = request.Name;
        toUpdate.Description = request.Description;
        toUpdate.StartDate = request.StartDate;
        toUpdate.EndDate = request.EndDate;
        toUpdate.ShowId = request.ShowId;

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}