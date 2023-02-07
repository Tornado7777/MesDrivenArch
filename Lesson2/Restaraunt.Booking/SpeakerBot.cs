using Messaging;
using Restaraunt.Booking.Model;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Restaraunt.Booking
{
    /// <summary>
    /// класс для взаимодействия с пользователем
    /// </summary>
    internal class SpeakerBot
    {
        private readonly Restaurant _restaraunt;
        private readonly TimeSpan interval = TimeSpan.FromMinutes(1);
        private const string _firstChoice = "1";
        private const string _secondChoice = "2";
        private readonly Random _rnd = new Random();
        public SpeakerBot(Restaurant restaraunt)
        {
            _restaraunt = restaraunt;

            CancelBookingTimer(interval);
        }
        /// <summary>
        /// Метод для запуска взаимодействия с пользователем
        /// </summary>
        public void InitialHello()
        {
            while (true)
            {
               Message.Send(Messages.InitialHelloChoice);

                var stopWatch = new Stopwatch();
                Thread.Sleep(1000 * 5);
                stopWatch.Start();
                switch (_rnd.Next(1,2).ToString())
                {
                    case _firstChoice:
                        ChooseTypeOfBooking();
                        break;
                    case _secondChoice:
                        ChooseTypeOfCancellation();
                        break;
                    default:
                        Message.Send(Messages.Input1Or2);
                        break;
                }

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;
                Message.Send($"{ts.Seconds:00}:{ts.Milliseconds:00}");
            }
        }

        private async void ChooseTypeOfBooking()
        {
            Message.Send(Messages.BookingChoice);
            bool validInput = false;
            while (!validInput)
            {
                switch (_rnd.Next(1, 2).ToString())
                {
                    case _firstChoice:
                        validInput = true;
                        Message.Send(Messages.WaitForBooking);
                        Message.Send(Messages.YoullBeNotified);
                        var table = await _restaraunt.BookFreeTableAsync(1);
                        NotifyOnBooking(table);
                        break;
                    case _secondChoice:
                        validInput = true;
                        Message.Send(Messages.WaitForBooking);
                        Message.Send(Messages.StayOnLine);
                        table = _restaraunt.BookFreeTable(1);
                        Message.Send(table is null
                            ? Messages.AllTablesOccupied
                            : Messages.BookingReady + table.Id);
                        break;
                    default:
                        Message.Send(Messages.Input1Or2);
                        break;
                }
            }
        }

        private async void ChooseTypeOfCancellation()
        {
            Message.Send(Messages.CancellationChoice);
            bool validInput = false;
            while (!validInput)
            {
                switch (_rnd.Next(1, 2).ToString())
                {
                    case _firstChoice:
                        validInput = true;
                        CancelAsync();
                        break;
                    case _secondChoice:
                        validInput = true;
                        Cancel();
                        break;
                    default:
                        Message.Send(Messages.Input1Or2);
                        break;
                }
            }
        }

        private async void CancelAsync()
        {
            bool validInput = false;
            while (!validInput)
            {
                Message.Send(Messages.WaitForCancellation);
                if (!int.TryParse(_rnd.Next(1,10).ToString(), out var tableId) || (tableId < 0 || tableId > 10))
                {
                    Message.Send(Messages.InputTableId);
                }
                else if (!_restaraunt.CheckIfBooked(tableId))
                {
                    validInput = true;
                    Message.Send(tableId + Messages.TableNotOccupied);
                }
                else
                {
                    validInput = true;
                    Message.Send(Messages.YoullBeNotified);
                    var table = await _restaraunt.CancelBookingAsync(tableId);
                    NotifyOnCancellation(table);
                }
            }
        }

        private void Cancel()
        {
            bool validInput = false;
            while (!validInput)
            {
                Message.Send(Messages.WaitForCancellation);
                if (!int.TryParse(_rnd.Next(1, 10).ToString(), out var tableId) || (tableId < 0 || tableId > 10))
                {
                    Message.Send(Messages.InputTableId);
                }
                else if (!_restaraunt.CheckIfBooked(tableId))
                {
                    validInput = true;
                    Message.Send(tableId + Messages.TableNotOccupied);
                }
                else
                {
                    validInput = true;
                    Message.Send(Messages.StayOnLine);
                    var table = _restaraunt.CancelBooking(tableId);
                    Message.Send(Messages.CancellationReady + table.Id);
                }
            }
        }

        private async void NotifyOnBooking(Table table)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1000);
                Message.Send(table is null
                    ? Messages.Notification + Messages.AllTablesOccupied
                    : Messages.Notification + Messages.BookingReady + table.Id);
            });
        }
        private async void NotifyOnCancellation(Table table)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1000);
                Message.Send(Messages.Notification + Messages.CancellationReady + table.Id);
            });
        }
        /// <summary>
        /// автоматически отменет бронь стола спустя какое-то время
        /// </summary>
        /// <param name="interval">интервал отмены брони</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CancelBookingTimer(TimeSpan interval, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                var delayTask = Task.Delay(interval, cancellationToken);
                var cancelledTable = _restaraunt.CancelBookingTimed();
                if (cancelledTable != null)
                {
                    Message.Send(Messages.AutoCancellation + cancelledTable.Id);
                }
                await delayTask;
            }
        }
    }
}
