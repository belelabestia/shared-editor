using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;

namespace SharedEditorBackend.Shared
{
    public class Trigger<State> : Hub
    {
        private Subject<State> subject;
        public Trigger(Subject<State> subject) => this.subject = subject;
        public void OnChange(State model) => subject.OnNext(model);
    }
}