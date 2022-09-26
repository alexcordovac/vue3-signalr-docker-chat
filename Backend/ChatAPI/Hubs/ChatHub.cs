using ChatAPI.Enums;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub
    {

        //public async Task AddToGroupAsync(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        //}

        //public async Task RemoveFromGroupAsync(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        //}

        public override async Task OnConnectedAsync()
        {
            //Add connection to all groups
            var idconn = Context.ConnectionId;

            foreach (GroupType gtype in (GroupType[])Enum.GetValues(typeof(GroupType)))
            {
                await this.Groups.AddToGroupAsync(idconn, gtype.ToString());
            }

            base.OnConnectedAsync();
        }

    }
}
