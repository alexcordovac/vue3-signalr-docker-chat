using ChatAPI.Enums;
using ChatAPI.Hubs;
using ChatAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IHubContext<ChatHub> _chatHubContext;

        public ChatController(IHubContext<ChatHub> chathub)
        {
            _chatHubContext = chathub;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send(MessageEnvelope envelope)
        {
            if(envelope.GroupType == GroupType.All)
                await _chatHubContext.Clients.All.SendAsync("GroupMessage", envelope.User.Username, envelope.Message);
            else
                await _chatHubContext.Clients.Group(envelope.GroupType.ToString()).SendAsync("GroupMessage", envelope);

            return Ok();
        }

        [HttpPost("SendToGroup")]
        public async Task<IActionResult> SendToGroup(MessageEnvelope envelope)
        {
            await _chatHubContext.Clients.Group(envelope.GroupType.ToString()).SendAsync("GroupMessage", envelope);

            return Ok();
        }



        

        //public async Task<IActionResult> AddToGroup(string groupName)
        //{
        //    var id = Request.Headers["x-signalr-connection"];
        //    await _chatHubContext.Groups.AddToGroupAsync(id, groupName);

        //    return Ok();
        //}

        //public async Task<IActionResult> RemoveFromGroup(string groupName)
        //{
        //    var id = Request.Headers["x-signalr-connection"];
        //    await _chatHubContext.Groups.RemoveFromGroupAsync(id, groupName);

        //    return Ok();
        //    //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        //}





    }
}
