namespace stopBullying.Comandos
{
public class ComandoActualizarDenuncia
{
        public int IdDenuncia { get; set; }
       public int IdEstado { get; set; }
        public string Notas { get; set; }
        public int IdPrioridad {get;set;}
        public int IdDirector { get; set; }
        public int IdNexo { get; set; }
}
}