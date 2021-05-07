namespace SharedEditorBackend.Models
{
    public abstract class UserAction
    {
        public new abstract string ToString();
    }

    public class UserJoinedAction : UserAction
    {
        public override string ToString() => "USER_JOINED";
    }

    public class UserLeftAction : UserAction
    {
        public override string ToString() => "USER_LEFT";
    }
}