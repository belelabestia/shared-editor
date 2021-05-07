using System.Reactive.Subjects;
using System;
using System.Reactive.Linq;

namespace SharedEditorBackend.Services
{
    public class ConnectionService
    {
        private Subject<bool> connected = new();
        public IObservable<bool> Connected => connected.AsObservable();
        public void SetConnectionState(bool state) => this.connected.OnNext(state);
    }
}