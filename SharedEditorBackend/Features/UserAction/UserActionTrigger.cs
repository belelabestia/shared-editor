using System.Threading.Tasks;
using System;
using SharedEditorBackend.Shared;
using System.Reactive.Subjects;

namespace SharedEditorBackend.Features
{
    public class UserActionTrigger : Trigger<UserAction>
    {
        public override Task OnConnectedAsync() => Task.Run(UserJoined);
        public override Task OnDisconnectedAsync(Exception exception) => Task.Run(UserLeft);
        public UserActionTrigger(Subject<UserAction> subject) : base(subject) { }
        private void UserJoined() => OnChange(UserAction.Joined);
        private void UserLeft() => OnChange(UserAction.Left);
    }
}