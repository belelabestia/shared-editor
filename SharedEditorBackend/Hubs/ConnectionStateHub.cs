using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using SharedEditorBackend.Services;

namespace SharedEditorBackend.Hubs
{
    public class ConnectionStateHub : Hub
    {
        private ConnectionService connectionService;
        public override Task OnConnectedAsync() => Task.Run(SetConnected);
        public override Task OnDisconnectedAsync(Exception exception) => Task.Run(SetDisconnected);
        public ConnectionStateHub(ConnectionService connectionService, UserActionNotificationService _) => this.connectionService = connectionService;
        private void SetConnected() => connectionService.SetConnectionState(true);
        private void SetDisconnected() => connectionService.SetConnectionState(false);
    }
}