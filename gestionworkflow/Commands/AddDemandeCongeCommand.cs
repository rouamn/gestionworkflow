using gestionworkflow.Models;
using MediatR;

namespace gestionworkflow.Commands
{
    public class AddDemandeCongeCommand : IRequest<DemandeConge>
    {
        //public string Id { get; set; }

        public int? UtilisateurId { get; set; }

        public DateTime? DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        public string? Commentaire { get; set; }

        public string? Statut { get; set; }
        public string? Type { get; set; }

        public string? Email { get; set; }


        public virtual User? Utilisateur { get; set; }
    }
}
