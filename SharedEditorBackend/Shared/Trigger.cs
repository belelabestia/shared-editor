using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;

namespace SharedEditorBackend.Shared
{
    public class Trigger<Model> : Hub
    {
        private Subject<Model> subject;
        public Trigger(Subject<Model> subject) => this.subject = subject;
        public void OnModelChange(Model model) => subject.OnNext(model);
    }
}