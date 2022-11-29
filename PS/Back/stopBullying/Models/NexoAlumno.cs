using System;
using System.Collections.Generic;

#nullable disable

namespace stopBullying.Models
{
    public partial class NexoAlumno
    {
        public NexoAlumno()
        {
            Denuncia = new HashSet<Denuncia>();
        }

        public int IdNexo { get; set; }
        public string NombreNexo { get; set; }
        public string TelNexo { get; set; }
        public string Mail { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool? IsDeleted { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Denuncia> Denuncia { get; set; }
    }
}
