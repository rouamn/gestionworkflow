using MediatR;

namespace gestionworkflow.Commands
{
    public class UpdateDemandeStatusCommand : IRequest
    {
        public int Id { get; set; }
        public string Statut { get; set; }
    }

}
