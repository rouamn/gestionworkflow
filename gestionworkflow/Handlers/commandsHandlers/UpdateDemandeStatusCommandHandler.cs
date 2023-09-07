using gestionworkflow.Commands;
using gestionworkflow.Exeptions;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commandsHandlers
{
    public class UpdateDemandeStatusCommandHandler : IRequestHandler<UpdateDemandeStatusCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateDemandeStatusCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateDemandeStatusCommand request, CancellationToken cancellationToken)
        {
            var demande = await _userRepository.GetDemandeCongeByIdAsync(request.Id);

            if (demande != null)
            {
                demande.Statut = request.Statut;
                await _userRepository.UpdateDemandeAsync(demande);
            }
        }
    }

}
