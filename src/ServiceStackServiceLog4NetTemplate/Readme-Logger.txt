  ###################################################
  #													#
  # EnterpriseMonitoring.Logging.NetFramework Readme  #
  #													#
  ###################################################
    
  #### Why You Want This ####
	You really should be logging more than you do now.
	ETW is the best way to log in Windows.
	It's about zero effort to add logging to new code.
	It's zero effort to add logging to existing interfaces.
	Generated IL keeps overhead low, and it's almost nothing if tracing is off.

  #### Logging WIKI ####
  https://github.chrobinson.com/CHR/EnterpriseMonitoring.Logging/wiki

  #### Example on using logger ####
  https://github.chrobinson.com/CHR/EnterpriseMonitoring.Logging.NetFramework/blob/development/samples/EnterpriseMonitoring.Logging.NetFramework.Samples/Program.cs
  
  #### Configuration ####
  In order to embed the configuration data in the .config file the section name must be identified to the .NET config file parser using a "configSections" element. 
  The section must specify the "EnterpriseMonitoring.Logging.NetFramework.Config.LoggingConfigurationSectionHandler" that will be used to parse the config section. 
  This type must be fully assembly qualified because it is being loaded by the .NET config file parser not by Logging Library. 
  The correct assembly name for the logging assembly must be specified. The following is a simple example configuration file that specifies the correct section handler to use for the "CHREnterpriseLogging" section.


<configuration>
  <configSections>
    <section name="CHREnterpriseLogging" type="EnterpriseMonitoring.Logging.NetFramework.Config.LoggingConfigurationSectionHandler, EnterpriseMonitoring.Logging.NetFramework"/>
  </configSections>
  
 <CHREnterpriseLogging>
    <telemetryModules> <!--these can only be configured through the .config below are the default settings-->
      <type name="Sql" enable="true"/>  <!--used to capture Sql calls-->
	  
      <type name="HttpInbound" enable="true" readRequestBody="false" readResponseBody="false"/> <!--Reports the response time and result code of HTTP requests.-->

      <type name="HttpOutbound" enable="true"/> <!--used to capture all http outbound requests-->

      <type name="Exception" enableAll="true" enableUnhandle="true" enableUnobservedTask="false"/>  <!--used to track handle and/or unhandled exceptions -->

      <type name="ApplicationInfo" enable="true" includeMicrosoftGacAssemblies="true" includeAssemblies="true"/>  <!--used to report details of your application when it first starts up -->
	  
    </telemetryModules>
    <additionalTelemetryData>
      <loggingLocation enable="false"/>  <!--All telemetry types will include the class & method that the logged event came from.-->
    </additionalTelemetryData>
    <globalData>
      <!-- use to help filter or search for different sets of events, this is shared within the appDomain.-->
      <data name="key goes here" value="value goes here"/> <!-- key value set, can have multi entries-->
    </globalData>
  </CHREnterpriseLogging>
</configuration>



  #### Using Telemetry Modules ####
  Telemetry modules "<telemetryModules>" are used for automatic collection of enabled telementry types.
  For ASP.NET apps they are initializes automatically, When ASP.NET creates an instance of the HttpApplication class that represents your application.
  For non ASP.NET apps, they must call "EnterpriseMonitoring.Logging.NetFramework.LogManager.InitializeModules();" method manually.


  #### Questions ####
  If any questions or comments, feel free to contact Chad McCormick or Justin Miller.
