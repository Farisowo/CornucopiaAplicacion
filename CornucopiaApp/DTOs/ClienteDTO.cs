using CommunityToolkit.Mvvm.ComponentModel;

namespace CornucopiaApp.DTOs
{
    public partial class ClienteDTO : ObservableObject
    {
        [ObservableProperty]
        public int idCliente;
        [ObservableProperty]
        public string nombreCliente;
        [ObservableProperty]
        public string apellidosCliente;
        [ObservableProperty]
        public string numeroTelefono;
    }
}
