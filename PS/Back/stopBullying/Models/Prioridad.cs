using System;
using System.Collections.Generic;

#nullable disable

namespace stopBullying.Models
{
    public partial class Prioridad
    {
        public Prioridad()
        {
            Denuncia = new HashSet<Denuncia>();
        }

        public int IdPrioridad { get; set; }
        public string Prioridad1 { get; set; }

        public virtual ICollection<Denuncia> Denuncia { get; set; }
    }
}
