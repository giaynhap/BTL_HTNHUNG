using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BqlMQtt
{
    public class DeviceControl
    {
        public DeviceControl()
        {

        }
        public void EnableReceivedDht11Event()
        {
            Dht11Control.EventNewValue += OnDht11NewData;
        }
        private void OnDht11NewData(double t,double h)
        {

        }
    }
}
