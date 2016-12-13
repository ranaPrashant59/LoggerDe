using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MultiThreadedLogger
{
   public class LoggerDemo
    {
       private ILogger Log { get; set; }

        #region Constructor

       public LoggerDemo(ILogger logger)
       {
           Log = logger;
       }
        #endregion

        #region Public Methods

       public void CreateLogs()
       {
           ConcurrentBag<string> bag = new ConcurrentBag<string>();
           List<string> namesList = new List<string> { "Danny",  "Rannu" };
           List<Task> taskList = new List<Task>();
           foreach (string name in namesList)
           {
               Task task = Task.Factory.StartNew(() =>
                   {
                       Log.WriteInfo(name);
                       Log.WriteWarning(name);
                       Log.WriteError(name);
                       bag.Add(name);
                   });
               taskList.Add(task);
               task.Wait();
           }
           Task.WaitAll(taskList.ToArray());
           foreach (Task item in taskList)
               Console.WriteLine("Task {0} Status {1}", item.Id, item.Status);
           Console.WriteLine("Number of files read: {0} ",bag.Count);
           Console.ReadLine();
       }
        #endregion
    }
}
