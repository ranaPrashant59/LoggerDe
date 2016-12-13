using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThreadedLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new Logger();
            LoggerDemo demo = new LoggerDemo(logger);
            demo.CreateLogs();
        }
    }
}
