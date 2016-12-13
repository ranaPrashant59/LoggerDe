
namespace MultiThreadedLogger
{
   public interface ILogger
    {
       void WriteInfo(string infoMsg);
       void WriteError(string errorMsg);
       void WriteWarning(string warningMsg);
    }
}
