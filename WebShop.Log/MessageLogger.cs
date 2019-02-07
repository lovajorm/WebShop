using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace WebShop.Log
{
    public class MessageLogger :  IMessageLogger
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void LogInfo(string source, string message)
        {
            Log.Info($"{source} - {message}");
        }

        public void LogWarning(string source, Exception ex = null)
        {
            Log.Warn(source, ex);
        }

        public void LogError(string source, Exception ex = null)
        {
            Log.Error(source, ex);
        }

        public void LogDebug(string source, Exception ex = null)
        {
            Log.Debug(source, ex);
        }

        public void LogFatal(string source, Exception ex = null)
        {
            Log.Fatal(source, ex);
        }
    }
}
