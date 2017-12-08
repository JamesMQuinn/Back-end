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

namespace ServiceStackServiceLog4NetTemplate.LoggingSetup
{
    /// <summary>
    /// override Telemetry context that is only seen in service stack ecosystem.
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
                    OperationContext operationContext = container.TryResolve<OperationContext>();
                    if (operationContext != null)
                    {
                        //call parent system name
                        if (!operationContext.SourceSystem.IsNullOrEmpty())
                        {
                            telemetry.Http.SourceSystem = operationContext.SourceSystem;
                        }

                        //user name of caller
                        if (!operationContext.UserName.IsNullOrEmpty())
                        {
                            telemetry.User.ClientUserName = operationContext.UserName;
                        }
                    }
                }

                //attempt to standarized REST route without individual path
                StandarizedRouteName(instance.TryGetCurrentRequest(), telemetry);
            }           
        }

        /// <summary>
        /// attempt to standarized REST route without individual path
        /// </summary>
        private void StandarizedRouteName(IRequest iRequest, ITelemetryContext telemetry)
        {
            if (iRequest != null)
            {
                RestPath restPath = iRequest.GetRoute();
                if (restPath != null)
                {
                    AspNetRequest aspNetRequest = iRequest as AspNetRequest;
                    telemetry.Http.Name = $"{aspNetRequest.HttpMethod} {restPath.Path}";
                }
            }
        }
    }
}