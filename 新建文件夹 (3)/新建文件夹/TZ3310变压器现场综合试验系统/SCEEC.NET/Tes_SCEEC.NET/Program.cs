using System;
using System.Collections.Generic;
using System.Text;
using SCEEC.NET;
using System.IO.Ports;
using System.Globalization;


namespace Tes_SCEEC.NET
{
    class Program
    {
         static SerialClass sc = new SerialClass();


        static void screceivee(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            Console.WriteLine(Encoding.Default.GetString(bits));
            

        }
        static void Main(string[] args)
        {
            Console.WriteLine(sc.PortName);
            Console.WriteLine(sc.BaudRate);
            sc.setSerialPort("COM9",115200,8,1);
            Console.WriteLine(sc.serialsIsConnected());

            string[] buf = sc.serialsIsConnected();
            foreach (string outbuf in buf)
            {
                Console.WriteLine(outbuf);
            }

            Console.WriteLine(sc.PortName);

            Console.WriteLine(sc.BaudRate);
            sc.openPort();
            Console.WriteLine("1111111111111");

            sc.DataReceived += new SerialClass.SerialPortDataReceiveEventArgs(screceivee);


            while (true)
            {
                string a = Console.ReadLine();
                sc.SendData(a);
            }
            Console.ReadKey();
           sc.closePort();
        }
    }
}
