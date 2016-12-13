using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadedLogger
{
   public class Logger :ILogger
    {
       private readonly BlockingCollection<LogParameters> _errorCollectionList = new BlockingCollection<LogParameters>();
       private readonly ReaderWriterLockSlim _fileLock = new ReaderWriterLockSlim();
       string _filePath = @"C:\Logs\Log.txt";

       #region Constructor

       public Logger()
       {
           GenerateLog();
       }

       ~Logger()
       {
           //Free the writing thread
           _errorCollectionList.CompleteAdding();
       }

       #endregion

       #region Private Methods

       private void GenerateLog()
       {
           Task.Factory.StartNew(() =>
           {
               foreach (LogParameters item in _errorCollectionList.GetConsumingEnumerable())
               {
                   string message = string.Empty;
                   switch (item.Type)
                   {
                       case LogParameters.LogType.Info:
                           message = string.Format("[{0}] Information: {1}", LogTimeStamp(), item.Message);
                           Log(message);
                           break;
                       case LogParameters.LogType.Warning:
                           message = string.Format("[{0}] Error: {1}", LogTimeStamp(), item.Message);
                           Log(message);
                           break;
                       case LogParameters.LogType.Error:
                           message = string.Format("[{0}] Warning {1}", LogTimeStamp(), item.Message);
                           Log(message);
                           break;
                       default:
                           break;
                   }
               }
           });
       }

       private string LogTimeStamp()
       {
           return DateTime.Now.ToShortTimeString();
       }

       private void Log(string message)
       {
           _fileLock.EnterWriteLock();
           try
           {
               if (File.Exists(_filePath) && File.GetCreationTime(_filePath).Date.AddDays(1) > DateTime.Now.Date)
               {
                   using (StreamWriter writer = File.AppendText(_filePath))
                   {
                       writer.WriteLine(message);
                   }
               }
               else
               {
                   if (!Directory.Exists(@"C:\Logs"))
                       Directory.CreateDirectory(@"C:\Logs");
                   using (StreamWriter file = new StreamWriter(_filePath, false, System.Text.Encoding.UTF8))
                   {
                       file.WriteLine(message);
                   }
               }
           }
           finally
           {
               _fileLock.ExitWriteLock();
           }
       }

       #endregion

       #region ILogger
       //Just callthese methods to log something

       public void WriteInfo(string infoMsg)
        {
            LogParameters log = new LogParameters(LogParameters.LogType.Info, infoMsg);
            _errorCollectionList.Add(log);
        }

        public void WriteError(string errorMsg)
        {
            LogParameters log = new LogParameters(LogParameters.LogType.Error, errorMsg);
            _errorCollectionList.Add(log);
        }

        public void WriteWarning(string warningMsg)
        {
            LogParameters log = new LogParameters(LogParameters.LogType.Warning, warningMsg);
            _errorCollectionList.Add(log);
        } 
        #endregion
    }
}
