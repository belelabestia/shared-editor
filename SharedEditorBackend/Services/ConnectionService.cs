using System.Reactive.Subjects;
using System;
using System.Reactive.Linq;

namespace SharedEditorBackend.Services
{
    public class ConnectionService
    {
        private BehaviorSubject<bool> connected = new(false);
        public IObservable<bool> Connected => connected
            .AsObservable()
            .DistinctUntilChanged();

        public void SetConnectionState(bool state) => this.connected.OnNext(state);
    }
}