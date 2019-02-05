using System;
using System.Collections.Generic;
using System.Text;


namespace WebShop.Log
{
    public interface IMessageLogger
    {
        void LogInfo(string source);
        void LogWarning();
        void LogError();
        void LogDebug();
        void LogFatal();
    }
}
