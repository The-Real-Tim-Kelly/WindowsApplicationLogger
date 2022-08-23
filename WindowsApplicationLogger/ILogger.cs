using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplicationLogger
{
    public interface ILogger
    {
        string LogDirectory { get; }
        void Log(object logMessage);
        void Log(LogItem logItem);
        void Log(object logMessage, string path);
        void Log(LogItem logItem, string path);
        void EventLog(LogItem logItem, EventLogEntryType type, int eventId, short category);
        LogItem GetLogItem(object logMessage);
    }
}
