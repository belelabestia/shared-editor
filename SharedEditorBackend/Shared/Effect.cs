using System;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;

namespace SharedEditorBackend.Shared
{
    public abstract class Effect<Model>
    {
        protected Subject<Model> subject;
        protected IHubContext<Trigger<Model>> context;

        public Effect(
            Subject<Model> subject,
            IHubContext<Trigger<Model>> context
        ) => (this.subject, this.context) = (subject, context);

        public abstract void Activate();
        public abstract void Deactivate();
    }
}