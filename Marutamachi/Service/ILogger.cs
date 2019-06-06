using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroLog;

namespace Marutamachi.Service
{
    public interface ILogger
    {
        void WriteLine<T>(string msg, LogLevel logLevel = LogLevel.Trace, Exception exception = null);
    }
}
