using System;
using System.Diagnostics;


namespace RestaurantDataAccessLayer
{
    internal class clsLogger
    {
        private static string SourceName = "E-Commerce_Exception";
        private static EventLogEntryType _eventLogEntryType;
        private static string _eventLogEntryTypeText
        {
            get
            {
                switch (_eventLogEntryType)
                {
                    case EventLogEntryType.Error:
                        return "ERROR";
                    case EventLogEntryType.Warning:
                        return "WARNING";
                    case EventLogEntryType.Information:
                        return "INFORMATION";
                    default:
                        return "WARNING";
                }
            }
        }

        public static void ErrorMessage(string message, Exception ex, EventLogEntryType eventLogEntryType)
        {
            _eventLogEntryType = eventLogEntryType;

            var stackTrace = new StackTrace();
            var callingFrame = new StackFrame(1);
            var method = callingFrame.GetMethod();
            var className = method.DeclaringType.Name;
            var methodName = method.Name;

            string fullMessage =
                $"[{_eventLogEntryTypeText} LOG]\n" +
                $"Class: {className}\n" +
                $"Method: {methodName}\n" +
                $"Message: {message}\n" +
                $"Exception: {ex.Message}\n" +
                $"StackTrace:\n{ex.StackTrace}";

            EventLog.WriteEntry(SourceName, fullMessage, eventLogEntryType);
        }


    }
}
