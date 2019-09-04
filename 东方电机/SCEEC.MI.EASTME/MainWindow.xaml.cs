using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SCEEC.NET.TCPSERVER;

namespace SCEEC.MI.EASTME
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private AsyncTCPServer TcpServer;
        const int bufferSize = 1024;
        byte[] buffer = new byte[bufferSize];
        int readBytes = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Windows_loading(object sender, RoutedEventArgs e)
        {
            IPAddress iP = IPAddress.Parse("192.168.18.27");
            TcpServer = new AsyncTCPServer(55555);
            TcpServer.Start();
            TcpServer.DataReceived += TcpServer_DataReceived;
            TcpServer.ClientConnected += TcpServer_ClientConnected;
            TcpServer.ClientDisconnected += TcpServer_ClientDisconnected;
            TcpServer.CompletedSend += TcpServer_CompletedSend;
            TcpServer.NetError += TcpServer_NetError;
            TcpServer.OtherException += TcpServer_OtherException;
            TcpServer.PrepareSend += TcpServer_PrepareSend;
        }

        private void TcpServer_PrepareSend(object sender, AsyncEventArgs e)
        {
        }

        private void TcpServer_OtherException(object sender, AsyncEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TcpServer_NetError(object sender, AsyncEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TcpServer_CompletedSend(object sender, AsyncEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TcpServer_ClientDisconnected(object sender, AsyncEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TcpServer_ClientConnected(object sender, AsyncEventArgs e)
        {
            e._state.TcpClient.Client.LocalEndPoint.ToString();//已经链接的用户
        }

        private void TcpServer_DataReceived(object sender, AsyncEventArgs e)
        {
            byte[] a = e._state.Buffer;
            int length = e._state.RecLength;
            //readBytes = e._state.NetworkStream.Read(buffer, 0, buffer.Length);
            //string str = Encoding.ASCII.GetString(buffer).Substring(0, readBytes);
        }


        private void Send()
        {
            byte[] tb = new byte[1024];
            try
            {
                TCPClientState tcps = (TCPClientState)TcpServer._clients.ToArray()[0];
                byte[] senddata = { 0x01 };
                TcpServer.Send(tcps, senddata);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

             TestClass.SendData(new byte[] { 0x01 }, TcpServer);//发送数据到所有丛机
         
        }
    }
}
