namespace chat.SignalR.Chat
{
    public interface IChatHub
    {
        Task sendMessageChat(ChatMethod method, string data);
    }
}
