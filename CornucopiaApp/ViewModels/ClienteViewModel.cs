using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using CornucopiaApp.DataAccess;
using CornucopiaApp.DTOs;
using CornucopiaApp.Utilidades;
using CornucopiaApp.Modelos;

namespace CornucopiaApp.ViewModels
{
    public partial class ClienteViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ClienteDBContext _dbContext;

        [ObservableProperty]
        private ClienteDTO clienteDto = new ClienteDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IdCliente;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public ClienteViewModel(ClienteDBContext context)
        {
            _dbContext = context;
            //ClienteDto
        }
        

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            
            var id = int.Parse(query["id"].ToString());
            IdCliente = id;

            if (IdCliente == 0)
            {
                TituloPagina = "Nuevo Cliente";
            } else
            {
                TituloPagina = "Editar Cliente";
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Clientes.FirstAsync(e => e.IdCliente == IdCliente);
                    ClienteDto.NombreCliente = encontrado.NombreCliente;
                    ClienteDto.ApellidosCliente = encontrado.ApellidosCliente;
                    ClienteDto.NumeroTelefono = encontrado.NumeroTelefono;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });
                });
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            loadingEsVisible = true;
            ClienteMensaje mensaje = new ClienteMensaje();
            await Task.Run(async () =>
            {
                if(IdCliente == 0)
                {
                    var tbCliente = new Cliente
                    {
                        NombreCliente = ClienteDto.NombreCliente,
                        ApellidosCliente = ClienteDto.ApellidosCliente,
                        NumeroTelefono = ClienteDto.NumeroTelefono,
                    };

                    _dbContext.Clientes.Add(tbCliente);
                    await _dbContext.SaveChangesAsync();

                    ClienteDto.IdCliente = tbCliente.IdCliente;

                    mensaje = new ClienteMensaje()
                    {
                        EsCrear = true,
                        ClienteDto = ClienteDto
                    };

                } else
                {
                    var encontrado = await _dbContext.Clientes.FirstAsync(e => e.IdCliente == IdCliente);
                    encontrado.NombreCliente = ClienteDto.NombreCliente;
                    encontrado.ApellidosCliente = ClienteDto.ApellidosCliente;
                    encontrado.NumeroTelefono = ClienteDto.NumeroTelefono;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new ClienteMensaje()
                    {
                        EsCrear = false,
                        ClienteDto = ClienteDto
                    };

                }


                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new ClienteMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });

            });
        }

    }
}
