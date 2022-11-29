using System;
using System.Collections.Generic;

#nullable disable

namespace stopBullying.Models
{
    public partial class Direccion
    {
        public Direccion()
        {
            Denuncia = new HashSet<Denuncia>();
        }

        public int IdDirector { get; set; }
        public string NombreDirector { get; set; }
        public string TelDirector { get; set; }
        public string Mail { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool? IsDeleted { get; set; }
        public string Direccion1 { get; set; }

        public virtual ICollection<Denuncia> Denuncia { get; set; }
    }
}
