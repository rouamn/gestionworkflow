using System;
using System.Collections.Generic;

namespace gestionworkflow.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string Status { get; set; }

    //public virtual ICollection<DemandeAvance> DemandeAvances { get; set; } = new List<DemandeAvance>();

    // public virtual ICollection<DemandeConge> DemandeConges { get; set; } = new List<DemandeConge>();

}
