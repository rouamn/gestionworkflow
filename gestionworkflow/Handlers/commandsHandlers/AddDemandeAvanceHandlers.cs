using gestionworkflow.Commands;
using gestionworkflow.Models;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commandsHandlers
{
    public class AddDemandeAvanceHandlers : IRequestHandler<AdDemandeAvanceCommand, DemandeAvance>
    {
        private readonly IUserRepository _userRepository;
        public AddDemandeAvanceHandlers(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<DemandeAvance> Handle(AdDemandeAvanceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DemandeAvance> HandleAsync(AdDemandeAvanceCommand request, CancellationToken cancellationToken)
        {
            var DemandeAvance = new DemandeAvance
            {
                UtilisateurId = request.UtilisateurId,
                DateDemande = request.DateDemande,
                Montant = request.Montant,
              
                Statut = request.Statut,
            };

            await _userRepository.AddDemandeAvanceAsync(DemandeAvance);

            return DemandeAvance;
        }
    }
}
