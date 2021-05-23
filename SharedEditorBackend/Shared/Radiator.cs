using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;

namespace SharedEditorBackend.Shared
{
    public class Radiator<Model> : Effect<Model>
    {
        private readonly string endpoint;
        private readonly IDisposable subscription;
        private readonly IHubContext<Trigger<Model>> context;

        public Radiator(Subject<Model> subject, IHubContext<Trigger<Model>> context, string endpoint) : base(subject) 
        {
            (this.endpoint, this.context) = (endpoint, context);
            subscription = subject.SelectMany(Radiate).Subscribe();
        }

        private IObservable<Unit> Radiate(Model model) => Observable.FromAsync(() => context.Clients.All.SendAsync(endpoint, model));
        public override void Dispose() => subscription.Dispose();
    }
}