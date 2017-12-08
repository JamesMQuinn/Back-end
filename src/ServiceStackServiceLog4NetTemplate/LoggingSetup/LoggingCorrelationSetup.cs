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
        private Container _container;
        public LoggingCorrelationSetup(Container container)
        {
            this._container = container;
            this._container.RegisterAs<CredentialsProvider, ICredentialsProvider>();
            this._container.RegisterAs<HttpWebRequestFactory, IHttpWebRequestFactory>();
            this._container.RegisterAs<DBConnectionFactory, IDBConnectionFactory>();
            HealthCheckerProvider healthCheckerProvider = new HealthCheckerProvider();
            healthCheckerProvider.Register<SqlDbHealthChecker>(new SqlDbHealthChecker(this._container.Resolve<IDBConnectionFactory>()));
            healthCheckerProvider.Register<RestServiceHealthChecker>(new RestServiceHealthChecker(this._container.Resolve<IHttpWebRequestFactory>(), this._container.Resolve<ICredentialsProvider>()));
            healthCheckerProvider.Register<WcfServiceHealthChecker>(new WcfServiceHealthChecker(this._container.Resolve<IHttpWebRequestFactory>()));
            healthCheckerProvider.Register<ElasticSearchHealthChecker>(new ElasticSearchHealthChecker(this._container.Resolve<IHttpWebRequestFactory>(), this._container.Resolve<ICredentialsProvider>()));
            this._container.Register<IHealthCheckerProvider>((IHealthCheckerProvider)healthCheckerProvider);
        }

        public void Register(IAppHost appHost)
        {
            //append correlation header for client calls
           ServiceClientBase.GlobalRequestFilter = new Action<HttpWebRequest>(AddCorrelationHeaders);


            appHost.RegisterService(typeof(HealthCheckService));
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


    /// <summary>
    /// use test
    /// </summary>
    public class HealthCheckService : Service
    {
        private IHealthCheckerProvider _healthCheckerProvider;
        private ServiceStack.Logging.ILog _log;

        public HealthCheckService(IHealthCheckerProvider healthCheckerProvider)
        {
            this._healthCheckerProvider = healthCheckerProvider;
            _log= ServiceStack.Logging.LogManager.LogFactory.GetLogger("testLogger");
        }

        public HealthCheckResponse Get(HealthCheckRequest request)
        {
            var client = new JsonServiceClient();
            var obj=client.Get("http://localhost:2309/status");
            _log.Info("Get HealthCheckRequest()");

            return new HealthCheckResponse();
        }
    }
}