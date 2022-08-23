using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplicationLogger
{
    public class LogItem
    {
        private string computerName;
        private string logMessage;
        private string appVersion;
        private string appName;
        private string userName;
        private DateTime date;

        public LogItem(string computerName, string logMessage, string appVersion, string appName, string userName, DateTime date)
        {
            this.computerName = computerName;
            this.logMessage = logMessage;
            this.appVersion = appVersion;
            this.appName = appName;
            this.userName = userName;
            this.date = date;
        }

        public override string ToString() =>
            $"Computer Name: {computerName}\n" +
            $"User: {userName}\tDate: {date}\n" +
            $"Application: {appName}\tVersion: {appVersion}\n" +
            $"Message:\n{logMessage}";
        
    }
}
