
namespace Restaraunt.Booking
{
    /// <summary>
    /// фразы, которые использует SpeakerBot. Вынесенны в отдельный класс, чтобы удобнее было редактировать, не залезая в основную логику программы. 
    /// </summary>
    public static class Messages
    {
        public static readonly string Notification = "УВЕДОМЛЕНИЕ:";
        public static readonly string WaitForBooking = "Подождите секунду, я подберу столик и подтвержу вашу бронь,";
        public static readonly string WaitForCancellation = "Назовите номер стола и мы отменим бронь.";
        public static readonly string StayOnLine = "Оставайтесь на линии";
        public static readonly string YoullBeNotified = "Вам придёт уведомление";
        public static readonly string AllTablesOccupied = "К сожалению, сейчас все столики заняты";
        public static readonly string BookingReady = "Готово! Забронировали столик под номером ";
        public static readonly string CancellationReady = "Готово! Отменили бронь столика под номером ";
        public static readonly string InitialHelloChoice = "Привет! Желаете забронировать столик или отменить бронь?" +
                                                            "\n1 - забронировать" +
                                                            "\n2 - отменить бронь";
        public static readonly string InitialHello = "Привет! Желаете забронировать столик или отменить бронь?";
        public static readonly string BookingChoice = "Отлично! Каким способом было бы удобнее забронировать столик?" +
                                                      "\n1 - мы уведомим вас по смс (асинхронно)" +
                                                      "\n2 - подождите на линии, мы Вас оповестим (синхронно)";
        public static readonly string CancellationChoice = "Отлично! Каким способом было бы удобнее отменить бронь?" +
                                                            "\n1 - мы уведомим вас по смс (асинхронно)" +
                                                            "\n2 - подождите на линии, мы Вас оповестим (синхронно)";
        public static readonly string Input1Or2 = "Введите, пожалуйста 1 или 2";
        public static readonly string InputTableId = "Введите, пожалуйста, номер стола от 1 до 10";
        public static readonly string TableDoesntExist = "У нас нет столика с таким номером";
        public static readonly string TableNotOccupied = " столик не занят";
        public static readonly string AutoCancellation = "Была автоматически отменена бронь столика под номером ";


    }    
}
