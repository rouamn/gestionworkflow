using MediatR;

namespace gestionworkflow.Commands
{
    public class UpdateAvanceStatusCommand : IRequest
    {
        public int Id { get; set; }
        public string Statut { get; set; }
    }
}
