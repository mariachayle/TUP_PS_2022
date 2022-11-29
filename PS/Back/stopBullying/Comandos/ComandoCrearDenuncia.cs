using System;
namespace stopBullying.Comandos
{
public class ComandoCrearDenuncia
{
       public int IdPrioridad { get; set; }
       public int IdEstado { get; set; }
        public string NombreDenunciante { get; set; }
        public string NombreObservador { get; set; }
        public string NombreAgresor { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public bool Emergencia { get; set; }
        public DateTime? Fecha { get; set; }
         public string Contacto { get; set; }
}
}