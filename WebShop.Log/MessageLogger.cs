using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace WebShop.Log
{
    public class MessageLogger :  IMessageLogger
    {
        private static log4net.ILog Log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MessageLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

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
