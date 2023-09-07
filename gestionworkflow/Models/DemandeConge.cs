using System;
using System.Collections.Generic;

namespace gestionworkflow.Models;

public partial class DemandeConge
{
    public int Id { get; set; }

    public int? UtilisateurId { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }
    public string? Type { get; set; }

    public string? Commentaire { get; set; }

    public string? Statut { get; set; }

    public string? Email { get; set; }

    // public virtual User? Utilisateur { get; set; }
}
