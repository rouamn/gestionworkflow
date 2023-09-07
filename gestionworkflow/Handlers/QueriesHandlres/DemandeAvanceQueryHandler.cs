using gestionworkflow.Context;
using gestionworkflow.Models;
using gestionworkflow.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace gestionworkflow.Handlers.QueriesHandlres
{
    public class DemandeAvanceQueryHandler : IRequestHandler<GetAvanceQuery, List<DemandeAvance>>
    {
        private readonly DbContextName _dbContext;

        public DemandeAvanceQueryHandler(DbContextName dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DemandeAvance>> Handle(GetAvanceQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the leave requests for the specified user from the database
            var Avance = await _dbContext.DemandeAvances
                .Where(lr => lr.UtilisateurId == request.UserId)
                .ToListAsync(cancellationToken);

            // Map the leave requests to DTOs or view models
            var AvanceDtos = Avance.Select(lr => new DemandeAvance
            {
                Id = lr.Id,
                UtilisateurId = lr.UtilisateurId,
                DateDemande = lr.DateDemande,
                Montant = lr.Montant,
                Statut = lr.Statut,
             
            }).ToList();

            return AvanceDtos;
        }
    }
}
