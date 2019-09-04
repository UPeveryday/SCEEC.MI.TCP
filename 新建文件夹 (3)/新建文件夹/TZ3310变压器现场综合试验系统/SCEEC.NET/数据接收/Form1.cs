using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SCEEC.NET;
using System.IO.Ports;

namespace 数据接收
{

    public partial class Form1 : Form
    {
        SerialClass sc = new SerialClass();
        List<byte> byteData = new List<byte>();
        public Form1()
        {
            InitializeComponent();

            //  newport.setSerialPort(textBox1.Text.ToString(), 115200, 8, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sc.closePort();

                sc.setSerialPort("COM8", 115200, 8, 1);
                sc.openPort();
                // MessageBox.Show("成功");
                sc.DataReceived += new SerialClass.SerialPortDataReceiveEventArgs(screceivee);

                // textValue_Copy.Text += "窗口打开成功，处于监听状态";

            }
            catch
            {

            }
        }

        public void screceivee(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            byteData.AddRange(bits);

            this.Invoke(new EventHandler(delegate
            {
                //    listBox1.Text += Encoding.ASCII.GetString(bits).Trim() + "\r\n";
                 richTextBox1.Text += Encoding.ASCII.GetString(bits)+"\t";
                 textBox1.Text +=  bits.Length.ToString()+"\t";
                //if(textBox1.Text.ToString().Length>350)
                //{
                //    textBox1.Text = "";
                //}

            }));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            textBox1.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (byte a in byteData)
            {
         
                richTextBox2.Text += a.ToString() + "\t";
              //  byteData.RemoveAt(i);
                i++;


            }

            richTextBox1.Text = i.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
