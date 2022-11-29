using System;
using System.Collections.Generic;

#nullable disable

namespace stopBullying.Models
{
    public partial class Denuncia
    {
        public int IdDenuncia { get; set; }
        public int? IdEstado { get; set; }
        public int? IdNexo { get; set; }
        public int? IdDirector { get; set; }
        public string NombreDenunciante { get; set; }
        public string NombreObservador { get; set; }
        public string NombreAgresor { get; set; }
        public string Descripcion { get; set; }
        public string Notas { get; set; }
        public int? IdPrioridad { get; set; }
        public string Imagen { get; set; }
        public bool? Emergencia { get; set; }
        public DateTime? Fecha { get; set; }
        public string Contacto { get; set; }

        public virtual Direccion IdDirectorNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual NexoAlumno IdNexoNavigation { get; set; }
        public virtual Prioridad IdPrioridadNavigation { get; set; }
    }
}
