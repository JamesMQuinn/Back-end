using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using ServiceStack;

namespace ServiceStackServiceLog4NetTemplate.ServiceModel.Messages
{
    public class WeatherResponse
    {
        /// <summary>
        /// Get the listed reservations
        /// </summary>
        //public List<Reservation> Reservations { get; set; }
        /// <summary>
        /// Get the error response status, if there is one.
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        //public WeatherData(int Day)
        //{
        // day = Day;
        //}
        private int averageTemp;
        private float chanceOfPrecip;
        private string skyCondition;
        private int month;
        private int day;

        public void CheckWeather()
        {
            WeatherAPI DataAPI = new WeatherAPI(Day);
            averageTemp = (int)DataAPI.GetTemp();
        }

        public int Day { get => day; set => day = value; }
        public int Month { get => month; set => month = value; }
        public string SkyCondition { get => skyCondition; set => skyCondition = value; }
        public float ChanceOfPrecip { get => chanceOfPrecip; set => chanceOfPrecip = value; }
        public int AverageTemp { get => averageTemp; set => averageTemp = value; }
    }
    public class WeatherAPI
    {
        public WeatherAPI(int day)
        {
            //SetCurrentURL(day);
            xmlDocument = GetXML(CurrentURL);
        }

        public float GetTemp()
        {
            XmlNode temp_node = xmlDocument.SelectSingleNode("//temperature");
            XmlAttribute temp_value = temp_node.Attributes["value"];
            string temp_string = temp_value.Value;
            return int.Parse(temp_string);
        }

        private string CurrentURL;
        private XmlDocument xmlDocument;

        private void SetCurrentURL(string location)
        {
            CurrentURL = "http://5a3844bcbe179d0012970288.mockapi.io/api/v1/weather"
                + "&mode=xml&units=metric&APPID=";
        }

        private XmlDocument GetXML(string CurrentURL)
        {
            using (WebClient client = new WebClient())
            {
                string xmlContent = client.DownloadString(CurrentURL);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlContent);
                return xmlDocument;
            }
        }
    }
}
