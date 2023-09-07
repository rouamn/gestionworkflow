using MediatR;

namespace gestionworkflow.Commands
{
    public class UpdateDemandeCommand : IRequest
    {
        public int Id { get; set; }

        public int? UtilisateurId { get; set; }

        public DateTime? DateDebut { get; set; }

        public DateTime? DateFin { get; set; }
        public string? Type { get; set; }

        public string? Commentaire { get; set; }

        public string? Statut { get; set; }

        public string? Email { get; set; }


    }
}

