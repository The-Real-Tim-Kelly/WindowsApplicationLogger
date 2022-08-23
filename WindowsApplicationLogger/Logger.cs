using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplicationLogger
{
    public class Logger : ILogger
    {
        public string LogDirectory { get; set; } = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)}\\Logs";

        private FileHandler fileHandler = new FileHandler();
        public void EventLog(LogItem logItem, EventLogEntryType type, int eventId, short category)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            EventLog log = new("Application");
            log.Source = "Application";
            log.WriteEntry(logItem.ToString(), type, eventId, category);
        }

        public LogItem GetLogItem(object logMessage)
        {
            string computerName = Environment.MachineName ?? string.Empty;
            string appVersion = Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
            string appName = Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;
            string userName = Environment.UserName ?? string.Empty;
            DateTime date = DateTime.Now;
            var log = new LogItem(computerName, logMessage?.ToString() ?? string.Empty, appVersion, appName, userName, date);
            return log;
        }

        public void Log(object logMessage)
        {
            Log(logMessage, LogDirectory);
        }

        public void Log(LogItem logItem)
        {
            Log(logItem, LogDirectory);
        }

        public void Log(object logMessage, string path)
        {
            Log(GetLogItem(logMessage), path);
        }

        public void Log(LogItem logItem, string path)
        {
            var file = Path.Combine(path, $"{AppDomain.CurrentDomain.FriendlyName}_{DateTime.Today.ToString("M-dd-yyyy")}.log");
            Directory.CreateDirectory(path);
            var stream = fileHandler.WaitUntilFileUnlocked(file, 10, 100, FileAccess.ReadWrite);
            if (stream == null) return;
            var streamwriter = new StreamWriter(stream);
            streamwriter.Write(logItem);
            streamwriter.Close();
        }
    }
}
