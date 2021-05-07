using System;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.AspNetCore.SignalR;
using SharedEditorBackend.Models;
using SharedEditorBackend.Services;

namespace SharedEditorBackend.Hubs
{
    public class UserActionHub : Hub
    {
        private IDisposable subscription;

        public UserActionHub(ConnectionService connectionService) => subscription = connectionService.Connected
            .Select(ToUserAction)
            .SelectMany(NotifyUser)
            .Subscribe();

        private IObservable<Unit> NotifyUser(UserAction action) => Observable.FromAsync(() => Clients.All.SendAsync(action.ToString()));
        private UserAction ToUserAction(bool connected) => connected ? new UserJoinedAction() : new UserLeftAction();
    }
}