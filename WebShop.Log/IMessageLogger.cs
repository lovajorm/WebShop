using System;
using System.Collections.Generic;
using System.Text;


namespace WebShop.Log
{
    public interface IMessageLogger
    {
        void LogInfo(string source, string message);
        void LogWarning(string source, Exception ex);
        void LogError(string source, Exception ex);
        void LogDebug(string source, Exception ex);
        void LogFatal(string source, Exception ex);
    }
}
