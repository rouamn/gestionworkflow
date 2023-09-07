using gestionworkflow.Commands;
using gestionworkflow.Exeptions;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commandsHandlers
{
    public class UpdateAvanceCommandHandler : IRequestHandler<UpdateAvanceCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateAvanceCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateAvanceCommand request, CancellationToken cancellationToken)
        {
            var existingAvance = await _userRepository.GetDemandeAvanceByIdAsync(request.Id);
            if (existingAvance == null)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
            }

            existingAvance.DateDemande = request.DateDemande;
            existingAvance.Montant = request.Montant;
            existingAvance.UtilisateurId = request.UtilisateurId;
            existingAvance.Statut = request.Statut;
          //  existingDemande.Commentaire = request.Commentaire;

            await _userRepository.UpdateAvanceAsync(existingAvance);
        }


    }
}
