using ChatAPI.Enums;

namespace ChatAPI.Models
{
    public class MessageEnvelope
    {

        //private MessageType type;

        //public string Type
        //{
        //    get { return type.ToString(); }
        //    set 
        //    {
        //        Enum.TryParse("Active", out MessageType messageType);
        //        type = messageType;
        //    }
        //}

        public MessageType? MessageType { get; set; }
        public GroupType? GroupType { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;

    }
}
