using System;
using System.Collections.Generic;

namespace gestionworkflow.Models;

public partial class DemandeAvance
{
    public int Id { get; set; }

    public int? UtilisateurId { get; set; }

    public DateTime? DateDemande { get; set; }

    public double? Montant { get; set; }

    public string? Statut { get; set; }

   // public virtual User? Utilisateur { get; set; }
}
