using gestionworkflow.Commands;
using gestionworkflow.Exeptions;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commandsHandlers
{
    public class UpdateDemandeCommandHandler : IRequestHandler<UpdateDemandeCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateDemandeCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateDemandeCommand request, CancellationToken cancellationToken)
        {
            var existingDemande = await _userRepository.GetDemandeCongeByIdAsync(request.Id);
            if (existingDemande == null)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
            }

            existingDemande.Email = request.Email;
            existingDemande.DateDebut= request.DateDebut;
            existingDemande.DateFin = request.DateFin;
            existingDemande.Statut = request.Statut;
            existingDemande.Commentaire = request.Commentaire;

            await _userRepository.UpdateDemandeAsync(existingDemande);
        }

        
    }
}
