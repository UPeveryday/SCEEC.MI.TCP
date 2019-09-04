﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Globalization;
//using ChangeData;
using System.Collections;
using System.Threading;

namespace SCEEC.MI.High_Precision
{
    public class SerialClass
    {

        SerialPort _seriaPort = null;
        //定义委托
        public delegate void SerialPortDataReceiveEventArgs(object sender, SerialDataReceivedEventArgs e, byte[] bits);
        //定义接收数据事件
        public event SerialPortDataReceiveEventArgs DataReceived;
        //定义接收错误事件
        // public event SerialErrorReceivedEventHandler Error;  
        //接收事件是否有效 false表示有效
        public bool ReceiveEventFlag = false;
        #region 获取串口号
        private string portName;
        public string PortName
        {
            get { return _seriaPort.PortName; }
            set
            {
                _seriaPort.PortName = value;
                portName = value;

            }
        }

        #endregion

        #region 获取比特率
        private int baudRate;
        public int BaudRate
        {
            get
            {
                return _seriaPort.BaudRate;
            }

            set
            {
                _seriaPort.BaudRate = value;
                baudRate = value;
            }
        }
        #endregion

        #region 默认构造函数
        /// <summary>
        /// 默认构造函数，操作COM1，速度为9600，没有奇偶校验，8位字节，停止位为1 "COM1", 9600, Parity.None, 8, StopBits.One
        /// </summary>
        public SerialClass()
        {
            _seriaPort = new SerialPort();
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数,
        /// </summary>
        /// <param name="comPortName"></param>
        public SerialClass(string comPortName)
        {
            _seriaPort = new SerialPort(comPortName);
            _seriaPort.BaudRate = 115200;
            _seriaPort.Parity = Parity.Even;
            _seriaPort.DataBits = 8;
            _seriaPort.StopBits = StopBits.One;
            _seriaPort.Handshake = Handshake.None;
            _seriaPort.RtsEnable = true;
            _seriaPort.ReadTimeout = 2000;
            _seriaPort.ReadBufferSize = 70000;
            _seriaPort.WriteBufferSize = 70000;
            setSerialPort();
        }
        #endregion

        #region 构造函数,可以自定义串口的初始化参数
        /// <summary>
        /// 构造函数,可以自定义串口的初始化参数
        /// </summary>
        /// <param name="comPortName">需要操作的COM口名称</param>
        /// <param name="baudRate">COM的速度</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="dataBits">数据长度</param>
        /// <param name="stopBits">停止位</param>
        public SerialClass(string comPortName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _seriaPort = new SerialPort(comPortName, baudRate, parity, dataBits, stopBits);
            _seriaPort.RtsEnable = true;
            _seriaPort.ReadTimeout = 2000;
            setSerialPort();
        }
        #endregion

        #region 析构函数
        /// <summary>
        /// 析构函数，关闭串口
        /// </summary>
        ~SerialClass()
        {
            if (_seriaPort.IsOpen)
                _seriaPort.Close();
        }
        #endregion

        #region 设置串口参数
        /// <summary>
        /// 设置串口参数
        /// </summary>
        /// <param name="comPortName">需要操作的COM口名称</param>
        /// <param name="baudRate">COM的速度</param>
        /// <param name="dataBits">数据长度</param>
        /// <param name="stopBits">停止位</param>
        public void setSerialPort(string comPortName, int baudRate, int dataBits, int stopBits)
        {
            if (_seriaPort.IsOpen)
                _seriaPort.Close();
            _seriaPort.PortName = comPortName;
            _seriaPort.BaudRate = baudRate;
            _seriaPort.Parity = Parity.None;
            _seriaPort.DataBits = dataBits;
            _seriaPort.StopBits = (StopBits)stopBits;
            _seriaPort.Handshake = Handshake.None;
            _seriaPort.RtsEnable = false;
            _seriaPort.ReadTimeout = 2000;
            _seriaPort.NewLine = "/r/n";
            // _seriaPort.NewLine = "NONE";
            _seriaPort.ReadBufferSize = 70000;
            _seriaPort.WriteBufferSize = 70000;

            //_seriaPort.ReceivedBytesThreshold
            // _seriaPort.ReceivedBytesThreshold = 1024;
            // _seriaPort.NewLine = "#";

            setSerialPort();

        }
        #endregion

        #region 设置串口资源
        /// <summary>
        /// 设置串口资源,还需重载多个设置串口的函数
        /// </summary>
        void setSerialPort()
        {
            if (_seriaPort != null)
            {
                //设置触发DataReceived事件的字节数为1
                _seriaPort.ReceivedBytesThreshold = 1;
                //接收到一个字节时，也会触发DataReceived事件
                _seriaPort.DataReceived += new SerialDataReceivedEventHandler(_seriaPort_DataReceived);
                //接收数据出错,触发事件
                _seriaPort.ErrorReceived += new SerialErrorReceivedEventHandler(_seriaPort_ErrorReceived);

            }
        }
        #endregion

        #region 打开串口资源
        /// <summary>
        /// 打开串口资源
        /// <returns>返回bool类型</returns>
        /// </summary>
        public bool openPort()
        {
            bool ok = false;
            if (_seriaPort.IsOpen)
                _seriaPort.Close();
            try
            {
                _seriaPort.Open();
                ok = true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return ok;
        }

        #endregion

        #region 关闭串口
        /// <summary>
        /// 关闭串口资源,操作完成后,一定要关闭串口
        /// </summary>
        public void closePort()
        {
            //如果串口处于打开状态,则关闭
            if (_seriaPort.IsOpen)
                _seriaPort.Close();
        }
        #endregion

        #region 接收串口数据事件
        /// <summary>
        /// 接收串口数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        public void _seriaPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //禁止接收事件时直接退出
            if (ReceiveEventFlag)
            {
                return;
            }
            try
            {
                byte[] _data = new byte[_seriaPort.BytesToRead];
                _seriaPort.Read(_data, 0, _data.Length);
                if (_data.Length == 0)
                {
                    return;
                }
                if (DataReceived != null)
                {
                    DataReceived(sender, e, _data);
                }
                //_seriaPort.DiscardInBuffer();
            }
            catch
            {

            }
        }
        #endregion



        #region 接收数据出错事件
        /// <summary>
        /// 接收数据出错事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void _seriaPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }
        #endregion

        #region 发送数据string类型
        public void SendDataString(string data)
        {
            //发送数据
            //禁止接收事件时直接退出
            if (ReceiveEventFlag)
            { return; }
            if (_seriaPort.IsOpen)
            {
                _seriaPort.Write(data);
            }
        }
        #endregion

        #region 发送数据byte类型

        /// <summary>
        /// 数据发送
        /// </summary>
        /// <param name="data">要发送的数据字节</param>
        public void SendDataByte(byte[] data, int offset, int count)
        {
            ReceiveEventFlag = false;
            // 禁止接收事件时直接退出

            try
            {
                if (_seriaPort.IsOpen)
                {
                    _seriaPort.Write(data, offset, count);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 发送命令
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="SendData">发送数据</param>
        /// <param name="ReceiveData">接收数据</param>
        /// <param name="Overtime">超时时间</param>
        /// <returns></returns>
        public int SendCommand(byte[] SendData, ref byte[] ReceiveData, int Overtime)
        {
            if (_seriaPort.IsOpen)
            {
                try
                {
                    ReceiveEventFlag = true;//关闭接收事件
                    _seriaPort.DiscardInBuffer();//清空接收缓冲区     
                    _seriaPort.Write(SendData, 0, SendData.Length);
                    int num = 0, ret = 0;
                    System.Threading.Thread.Sleep(10);
                    ReceiveEventFlag = false;//打开事件
                    while (num++ < Overtime)
                    {
                        if (_seriaPort.BytesToRead >= ReceiveData.Length)
                        {
                            break;
                        }
                        System.Threading.Thread.Sleep(10);
                    }
                    if (_seriaPort.BytesToRead >= ReceiveData.Length)
                    {
                        ret = _seriaPort.Read(ReceiveData, 0, ReceiveData.Length);
                    }
                    else
                    {
                        ret = _seriaPort.Read(ReceiveData, 0, _seriaPort.BytesToRead);

                    }

                    ReceiveEventFlag = true;
                    return ret;

                }
                catch //(Exception ex)
                {
                    ReceiveEventFlag = true;
                    // throw ex;
                }
            }
            return -1;
        }
        #endregion

        /// <summary>
        /// 读取长数据>4092
        /// </summary>
        /// <param name="SendData">发送指令</param>
        /// <param name="RecData">接受数组</param>
        /// <param name="RecDataLength">需要接受数据的长度</param>
        /// <param name="Timeout">超时时间</param>
        /// <returns></returns>
        public byte[] ReadPortsData(byte[] SendData, byte[] RecData, int RecDataLength, int Timeout)
        {
            if (_seriaPort.IsOpen)
            {
                ReceiveEventFlag = true;//关闭接收事件
                _seriaPort.DiscardInBuffer();//清空接收缓冲区  
                Thread.Sleep(10);
                _seriaPort.DiscardInBuffer();//清空接收缓冲区     
                if (SendData.Length != 0) _seriaPort.Write(SendData, 0, SendData.Length);
                if (RecDataLength == 0) return null;
                if (RecDataLength != -1)
                {
                    for (int t = 0; t < Timeout; t++)
                    {
                        if (_seriaPort.BytesToRead >= RecDataLength) break;
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    for (int t = 0; t < Timeout; t++)
                    {
                        if (_seriaPort.BytesToRead > 0) break;
                        Thread.Sleep(10);
                    }
                }
                int count = _seriaPort.BytesToRead;
                int offset = 0;
                int SingalReadDataLength = 0;
                if (count >= RecDataLength && count != 0)
                {
                    while (offset < count)
                    {
                        SingalReadDataLength = _seriaPort.Read(RecData, offset, count - offset);
                        if (SingalReadDataLength > 0)
                        {
                            offset += SingalReadDataLength;
                            if (RecData.Length == RecDataLength)
                                return RecData;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            else
            {
                return new byte[1] { 0x01 };//串口出错
            }

            return new byte[1] { 0x02 };//shezhi
        }

        public byte[] ReadPortsData(byte[] SendData, byte[] RecData, int RecDataLength)
        {
            int Timeout = RecDataLength / 1000;
            if (_seriaPort.IsOpen)
            {

                ReceiveEventFlag = true;//关闭接收事件
                _seriaPort.DiscardInBuffer();//清空接收缓冲区  
                Thread.Sleep(10);
                _seriaPort.DiscardInBuffer();//清空接收缓冲区     
                if (SendData.Length != 0) _seriaPort.Write(SendData, 0, SendData.Length);
                if (RecDataLength == 0) return null;
                if (RecDataLength != -1)
                {
                    for (int t = 0; t < Timeout; t++)
                    {
                        if (_seriaPort.BytesToRead >= RecDataLength) break;
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    for (int t = 0; t < Timeout; t++)
                    {
                        if (_seriaPort.BytesToRead > 0) break;
                        Thread.Sleep(10);
                    }
                }
                int count = _seriaPort.BytesToRead;
                int offset = 0;
                int SingalReadDataLength = 0;
                if (count >= RecDataLength && count != 0)
                {
                    while (offset < count)
                    {
                        SingalReadDataLength = _seriaPort.Read(RecData, offset, count - offset);
                        if (SingalReadDataLength > 0)
                        {
                            offset += SingalReadDataLength;
                            if (RecData.Length == RecDataLength)
                                return RecData;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            else
            {
                return new byte[1] { 0x01 };//串口出错
            }

            return new byte[1] { 0x02 };//shezhi
        }

        #region 获取串口
        /// <summary>
        /// 获取所有已连接短信猫设备的串口
        /// </summary>
        /// <returns></returns>
        public string[] serialsIsConnected()
        {
            List<string> lists = new List<string>();
            // lists.Add("COM3");
            string[] seriallist = getSerials();
            foreach (string s in seriallist)
            {
                lists.Add(s);
            }
            // return seriallist;
            return lists.ToArray();//原型
        }
        #endregion

        #region 获取当前全部串口资源
        /// <summary>
        /// 获得当前电脑上的所有串口资源
        /// </summary>
        /// <returns></returns>
        public string[] getSerials()
        {
            return SerialPort.GetPortNames();
        }
        #endregion

        #region 字节型转换16
        /// <summary>
        /// 把字节型转换成十六进制字符串
        /// </summary>
        /// <param name="InBytes"></param>
        /// <returns></returns>
        //public string ByteTostring(byte[] InBytes)
        //{
        //    string StringOut = "";
        //    foreach (byte InByte in InBytes)
        //    {
        //        StringOut = StringOut + string.Format("{X2}",InByte);
        //    }
        //    return StringOut;
        //}
        #endregion

        #region 十六进制字符串转字节型
        /// <summary>
        /// 把十六进制字符串转换成字节型(方法1)
        /// </summary>
        /// <param name="InString"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string InString)
        {
            string[] ByteStrings;
            ByteStrings = InString.Split(" ".ToCharArray());
            byte[] ByteOut;
            ByteOut = new byte[ByteStrings.Length];
            for (int i = 0; i <= ByteStrings.Length - 1; i++)
            {
                ByteOut[i] = Byte.Parse(ByteStrings[i], System.Globalization.NumberStyles.HexNumber);

            }
            return ByteOut;
        }

        #endregion

        #region 十六进制字符串转字节型
        /// <summary>
        /// 字符串转16进制字节数组(方法2)
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] stringToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;

        }
        #endregion

        #region 字节型转十六进制字符串
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }

            }

            return returnStr;

        }
        #endregion

        public void send16(string text)
        {
            string sendstr = text;
            sendstr = sendstr.Replace(" ", "");
            byte[] send = new byte[sendstr.Length / 2];
            for (int i = 0; i < sendstr.Length; i += 2)
            {
                send[i / 2] = (byte)Convert.ToByte(sendstr.Substring(i, 2), 16);
            }
            _seriaPort.Write(send, 0, send.Length);


        }





    }


    
}
