using gestionworkflow.Context;
using gestionworkflow.Models;
using gestionworkflow.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace gestionworkflow.Handlers.QueriesHandlres
{
    public class DemandeCongeQueryHandler : IRequestHandler<GetDemandeCongeQuery, List<DemandeConge>>
    {
        private readonly DbContextName _dbContext;

        public DemandeCongeQueryHandler(DbContextName dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DemandeConge>> Handle(GetDemandeCongeQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the leave requests for the specified user from the database
            var leaveRequests = await _dbContext.DemandeConges
                .Where(lr => lr.UtilisateurId == request.UserId)
                .ToListAsync(cancellationToken);

            // Map the leave requests to DTOs or view models
            var leaveRequestDtos = leaveRequests.Select(lr => new DemandeConge
            {
                Id = lr.Id,
                DateDebut = lr.DateDebut,
                DateFin = lr.DateFin,
                Email = lr.Email,
                Type = lr.Type,
                Commentaire = lr.Commentaire,
                Statut = lr.Statut
            }).ToList();

            return leaveRequestDtos;
        }
    }
}
