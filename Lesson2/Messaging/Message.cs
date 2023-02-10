
namespace Messaging
{
    public static class Message
    {
        private static readonly Producer _producer =
            new("BookingNotificationFanout", "localhost"); 
       //private static readonly Consumer _consumer = 
       //     new("BookingNotification", "localhost");

        public static void Send(string message)
        {
            _producer.Send(message);
        }
    }
}
