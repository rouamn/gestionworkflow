
    using gestionworkflow.Commands;
using gestionworkflow.Models;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commandsHandlers
{
    public class AdDemandeCongeHandler : IRequestHandler<AddDemandeCongeCommand, DemandeConge>
    {
        private readonly IUserRepository _userRepository;
        public AdDemandeCongeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<DemandeConge> Handle(AddDemandeCongeCommand request, CancellationToken cancellationToken)
        {
            var demandeConge = new DemandeConge
            {
                UtilisateurId = request.UtilisateurId,
                DateDebut = request.DateDebut,
                DateFin = request.DateFin,
                Commentaire = request.Commentaire,
                Statut = request.Statut,
                Type = request.Type ,
                Email = request.Email,

            };

            await _userRepository.AddDemandeCongeAsync(demandeConge);

            return demandeConge;
        }
    }
     

   

      
    
}
