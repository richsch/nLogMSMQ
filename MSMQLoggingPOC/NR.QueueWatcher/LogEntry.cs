using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR.QueueWatcher
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
