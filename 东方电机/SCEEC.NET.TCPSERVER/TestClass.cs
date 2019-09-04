using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCEEC.NET.TCPSERVER;
using System.Net;
using System.Net.Sockets;

namespace SCEEC.NET.TCPSERVER
{

    public static class TestClass
    {
        public static void SendData(byte[] senddata, AsyncTCPServer tCPServer)
        {
            try
            {
                // state = (TCPClientState)tCPServer._clients.ToArray()[0];
                var temp = tCPServer._clients.ToArray();
                foreach (var a in temp)
                {
                    tCPServer.Send((TCPClientState)a, senddata);

                }
                // tCPServer.Send(state.TcpClient, senddata);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }

        private static byte CheckData(byte[] checkdata)
        {
            byte sun = 0;
            for (int i = 0; i < checkdata.Length - 1; i++)
            {
                sun += checkdata[i];
            }
            return sun;
        }



        private static bool IsCheckData(byte[] checkdata)
        {
            byte[] tempD = new byte[checkdata.Length - 1];
            for (int i = 0; i < checkdata.Length - 1; i++)
            {
                tempD[i] = checkdata[i];
            }
            byte Endcheckdata = 0;
            foreach (byte outd in tempD)
            {
                Endcheckdata += outd;
            }
            return Endcheckdata == checkdata[checkdata.Length - 1];
        }
        /// <summary>
        /// 查询发送 02
        /// </summary>
        private static void SerchSend(AsyncTCPServer tCPServer, byte[] data)
        {

            SendData(new byte[] { 0xac, data[data.Length - 2] }, tCPServer);
        }

        private static void Connec(AsyncTCPServer tCPServer, byte[] data)
        {
            if(data[0]==0xcc&&data[1]==0x90&&data[2]==0x03)
            {
                //在此联机
            }
        }

        private static void DisConnec(AsyncTCPServer tCPServer, byte[] data)
        {
            if (data[0] == 0xbc && data[1] == 0x90 && data[2] == 0x03)
            {
                //在此断开联机
            }
        }

        private static void SetPar(AsyncTCPServer tCPServer, byte[] data, bool IsSuccess)
        {
            if (IsSuccess)
            {
                if (data.Length >= 3)
                {
                    //在此解析数据和设置参数
                    byte Sum = Convert.ToByte(0xac + data[data.Length - 2]);
                    SendData(new byte[] { 0xac, data[data.Length - 2], Sum }, tCPServer);
                }
                else
                    SendData(new byte[] { 0xee, 0xee }, tCPServer);
            }

        }
        /// <summary>
        /// 查询测量状态
        /// </summary>
        /// <param name="tCPServer"></param>
        /// <param name="data"></param>
        /// <param name="NeedReturnData"></param>
        private static void QueryTestState(AsyncTCPServer tCPServer, byte[] data, byte[] NeedReturnData)
        {
            SendData(NeedReturnData, tCPServer);
        }
        /// <summary>
        /// 查询测量结果
        /// </summary>
        /// <param name="tCPServer"></param>
        /// <param name="data"></param>
        /// <param name="NeedReturnData"></param>
        private static void QueryTestResult(AsyncTCPServer tCPServer, byte[] data, byte[] NeedReturnData)
        {
            SendData(NeedReturnData, tCPServer);
        }

        /// <summary>
        /// 反接状态
        /// </summary>
        /// <param name="tCPServer"></param>
        /// <param name="data"></param>
        /// <param name="ISTRUE"></param>
        private static void QueryDisStata(AsyncTCPServer tCPServer, byte[] data, bool ISTRUE)
        {
            if (data[0] == 0xbd && data[1] == 0xaa && data[2] == 0xff)
            {
                if (ISTRUE == true)
                    SendData(new byte[] { 0xac, 0x00 }, tCPServer);
                else
                    SendData(new byte[] { 0xee, 0xee }, tCPServer);
            }
        }


        public static void ReturnMessages(AsyncTCPServer tCPServer, byte[] data)
        {
            if (data != null)
            {
                switch (data[0])
                {
                    case 0x02:
                        SetPar(tCPServer, data, true);
                        break;
                    case 0xcc:
                        if (data[1] == 0x90)
                            ;
                        else
                            ;
                        break;
                    case 0xbc:
                        break;
                    case 0x41:
                        break;
                    case 0x42:
                        break;
                    case 0xac:
                        break;
                    case 0xed:
                        break;
                    case 0x32:
                        break;
                    case 0x3a:
                        break;
                    case 0xda:
                        break;
                    case 0xff:
                        break;
                    case 0xfd:
                        break;
                    case 0xbd:
                        break;

                }
            }
        }


    }
}
