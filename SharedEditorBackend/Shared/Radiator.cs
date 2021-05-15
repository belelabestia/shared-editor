using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;

namespace SharedEditorBackend.Shared
{
    public class Radiator<Model> : Effect<Model>
    {
        private string Endpoint;
        private IDisposable subscription;
        public Radiator(Subject<Model> subject, IHubContext<ObserverHub<Model>> context, string endpoint) : base(subject, context) => this.Endpoint = endpoint;
        public override void Activate() => subscription = subject.SelectMany(Radiate).Subscribe();
        public override void Deactivate() => subscription.Dispose();
        private IObservable<Unit> Radiate(Model model) => Observable.FromAsync(() => context.Clients.All.SendAsync(Endpoint, model));
    }
}