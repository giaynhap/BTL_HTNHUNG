using MQTTnet;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BqlMQtt
{
    class Program
    {
        const int MQTT_PORT = 9833;
        const int MTTC_PORT = 9803;
        static void MessageInterceptor(MqttApplicationMessageInterceptorContext context)
        {
            Console.WriteLine("Client [" +context.ClientId+ "]Message to topic: "+context.ApplicationMessage.Topic);

        }
       static Dht11Control dht11 = new Dht11Control();

        static void onMessage(Socket  socket,string message)
        {
            dht11.OnInput(message);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\n\n\nMQTTSERVER - KMAVN\n\tGiay Nhap\n\n");

            var optionsBuilder = new MqttServerOptionsBuilder()
            .WithConnectionBacklog(100)
            .WithApplicationMessageInterceptor(MessageInterceptor)
            .WithDefaultEndpointPort(MQTT_PORT);

            var mqttServer = new MqttFactory().CreateMqttServer();
              mqttServer.StartAsync(optionsBuilder.Build());
         
            Console.WriteLine("MQTTServer started at port "+ MQTT_PORT);
            var mttcServer = new MTTCServer(MTTC_PORT);
            mttcServer.start();
            mttcServer.onMessage += onMessage;


            while (true)
            {
                Thread.Sleep(3000);
            }
        }
 
    }
}
