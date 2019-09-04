using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SCEEC.NET;
using System.IO.Ports;

//using INIFILE;



namespace YUYUXU
{
    public partial class Form1 : Form
    {
        SerialClass sc = new SerialClass();
       // SerialPort s = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }
        public void screceivee(object sender, SerialDataReceivedEventArgs e,  byte[] bits)
        {
            //Console.WriteLine(Encoding.Default.GetString(bits));
            Control.CheckForIllegalCrossThreadCalls = false;
            richTextBox2.Text = Encoding.Default.GetString(bits);
            //this.Invoke((EventHandler)(delegate
            //{
               
                
            //}));

            richTextBox2.Text = Encoding.Default.GetString(bits);
            
        }
        
    

        private void Form1_Load(object sender, EventArgs e)
        {
            sc.closePort();

            //  SerialClass sc = new SerialClass();
            comboBox2.SelectedIndex = 3;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 7;
            try
            {
                comboBox1.Items.AddRange(SerialPort.GetPortNames());
                comboBox1.SelectedIndex = 0;
               
            }
            catch
            {
                // MessageBox.Show("请插入串口线或者键入正确的COM口"); 
                richTextBox1.Text = "请插入串口线或者键入正确的COM口";
            }

            

               // System.Threading.Thread.Sleep(20);

            
            //sc.openPort();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // button1.Text = "OPEN PORTS";
            

            string comP = comboBox1.SelectedItem.ToString();
            int baudL = Convert.ToInt32(comboBox5.SelectedItem);
            int dataB = Convert.ToInt32(comboBox2.SelectedItem);
            int stopD = Convert.ToInt32(comboBox4.SelectedItem);
            sc.setSerialPort(comP, baudL, dataB, stopD);
            if (button1.Text == "CLOSE PORT")
            {
                sc.closePort();
                sc.DataReceived -= new SerialClass.SerialPortDataReceiveEventArgs(screceivee);

                label11.Text = "SUCCESS CLOSE PORT PLEASE CONTINUE";
                button1.Text = "OPEN PORT";

            }
            sc.openPort();
            sc.DataReceived += new SerialClass.SerialPortDataReceiveEventArgs(screceivee);

            label11.Text = "SUCCESS OPEN PORT PLEASE CONTINUE";
            button1.Text = "CLOSE PORT";
            
           // string a = richTextBox1.Text.ToString();

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == null)
            {
                label11.Text = "请输入需要发送的内容，不可为空";
            }
            string a = richTextBox1.Text.ToString();
            sc.SendData(a);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sc.closePort();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

