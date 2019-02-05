using System;
using System.Collections.Generic;
using System.Text;
using log4net.Appender;
using log4net.Core;

namespace WebShop.Log
{
    public class CustomAppender: AppenderSkeleton
    {
        public string ClassName { get; set; }
        protected override void Append(LoggingEvent loggingEvent)
        {
            loggingEvent.Properties["ClassName"] = ClassName;
        }
    }
}
