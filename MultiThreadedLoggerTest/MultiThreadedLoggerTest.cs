using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MultiThreadedLogger;

namespace MultiThreadedLoggerTest
{
    [TestClass]
    public class MultiThreadedLoggerTest
    {
        [TestMethod]
        public void TestWriteInfo()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            logger.Setup(s => s.WriteInfo(It.IsAny<string>()));
            LoggerDemo demo = new LoggerDemo(logger.Object);
            //Act
            demo.CreateLogs();
            //Assert
            logger.Verify(s=> s.WriteInfo(It.IsAny<string>()),Times.AtLeastOnce);
        }

        [TestMethod]
        public void TestWriteError()
        {
                        //Arrange
            var logger = new Mock<ILogger>();
            logger.Setup(s => s.WriteError(It.IsAny<string>()));
            LoggerDemo demo = new LoggerDemo(logger.Object);
            //Act
            demo.CreateLogs();
            //Assert
            logger.Verify(s=> s.WriteInfo(It.IsAny<string>()),Times.AtLeastOnce);
        }

        [TestMethod]
        public void TestWriteWarning()
        {
                        //Arrange
            var logger = new Mock<ILogger>();
            logger.Setup(s => s.WriteWarning(It.IsAny<string>()));
            LoggerDemo demo = new LoggerDemo(logger.Object);
            //Act
            demo.CreateLogs();
            //Assert
            logger.Verify(s=> s.WriteInfo(It.IsAny<string>()),Times.AtLeastOnce);
        }
    }
}
