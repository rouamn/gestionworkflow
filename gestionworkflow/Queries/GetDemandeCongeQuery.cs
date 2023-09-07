using gestionworkflow.Models;
using MediatR;

namespace gestionworkflow.Queries
{
    public class GetDemandeCongeQuery : IRequest<List<DemandeConge>>
    {
        public int UserId { get; set; }
    }
}
