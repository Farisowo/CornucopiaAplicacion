using.System.ComponentModel.DataAnnotations;


namespace CornucopiaApp.Modelos
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string NumeroTelefono { get; set; }


    }
}
