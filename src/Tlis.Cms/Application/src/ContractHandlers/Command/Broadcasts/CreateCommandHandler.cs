using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Base;
using Tlis.Cms.Application.Contracts.Commands.Broadcasts.CreateCommand;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Broadcasts;

internal sealed class CreateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCommand, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var broadcast = request.MapToBroadcast();

        await unitOfWork.BroadcastRepository.InsertAsync(broadcast);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = broadcast.Id,
        };
    }
}