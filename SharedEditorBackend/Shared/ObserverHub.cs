using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;

namespace SharedEditorBackend.Shared
{
    public class ObserverHub<Model> : Hub
    {
        private Subject<Model> subject;
        public ObserverHub(Subject<Model> subject) => this.subject = subject;
        public void OnModelChange(Model model) => subject.OnNext(model);
    }
}