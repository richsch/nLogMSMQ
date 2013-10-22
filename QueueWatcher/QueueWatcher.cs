using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Messaging;

namespace NR.QueueWatcher.UI
{
    public class QueueWatcher
    {
        public IEnumerable<LogEntry> GetLogEntries()
        {
            var queueAddress = ConfigurationManager.AppSettings["queueName"];
            var messages = new List<Message>();
            using (var queue = new MessageQueue(queueAddress))
            {
                var enumerator = queue.GetMessageEnumerator2();
                while (enumerator.MoveNext(new TimeSpan(0, 0, 1)))
                {
                    messages.Add(enumerator.Current);
                }
            }
            return ParseMessages(messages);
        }

        private IEnumerable<LogEntry> ParseMessages(IEnumerable<Message> messages)
        {
            return messages.Where(m => m.BodyType == 0).Select(ParseMessage).ToList();
        }

        private LogEntry ParseMessage(Message message)
        {
            var textReader = new StreamReader(message.BodyStream);
            var msg = textReader.ReadToEnd();

            var items = msg.Split('|');

            return new LogEntry()
                {
                    RawText = msg,
                    Timestamp = DateTime.Parse(items[0]),
                    Level = items[1],
                    Application = items[2],
                    Message = items[3]
                };
        } 
    }
}
