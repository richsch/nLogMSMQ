﻿using System;
using NLog;
using NR.QueueWatcher.UI;

namespace MSMQLoggingPOC
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            Console.WriteLine("Writing log items:");
            logger.Info("This is an info item");
            logger.Warn("this is a warn item");
            logger.Error("This is an error item");

            Console.WriteLine("Now writing to message queue:");
//            using (var queue = new MessageQueue(@"FormatName:DIRECT=OS:server01\private$\tasi_log"))
//            using (var queue = new MessageQueue(@"FormatName:DIRECT=OS:richardschr53c2\private$\nlogpoc"))
//            {
//                var message = new Message("TEST");
//                message.Formatter = new BinaryMessageFormatter();
//                queue.Send(message);
//            } 

            var queueWatcher = new QueueWatcher();
            var entryes = queueWatcher.GetLogEntries();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
