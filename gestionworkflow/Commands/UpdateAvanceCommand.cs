using MediatR;

namespace gestionworkflow.Commands
{
    public class UpdateAvanceCommand : IRequest
    {
        public int Id { get; set; }

        public int? UtilisateurId { get; set; }

        public DateTime? DateDemande { get; set; }

        public double? Montant { get; set; }

        public string? Statut { get; set; }

        // public virtual User? Utilisateur { get; set; }
    }
}
