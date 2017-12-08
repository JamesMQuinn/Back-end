using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnterpriseMonitoring.Logging.NetFramework;
using System.Net;
using CHR.ServiceStack.Plugins.Models.HealthCheckFeature;
using CHR.ServiceStack.Plugins.ServiceDefinition;
using Funq;
using CHR.ServiceStack.Plugins.Plugins.HealthCheck;
using CHR.ServiceStack.Plugins.ServiceDefinition.Wrappers;

namespace ServiceStackServiceLog4NetTemplate.LoggingSetup
{
    public class LoggingCorrelationSetup : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            //append correlation header for client calls
           ServiceClientBase.GlobalRequestFilter = new Action<HttpWebRequest>(AddCorrelationHeaders);
        }

        /// <summary>
        /// add correlation headers for HttpWebRequest
        /// </summary>
        /// <param name="outboundRequest"></param>
        public static void AddCorrelationHeaders(HttpWebRequest outboundRequest)
        {
            outboundRequest?.SetupCorrelationLogging();
        }
    }
}