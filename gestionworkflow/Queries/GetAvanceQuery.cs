using gestionworkflow.Models;
using MediatR;

namespace gestionworkflow.Queries
{
    public class GetAvanceQuery : IRequest<List<DemandeAvance>>
    {
        public int UserId { get; set; }
    }
}
