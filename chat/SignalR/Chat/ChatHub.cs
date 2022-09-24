using Microsoft.AspNetCore.SignalR;

namespace chat.SignalR.Chat
{
   

    public class ChatHub : Hub<IChatHub>
    {

        //public async Task SendMessage(string user, string message) => await Clients.All.SendAsync("ReceiveMessage", user, message);

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).sendMessageChat(ChatMethod.JoinRoom, $"{Context.ConnectionId} has joined the group {roomName}.");
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).sendMessageChat(ChatMethod.JoinRoom, $"{Context.ConnectionId} has left the group {roomName}.");
        }
    }
}
