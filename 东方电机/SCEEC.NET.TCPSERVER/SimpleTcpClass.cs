using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.NET.TCPSERVER
{
    public class SimpleTcpClass
    {

        public string Ip { get; set; }
        public int Port { get; set; }

        public SimpleTcpClass(string ip,int portnum)
        {
            this.Ip = ip;
            this.Port = portnum;
        }



    }
}
