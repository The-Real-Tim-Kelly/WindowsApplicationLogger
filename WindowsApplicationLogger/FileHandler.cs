using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplicationLogger
{
    internal class FileHandler
    {
        public Stream? IsFileLocked(string path, FileAccess fileAccess)
        {
            Stream? stream = null;
            try
            {
                var file = new FileInfo(path);
                if (!file.Exists) return file.Create();
                stream = file.Open(FileMode.Open, fileAccess, FileShare.Read);
            } catch(IOException) {
                return stream;
            }
            return stream;
        }

        public Stream? WaitUntilFileUnlocked(string file, int attempts, int attemptWait, FileAccess fileAccess)
        {
            int i = 0;
            while (i < attempts)
            {
                Stream? fs = null;
                fs = IsFileLocked(file, fileAccess);
                if(fs != null)
                {
                    return fs;
                }
                Task.Delay(attemptWait).Wait();
                i++;
            }
            return null;
        }
    }
}
