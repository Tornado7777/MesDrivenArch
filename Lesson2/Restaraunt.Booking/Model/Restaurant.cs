using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaraunt.Booking.Model
{
    public class Restaurant
    {
        private readonly List<Table> _tables = new List<Table>();
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(true);
        public Restaurant() 
        {
            for (ushort i = 1; i <= 10; i++)
            {
                _tables.Add(new Table(i));
            }
        }

        /// <summary>
        /// Забронировать свободный столик по количеству гостей, синхронно
        /// </summary>
        /// <param name="countOfGuests">количество гостей</param>
        /// <returns>забронированный стол</returns>
        public Table BookFreeTable(int countOfGuests)
        {
            _autoResetEvent.WaitOne();
            var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfGuests && t.State == State.Free);
            Thread.Sleep(1000 * 5);
            table?.SetState(State.Booked);
            _autoResetEvent.Set();
            return table;
        }
        /// <summary>
        /// Забронировать свободный столик по количеству гостей, асинхронно
        /// </summary>
        /// <param name="countOfGuests">количество гостей</param>
        /// <returns>забронированный стол</returns>
        public async Task<Table> BookFreeTableAsync(int countOfGuests)
        {
            _autoResetEvent.WaitOne();
            var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfGuests && t.State == State.Free);
            await Task.Delay(1000 * 5);
            table?.SetState(State.Booked);
            _autoResetEvent.Set();
            return table;
        }
        /// <summary>
        /// отменить бронь стола, синхронно
        /// </summary>
        /// <param name="tableId">номер стола</param>
        /// <returns>стол, на котором была отменена бронь</returns>
        public Table CancelBooking(int tableId)
        {
            var table = _tables.Find(t => t.Id == tableId);
            Thread.Sleep(1000 * 5);
            table?.SetState(State.Free);
            return table;
        }
        /// <summary>
        /// отменить бронь стола, асинхронно
        /// </summary>
        /// <param name="tableId">номер стола</param>
        /// <returns>стол, на котором была отменена бронь</returns>
        public async Task<Table> CancelBookingAsync(int tableId)
        {
            var table = _tables.Find(t => t.Id == tableId);
            await Task.Delay(1000 * 5);
            table?.SetState(State.Free);
            return table;

        }
        /// <summary>
        /// проверка, забронирован ли стол
        /// </summary>
        /// <param name="tableId">номер стола</param>
        /// <returns></returns>
        public bool CheckIfBooked(int tableId)
        {
            var table = _tables.Find(t => t.Id == tableId);
            return (table.State == State.Booked);
        }
        /// <summary>
        /// метод для повторяющейся отмены брони после определенного времени
        /// </summary>
        /// <returns>стол с отмененной бронью</returns>
        public Table CancelBookingTimed()
        {
            var table = _tables.Find(t => t.State == State.Booked);
            if (table != null)
            {
                table?.SetState(State.Free);
            }
            return table;
        }
    }
}
