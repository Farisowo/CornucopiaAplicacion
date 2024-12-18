using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CornucopiaApp.Utilidades
{
    public class ClienteMensajeria : ValueChangedMessage<ClienteMensaje>
    {
        public ClienteMensajeria(ClienteMensaje value) : base(value) 
        { 

        }
    }
}
