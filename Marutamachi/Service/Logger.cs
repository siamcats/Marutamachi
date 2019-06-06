using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroLog;

namespace Marutamachi.Service
{
    public class Logger
    {
        public static Logger Instance { get; }

        public static bool Enabled { get; set; } = true;

        static Logger()
        {
            Instance = Instance ?? new Logger();
        }

        public void WriteLine<T>(string msg, LogLevel logLevel = LogLevel.Trace, Exception exception = null)
        {
            var logger = LogManagerFactory.DefaultLogManager.GetLogger<T>();
            logger.Trace(msg);
        }
    }
}
