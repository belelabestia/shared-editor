using System;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.AspNetCore.SignalR;
using SharedEditorBackend.Services;

namespace SharedEditorBackend.Hubs
{
    public class NotifyUserLeftHub : Hub
    {
        private const string USER_LEFT = "USER_LEFT";
        private IDisposable subscription;
        public NotifyUserLeftHub(ConnectionService connectionService) => subscription = connectionService.Connected
            .Where(connected => !connected)
            .SelectMany(NotifyUserLeft)
            .Subscribe();

        public new void Dispose() => subscription.Dispose();

        private IObservable<Unit> NotifyUserLeft(bool connected) => Observable.FromAsync(() => Clients.All.SendAsync(USER_LEFT));
    }
}