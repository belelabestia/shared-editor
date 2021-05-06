using System;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.AspNetCore.SignalR;
using SharedEditorBackend.Services;

namespace SharedEditorBackend.Hubs
{
    public class NotifyUserJoinedHub : Hub
    {
        private const string USER_JOINED = "USER_JOINED";
        private IDisposable subscription;
        public NotifyUserJoinedHub(ConnectionService connectionService) => subscription = connectionService.Connected
            .Where(connected => connected)
            .SelectMany(NotifyUserJoined)
            .Subscribe();

        public new void Dispose() => subscription.Dispose();

        private IObservable<Unit> NotifyUserJoined(bool connected) => Observable.FromAsync(() => Clients.All.SendAsync(USER_JOINED));
    }
}