namespace BOS.Webclient.Models.Messaging
{
    public class Message
    {
        public string Key { get; private set; }
        public string Text { get; private set; }

        public static Message For(string key, string text)
        {
            return new Message
            {
                Key = key,
                Text = text
            };
        }

        public override bool Equals(object obj)
        {
            var compare = (Message) obj;
            return compare?.Key == Key && compare?.Text == Text;
        }

        public override int GetHashCode()
        {
            var id = $"{ Key }-{ Text }";
            return id.GetHashCode();
        }
    }
}