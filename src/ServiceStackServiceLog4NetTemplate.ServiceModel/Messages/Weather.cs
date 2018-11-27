using System.Net;
using System.Xml;
using ServiceStack;

namespace ServiceStackServiceLog4NetTemplate.ServiceModel.Messages
{
    [Route("/GetWeather", Verbs = "GET")]
    public class WeatherData : IReturn<WeatherResponse>
    {
        
    }
}
