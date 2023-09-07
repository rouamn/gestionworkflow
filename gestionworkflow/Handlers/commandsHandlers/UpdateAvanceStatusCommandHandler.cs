using gestionworkflow.Commands;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commandsHandlers
{
    public class UpdateAvanceStatusCommandHandler : IRequestHandler<UpdateAvanceStatusCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateAvanceStatusCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateAvanceStatusCommand request, CancellationToken cancellationToken)
        {
            var demande = await _userRepository.GetDemandeAvanceByIdAsync(request.Id);

            if (demande != null)
            {
                demande.Statut = request.Statut;
                await _userRepository.UpdateAvanceAsync(demande);
            }
        }
    }
}
