
namespace MultiThreadedLogger
{
   internal class LogParameters
   {
       internal enum LogType { Info, Warning, Error };
       internal LogType Type { get; set; }
       internal string Message { get; set; }

       #region Constructor
       public LogParameters(LogType logType,string logMsg)
       {
           Type = logType;
           Message = logMsg;
       }
       #endregion
   }
}
