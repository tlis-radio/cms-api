using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Base;
using Tlis.Cms.Application.Contracts.Commands.Shows.CreateCommand;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Shows;

internal sealed class CreateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCommand, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
    {   
        var toCreate = request.MapToShow();

        await unitOfWork.ShowRepository.InsertAsync(toCreate);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = toCreate.Id
        };
    }
}