using BqlMQtt.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BqlMQtt
{
    public delegate void OnNewValueDht11Delegate(double temperature, double humidity);
    public class Dht11Control
    {
       static public OnNewValueDht11Delegate EventNewValue;
        public Dht11Control()
        {

        }
        public void OnInput(String str)
        {
            string pattern = @"nd=(.*?)&da=(.*?);";
            Match match = Regex.Match(str, pattern);
            if (!match.Success || match.Groups.Count < 3)
            {
                return;
            }
            double temperature=0, humidity=0;
            Double.TryParse(match.Groups[1].Value, out temperature);
            Double.TryParse(match.Groups[2].Value, out humidity);
            StoreDHT11Value(temperature, humidity);
        }

        public Boolean StoreDHT11Value(double temperature, double humidity)
        {
            Console.WriteLine("["+DateTime.Now.ToString("dd/MM/yyyy HH:mm")+"]Store DHT11Value: temperature: " + temperature + " humidity: " + humidity);
            bool value = DBSQLServerUtils.StoreDht11(temperature, humidity);
            EventNewValue.Invoke(temperature, humidity);
            return value;
        }
    }
}
