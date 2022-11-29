using System;
using System.Collections.Generic;

#nullable disable

namespace stopBullying.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Denuncia = new HashSet<Denuncia>();
        }

        public int IdEstado { get; set; }
        public string Estado1 { get; set; }

        public virtual ICollection<Denuncia> Denuncia { get; set; }
    }
}
