using System;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.AspNetCore.SignalR;
using SharedEditorBackend.Hubs;
using SharedEditorBackend.Models;

namespace SharedEditorBackend.Services
{
    public class UserActionNotificationService
    {
        public UserActionNotificationService(
            ConnectionService connectionService,
            IHubContext<ConnectionStateHub> context
        ) => NotifyUserActionsToClients(connectionService.Connected, context.Clients);

        private void NotifyUserActionsToClients(IObservable<bool> connectedStream, IHubClients clients)
        {
            connectedStream
                .Select(ToUserAction)
                .SelectMany(NotifyToClients(clients))
                .Subscribe();
        }

        private Func<UserAction, IObservable<Unit>> NotifyToClients(IHubClients clients) => (UserAction action) => Observable.FromAsync(() => clients.All.SendAsync(action.ToString()));
        private UserAction ToUserAction(bool connected) => connected ? new UserJoinedAction() : new UserLeftAction();
    }
}