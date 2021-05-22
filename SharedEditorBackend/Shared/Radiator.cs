using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;

namespace SharedEditorBackend.Shared
{
    public class Radiator<Model> : Effect<Model>
    {
        private string endpoint;
        private IDisposable subscription;
        private IHubContext<Trigger<Model>> context;
        public Radiator(Subject<Model> subject, IHubContext<Trigger<Model>> context, string endpoint) : base(subject) => (this.endpoint, this.context) = (endpoint, context);
        public override void Activate() => subscription = subject.SelectMany(Radiate).Subscribe();
        public override void Deactivate() => subscription.Dispose();
        private IObservable<Unit> Radiate(Model model) => Observable.FromAsync(() => context.Clients.All.SendAsync(endpoint, model));
    }
}