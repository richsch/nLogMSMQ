using System;

namespace NR.QueueWatcher.UI
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Application { get; set; }
        public string Message { get; set; }

        public string RawText { get; set; }
    }
}
