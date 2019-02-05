using System;
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


        //public void TestLog(string name, LoggingLevel level, Exception ex = null)      
        //{
        //    switch (level)
        //    {
        //        case LoggingLevel.Info:
        //            Log.Info(name, ex);
        //            break;
        //        case LoggingLevel.Error:
        //            Log.Error(name, ex);
        //            break;
        //    }

        //}

        public void LogInfo(string source)
        {
            CustomAppender ca = new CustomAppender();
            ca.ClassName = source;
            Log.Info(ca);
        }

        public void LogWarning()
        {
            Log.Warn("");
        }

        public void LogError()
        {
            Log.Error("");
        }

        public void LogDebug()
        {
            Log.Debug("");
        }

        public void LogFatal()
        {
            Log.Fatal("");
        }
    }
}
