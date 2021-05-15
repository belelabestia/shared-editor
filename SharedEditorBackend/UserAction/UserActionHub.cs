using System.Threading.Tasks;
using System;
using SharedEditorBackend.Shared;
using System.Reactive.Subjects;

namespace SharedEditorBackend
{
    public class UserActionHub : ObserverHub<UserAction>
    {
        public override Task OnConnectedAsync() => Task.Run(UserJoined);
        public override Task OnDisconnectedAsync(Exception exception) => Task.Run(UserLeft);
        public UserActionHub(Subject<UserAction> subject) : base(subject) { }
        private void UserJoined() => OnModelChange(UserAction.Joined);
        private void UserLeft() => OnModelChange(UserAction.Left);
    }
}