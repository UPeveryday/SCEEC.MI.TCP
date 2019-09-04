using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SCEEC.MI.High_Precision
{
   public static  class GetBasePraameter
    {
        
        public static string[] Portnames
        {
            get
            {
                return SerialPort.GetPortNames();
            }
        }
    }
}
