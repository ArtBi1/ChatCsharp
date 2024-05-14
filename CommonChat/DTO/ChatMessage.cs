using System.Text.Json;

namespace CommonChat.DTO
{
    public class ChatMessage
    {
        public int? Id { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string Text { get; set; }
        public Command Command { get; set; }
        public string ToJson() => JsonSerializer.Serialize(this);
        public static ChatMessage FromJson(string text) => JsonSerializer.Deserialize<ChatMessage>(text);
    }

    public enum Command
    {
        Message,
        Confirmation,
        Register
    }
}