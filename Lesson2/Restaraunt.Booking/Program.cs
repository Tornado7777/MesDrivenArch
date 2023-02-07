using Restaraunt.Booking.Model;
using System;

namespace Restaraunt.Booking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var rest = new Restaurant();
            var speakerBot = new SpeakerBot(rest);

            speakerBot.InitialHello();
        }
    }
}
