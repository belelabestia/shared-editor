using System.Reactive.Subjects;

namespace SharedEditorBackend.Shared
{
    public abstract class Effect<Model>
    {
        protected Subject<Model> subject;
        public Effect(Subject<Model> subject) => this.subject = subject;
        public abstract void Activate();
        public abstract void Deactivate();
    }
}