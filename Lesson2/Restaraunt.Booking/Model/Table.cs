using System;

namespace Restaraunt.Booking.Model
{
    public class Table
    {
        /// <summary>
        /// Стостояние стола (свободен/занят)
        /// </summary>
        public State State { get; private set; }

        /// <summary>
        /// Кол-во мест за столиком
        /// </summary>
        public int SeatsCount { get; }

        /// <summary>
        /// Номер столика
        /// </summary>
        public int Id { get; }
        public Table(int id) 
        {
            var rnd = new Random();
            Id = id;
            State = State.Free; // новый стол всегда свободен
            SeatsCount = rnd.Next(2, 5); //кол-во мест за каждым столиком
        }

        public bool SetState(State state)
        {
            if (state == State)
            {
                return false;
            }

            State = state;
            return true;
        }
    }

    
    public enum State
    {
        /// <summary>
        /// Стол своббоден
        /// </summary>
        Free = 0,
        /// <summary>
        /// Стол занят
        /// </summary>
        Booked = 1,
    }
}
