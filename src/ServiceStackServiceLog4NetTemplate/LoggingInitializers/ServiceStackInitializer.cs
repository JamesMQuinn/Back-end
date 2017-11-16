using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnterpriseMonitoring.Logging.NetFramework.Telemetry.Extend;
using ServiceStack;
using CHRobinson.Shared;
using ServiceStack.Web;
using ServiceStack.Host;
using ServiceStack.Host.AspNet;

namespace ServiceStackServiceLog4NetTemplate.LoggingInitializers
{
    /// <summary>
    /// override data that is only seen in service stack ecosystem.
    /// </summary>
    public class ServiceStackInitializer : ITelemetryContextInitializer
    {
        public void Initialize(ITelemetryContext telemetry)
        {
            ServiceStackHost instance = ServiceStackHost.Instance;
            if (instance != null)
            {
                Funq.Container container = instance.Container;
                if (container != null)
                {
                    //grab data if application is using operation context.
                    var operationContext = container.TryResolve<OperationContext>();
                    if (operationContext != null)
                    {
                        if (!operationContext.SourceSystem.IsNullOrEmpty())
                        {
                            telemetry.Http.SourceSystem = operationContext.SourceSystem;
                        }

                        if (!operationContext.CorrelationId.IsNullOrEmpty())
                        {
                            telemetry.Http.CorrelationId = operationContext.CorrelationId;
                        }

                        if (!operationContext.UserName.IsNullOrEmpty())
                        {
                            telemetry.User.ClientUserName = operationContext.UserName;
                        }
                    }
                }

                //standarized REST route without individual path
                IRequest iRequest = instance.TryGetCurrentRequest();

                //test web api to see if this comes through for dev side.
                var iasp = iRequest as AspNetRequest;
                iasp.XForwardedFor;
                iasp.XForwardedPort;
                iasp.XForwardedProtocol;

                if (iRequest != null)
                {
                    RestPath restPath = iRequest.GetRoute();
                    if (restPath!=null)
                    {
                        var aspNetRequest = iRequest as AspNetRequest;
                        telemetry.Http.Name = $"{aspNetRequest.HttpMethod} {restPath.Path}";
                    }
                }
            }           
        }
    }
}