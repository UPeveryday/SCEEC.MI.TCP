using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCEEC.MI.TZ3310;
using System.Threading;
using System.Collections;

namespace TZ3310调试程序
{
    public partial class Form1 : Form
    {
       
        SCEEC.NET.SerialClass  sc=new SCEEC.NET.SerialClass();
        ClassTz3310 Tz3310 = new ClassTz3310();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;

            try
            {
                comboBox1.Items.AddRange(sc.getSerials());
                comboBox1.SelectedIndex = 0;

            }
            catch
            {
                MessageBox.Show("串口设置错误");
            }

            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;

            comboBox10.SelectedIndex = 0;
            comboBox13.SelectedIndex = 0;
            comboBox14.SelectedIndex = 0;
            comboBox16.SelectedIndex = 0;
            comboBox18.SelectedIndex = 0;
            comboBox20.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox12.SelectedIndex = 0;



        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label26_Click(object sender, EventArgs e)
        {

        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox3.SelectedValue.ToString());
            // richTextBox1.Text = comboBox3.SelectedValue.ToString();
            try
            {

               // string comP = comboBox1.SelectedItem.ToString();
                int baudL = Convert.ToInt32(comboBox3.SelectedItem);
                int dataB = Convert.ToInt32(comboBox4.SelectedItem);
                int stopD = Convert.ToInt32(comboBox5.SelectedItem);

                if (button1.Text == "打开串口")
                {
                    if (true == Tz3310.OpenPort("COM3", baudL, dataB, stopD))
                    {
                       
                        if (true == Tz3310.CommunicationQuery(0x01))
                        {
                            button1.Text = "关闭串口";
                            button1.ForeColor = System.Drawing.Color.Red;//改变按钮中文字的颜色
                            label15.Text = "仪器通讯正常";
                        }
                        else
                        {
                            label15.Text = "仪器通讯错误";

                        }
                    }
                    else
                    {
                        MessageBox.Show("没有插入串口或者参数输入有误");
                    }

                }
                else
                {
                    Tz3310.Closeport();
                    button1.Text = "打开串口";
                    button1.ForeColor = System.Drawing.Color.Green;//改变按


                }

            }
            catch
            { }

            if (button1.Text == "打开串口")
            {
                tabControl1.Enabled = false;
            }
            else
            {
                tabControl1.Enabled = true;

            }


        }

        private  void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        //  await  Tz3310.CirCleReadTestData(Parameter.TestKind.介质损耗);
        }

        public async Task readJs()
        {
            
           await Task.Run(() =>
            {
                //...

                while (true)
                {
                 string[] RecData = Tz3310.ReadTestData(Parameter.TestKind.介质损耗);
                 if (RecData != null)
                { 
                  //  continue;
               
                    try
                    {
                        for (int i = 0; i < RecData.Length; i++)
                        {
                            if (RecData[i] == null)
                                RecData[i] = "NONE";
                        }
                    }
                    catch { }


                    if (RecData[RecData.Length - 1] == "0")
                    {
                          
                                try
                                {

                                    this.Invoke(new Action(() =>
                                    {
                                        textBox3.Text = RecData[1].ToString();
                                        textBox4.Text = RecData[2].ToString();
                                        textBox5.Text = RecData[3].ToString();

                                    }));
                                }
                                catch (Exception)
                                {


                                }

                    }
                    else if (RecData[RecData.Length - 1] == "1")
                    {
                            try
                            {
                                this.Invoke(new Action(() =>
                                {
                                    textBox22.Text = RecData[0].ToString();
                                    textBox21.Text = RecData[1].ToString();
                                    textBox23.Text = RecData[2].ToString();
                                    textBox20.Text = RecData[3].ToString();


                                }));
                            }
                            catch (Exception)
                            {

                            }
                            
                      



                    }
                    else
                    {
                        if (RecData[RecData.Length - 1] == "2")
                        {
                                try
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        textBox27.Text = RecData[0].ToString();
                                        textBox26.Text = RecData[0].ToString();
                                        textBox25.Text = RecData[0].ToString();
                                        textBox6.Text = RecData[0].ToString();
                                        textBox1.Text = RecData[0].ToString();
                                        textBox7.Text = RecData[0].ToString();

                                    }));
                                }
                                catch (Exception)
                                {

                                    
                                }
                           
                        }
                    }

                    Thread.Sleep(100);
                }

                
                 }

            });


        }

        private async void Button2_Click(object sender, EventArgs e)
        {

            byte[] testMind = Tz3310.ISetPraJs(comboBox2.SelectedIndex, comboBox8.SelectedIndex, comboBox6.SelectedIndex
                , comboBox7.SelectedIndex, int.Parse(textBox2.Text));
            Thread.Sleep(1000);
            label58.Text = "";
            bool kind = Tz3310.StartTest(testMind);
            if (kind == true)
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);

                    this.Invoke(new Action(() =>
                    {
                        label63.Text = "切换线成功";
                    }));
                });
                //Thread.Sleep(1000);
                //label63.Text = "切换线成功";
            }
            else
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);

                    this.Invoke(new Action(() =>
                    {
                        label63.Text = "切换线失败";

                    }));
                });
             //   Thread.Sleep(1000);

            }
          //  Thread.Sleep(2000);
            button3.Enabled = true;

            //  readJs(1000);



        }

        private async void Button3_Click(object sender, EventArgs e)
        {
                await readJs();
        }

        private async void Button9_Click(object sender, EventArgs e)
        {
          
            byte[] testMind = Tz3310.ISetPraYzfj(comboBox10.SelectedIndex, comboBox18.SelectedIndex, comboBox20.SelectedIndex,
                int.Parse(textBox50.Text), int.Parse(textBox16.Text), 2);
            bool kind = Tz3310.StartTest(testMind);
            //if (kind == true)
            //{
            //    Thread.Sleep(1000);
            //    label66.Text = "切换线成功";
            //}
            //else
            //{
            //    Thread.Sleep(1000);
            //    label66.Text = "切换线失败";

            //}
           await TTT();
        }


        public async Task datauiYzfj()
        {
            await Task.Run(() =>
            {
                try
                { 
                 while (true)
                {


                    string[] RecData = Tz3310.ReadTestData(Parameter.TestKind.有载分接);
                    for (int i = 0; i < RecData.Length; i++)
                    {
                        if (RecData[i] == null)
                            RecData[i] = "NONE";
                    }
                    if (RecData.Length == 7)
                    {
                        this.Invoke(new Action(() =>
                        {
                            Thread.Sleep(1000);
                            if (RecData[0] == "0")
                                label74.Text = "不可以触发";
                            if(RecData[0]=="1")
                                label74.Text = "可以触发";
                            if (RecData[0]=="2")
                                label74.Text = "触发成功";

                            //第0个0不可触发 1可以触发 2触发成功
                            textBox43.Text = RecData[1].ToString();
                            textBox46.Text = RecData[2].ToString();
                            textBox42.Text = RecData[3].ToString();
                            textBox45.Text = RecData[4].ToString();
                            textBox41.Text = RecData[5].ToString();
                            textBox44.Text = RecData[6].ToString();
                        }));
                    }
                    else if (RecData.Length == 2)
                    {
                        this.Invoke(new Action(() =>
                        {
                            textBox15.Text = RecData[0].ToString();
                            textBox49.Text = RecData[1].ToString();
                            textBox14.Text = RecData[0].ToString();
                            textBox48.Text = RecData[1].ToString();
                            textBox13.Text = RecData[0].ToString();
                            textBox47.Text = RecData[1].ToString();
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {

                            textBox43.Text = "错误类型：" + RecData[0].ToString();
                            textBox46.Text = "错误类型：" + RecData[0].ToString();
                            textBox42.Text = "错误类型：" + RecData[0].ToString();
                            textBox45.Text = "错误类型：" + RecData[0].ToString();
                            textBox41.Text = "错误类型：" + RecData[0].ToString();
                            textBox44.Text = "错误类型：" + RecData[0].ToString();
                        }));
                    }

                    Thread.Sleep(1000);
                }
                }
                catch
                { }
            });

        }
        private async void Button8_Click(object sender, EventArgs e)
        {


            await datauiYzfj();
        }

        public async Task Setzldz()
        {
            await Task.Run(() =>
           {
               //byte[] tk = new byte[10];
               //  Tz3310.InterRuptMe(Parameter.CommanTest.模拟按键中断测量);
               //   this.Invoke(new Action()=>{ }));
               int a = 0;
               int b= 0;
               int c = 0;
               this.Invoke(new Action(() =>
               {
               label58.Text = "正在设置测量参数...";
               //   byte[] testMind = Tz3310.ISetPraZldz(comboBox14.SelectedIndex, comboBox13.SelectedIndex, comboBox16.SelectedIndex, 1);
               a = comboBox14.SelectedIndex;
               b = comboBox13.SelectedIndex;
               c = comboBox16.SelectedIndex;

               }));
           // byte[] testMind = Tz3310.ISetPraZldz(1, 2, 0, 1);
          byte[] testMind = Tz3310.ISetPraZldz(a, b, c, 1);
           Thread.Sleep(1000);
           this.Invoke(new Action(() =>
           {
               label58.Text = "完成";

           }));
           //this.Invoke(new Action(() =>{

           //}));
               bool kind = Tz3310.StartTest(testMind);
               if (kind == true)
               {
                   this.Invoke(new Action(() =>{
                    Thread.Sleep(1000);
                      label65.Text = "直流电阻切线成功";
                   }));


               }
               else
               {
                   this.Invoke(new Action(() =>{
                   Thread.Sleep(1000);
                   label65.Text = "直流电阻切线失败";
                   }));


               }

           });
        }
        private async void Button7_Click(object sender, EventArgs e)
        {
            await Setzldz();

            button16.Text = "开启";
        }

        public async Task  DataUiZldz()
        {
            await Task.Run(() =>
            {
               // Thread.Sleep(2000);
                while(true)
                {
                    string[] RecData = Tz3310.ReadTestData(Parameter.TestKind.直流电阻);
                    Thread.Sleep(10);

                    if (RecData != null)
                    {
                        if (RecData.Length == 10)
                        {
                            try
                            {
                                for (int i = 0; i < RecData.Length; i++)
                                {
                                    if (RecData[i] == null)
                                        RecData[i] = "NONE";
                                }

                                if(RecData[RecData.Length-1]=="0")
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                       // Thread.Sleep(10);

                                        textBox9.Text = RecData[0].ToString();
                                        textBox10.Text = RecData[1].ToString();
                                        textBox11.Text = RecData[2].ToString();
                                        textBox28.Text = RecData[3].ToString();
                                        textBox24.Text = RecData[4].ToString();
                                        textBox12.Text = RecData[5].ToString();
                                        textBox31.Text = RecData[6].ToString();
                                        textBox30.Text = RecData[7].ToString();
                                        textBox29.Text = RecData[8].ToString();
                                    }));

                                }
                                else if (RecData[RecData.Length - 1] == "1")
                                {
                                  //  Thread.Sleep(10);

                                    textBox40.Text = RecData[0].ToString();
                                    textBox39.Text = RecData[1].ToString();
                                    textBox38.Text = RecData[2].ToString();
                                    textBox37.Text = RecData[3].ToString();
                                    textBox36.Text = RecData[4].ToString();
                                    textBox35.Text = RecData[5].ToString();
                                    textBox34.Text = RecData[6].ToString();
                                    textBox33.Text = RecData[7].ToString();
                                    textBox32.Text = RecData[8].ToString();

                                }
                               
                            }
                                catch
                            { }
                        }
                        else
                        {
                            this.Invoke(new Action(() =>
                            {
                                textBox9.Text = "错误类型：" + RecData[0].ToString();
                                textBox10.Text = "错误类型：" + RecData[0].ToString();
                                textBox11.Text = "错误类型：" + RecData[0].ToString();
                                textBox28.Text = "错误类型：" + RecData[0].ToString();
                                textBox24.Text = "错误类型：" + RecData[0].ToString();
                                textBox12.Text = "错误类型：" + RecData[0].ToString();
                            }));
                        }
                    }


                    Thread.Sleep(1000);
                }
              
            });
        }
      
        private async void Button6_Click(object sender, EventArgs e)
        {

            await DataUiZldz();

        }

        private void TabPage3_Click(object sender, EventArgs e)
        {

        }


        public void  showD()
        {
         

                try
                {
                    while (true)
                    {

                        this.Invoke(new Action(() => {
                            label60.Text = "正在复位";

                        }));
                        Thread.Sleep(600);
                        this.Invoke(new Action(() => {
                            label60.Text = "正在复位。";

                        }));
                        Thread.Sleep(600);

                        this.Invoke(new Action(() => {
                            label60.Text = "正在复位。。";

                        }));
                        Thread.Sleep(600);
                        this.Invoke(new Action(() => {
                            label60.Text = "正在复位。。。";

                        }));
                        Thread.Sleep(600);


                    }
                }
                catch 
                {

                    
                }
             

                //Thread.Sleep();

            

        }
        public void showD1()
        {


            try
            {
                while (true)
                {

                    this.Invoke(new Action(() => {
                        label75.Text = "正在复位";

                    }));
                    Thread.Sleep(600);
                    this.Invoke(new Action(() => {
                        label75.Text = "正在复位。";
                         
                    }));
                    Thread.Sleep(600);

                    this.Invoke(new Action(() => {
                        label75.Text = "正在复位。。";

                    }));
                    Thread.Sleep(600);
                    this.Invoke(new Action(() => {
                        label75.Text = "正在复位。。。";

                    }));
                    Thread.Sleep(600);


                }
            }
            catch
            {


            }


            //Thread.Sleep();



        }


        public async Task ReSetMe()
        {
            await Task.Run(() => {

                try
                {
                    if (true == Tz3310.InterRuptMe(Parameter.CommanTest.仪器复位))
                    {
                        this.Invoke(new Action(() => {
                            label60.Text = "复位成功....";


                        }));

                    }
                    else
                        //  Thread.Sleep(300);

                        this.Invoke(new Action(() => {
                            label60.Text = "复位失败....";


                        }));
                }

                catch
                {


                }


               // Thread.Sleep(2000);

            });

        }


        private  void Button10_Click(object sender, EventArgs e)
        {
            //Thread t1 = new Thread(showD1);

            //t1.Start();
            //Thread t = new Thread(showD);

           
            // await showD();
            // Thread.Sleep(4000);

            // t.Abort();
            try
            {
              //  t.Start();

                if (true == Tz3310.InterRuptMe(Parameter.CommanTest.仪器复位))
                {
                   // t.Abort();
                    label60.Text = "复位成功...."; 
                }
                else
                {
                   // t.Abort();
                    label60.Text = "复位失败...."; 

                }
                   
            }

            catch
            {


            }
            //  await ReSetMe();
           // t.Abort();

            //if (true == Tz3310.InterRuptMe(Parameter.CommanTest.仪器复位))
            //{
            //   // Thread.Sleep(300);
               
            //    label60.Text = "复位成功....";
            //}
            //else
            //  //  Thread.Sleep(300);

            //label60.Text = "复位失败....";


        }

        private void Button11_Click(object sender, EventArgs e)
        {

            if (true == Tz3310.InterRuptMe(Parameter.CommanTest.模拟按键中断测量))
            {
                Thread.Sleep(2000);
                //MessageBox.Show("模拟按键中断测量成功");
                //MessageBox.Show("模拟按键中断测量成功");
                label65.Text = "模拟按键中断测量成功";
            }
            else
            {
                Thread.Sleep(2000);
                label65.Text = "模拟按键中断测量失败";

               // MessageBox.Show("模拟按键中断测量失败");


            }

        }

        private void Button12_Click(object sender, EventArgs e)
        {
            if (true == Tz3310.InterRuptMe(Parameter.CommanTest.模拟按键中断测量))
            {
                Thread.Sleep(2000);
                MessageBox.Show("模拟按键中断测量成功");
            }
            else
            {
                Thread.Sleep(2000);

                MessageBox.Show("模拟按键中断测量失败");


            }

        }

        private void TextBox50_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox50.Text) >= 3 && Convert.ToInt32(textBox50.Text) <= 200)
                {
                    //操作代码
                    label61.Text = "";
                }
                else
                {
                    label61.Text = "电阻在3-200范围";

                }
            }
            catch (FormatException)
            {
                label61.Text = "电阻在3-200范围";

            }
            //if (textBox50.Text!=null)
            //{
            //    if (Convert.ToInt32(textBox50.Text) >= 3 && Convert.ToInt32(textBox50.Text) <= 200)
            //    {
            //        label61.Text = "";
            //    }
            //    else
            //    {
            //        label61.Text = "电阻在3-200范围";
            //    }

            //}
            //else
            //{
            //    label61.Text = "请输入电阻值";

            //}

        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox16.Text) >= 1 && Convert.ToInt32(textBox16.Text) <= 100)
                {
                    label62.Text = "";
                }
                else
                {
                    label62.Text = "延迟1-100范围";
                }
            }
            catch (FormatException)
            {
                label62.Text = "延迟1-100范围";

            }
        }

        private void Label34_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            byte[] testMind = Tz3310.ISetPraJydz(comboBox9.SelectedIndex, comboBox12.SelectedIndex, int.Parse(textBox17.Text)
                , int.Parse(textBox19.Text), int.Parse(textBox18.Text), int.Parse(textBox8.Text));
            Thread.Sleep(1000);
            label58.Text = "";
            bool kind = Tz3310.StartTest(testMind);
            if (kind == true)
            {
                Thread.Sleep(1000);
                MessageBox.Show("切换线成功");
            }
            else
            {
                Thread.Sleep(1000);
                MessageBox.Show("切换线失败");
            }
        }

        public async Task ReadJydz()
        {
            await Task.Run(() =>
            {
                //...

               while(true)
                { 
                    string[] RecData = Tz3310.ReadTestData(Parameter.TestKind.绝缘电阻);
                    if (RecData != null)
                    {
                        try
                        {
                            for (int i = 0; i < RecData.Length; i++)
                            {
                                if (RecData[i] == null)
                                    RecData[i] = "NONE";
                            }

                            if (RecData.Length == 4)
                            {
                                if (RecData[RecData.Length - 1] == "0")
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        textBox27.Text = RecData[0].ToString();
                                        textBox26.Text = RecData[1].ToString();
                                        textBox25.Text = RecData[2].ToString();

                                    }));

                                }
                                else if (RecData[RecData.Length - 1] == "1")
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        textBox6.Text = RecData[0].ToString();
                                        textBox1.Text = RecData[1].ToString();
                                        textBox7.Text = RecData[2].ToString();

                                    }));

                                }

                            }
                            else
                            {
                                if (RecData[RecData.Length - 1] == "2")
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        textBox27.Text = RecData[0].ToString();
                                        textBox26.Text = RecData[0].ToString();
                                        textBox25.Text = RecData[0].ToString();
                                        textBox6.Text = RecData[0].ToString();
                                        textBox1.Text = RecData[0].ToString();
                                        textBox7.Text = RecData[0].ToString();

                                    }));
                                }
                            }



                        }
                        catch
                        { }

                        Thread.Sleep(100);
                    }
                    


                }

            });
        }
            private async void Button4_Click(object sender, EventArgs e)
              {

            await ReadJydz();
            //string[] RecData = Tz3310.ReadTestData(Parameter.TestKind.绝缘电阻);
          //  string[] RecData =await Tz3310.CirCleReadTestData(Parameter.TestKind.绝缘电阻);
            //try
            //{
            //    for (int i = 0; i < RecData.Length; i++)
            //    {
            //        if (RecData[i] == null)
            //            RecData[i] = "NONE";
            //    }

            //    if (RecData.Length == 4)
            //    {
            //        if (RecData[RecData.Length - 1] == "0")
            //        {
            //            this.Invoke(new Action(() =>
            //            {
            //                textBox27.Text = RecData[0].ToString();
            //                textBox26.Text = RecData[1].ToString();
            //                textBox25.Text = RecData[2].ToString();

            //            }));

            //        }
            //        else if (RecData[RecData.Length - 1] == "1")
            //        {
            //            this.Invoke(new Action(() =>
            //            {
            //                textBox6.Text = RecData[0].ToString();
            //                textBox1.Text = RecData[1].ToString();
            //                textBox7.Text = RecData[2].ToString();

            //            }));

            //        }

            //    }
            //    else
            //    {
            //        if (RecData[RecData.Length - 1] == "2")
            //        {
            //            this.Invoke(new Action(() =>
            //            {
            //                textBox27.Text = RecData[0].ToString();
            //                textBox26.Text = RecData[0].ToString();
            //                textBox25.Text = RecData[0].ToString();
            //                textBox6.Text = RecData[0].ToString();
            //                textBox1.Text = RecData[0].ToString();
            //                textBox7.Text = RecData[0].ToString();

            //            }));
            //        }
            //    }



            //}
            //catch
            //{ }
          
           


        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox2.Text) <= 9)
                {
                    //操作代码
                    label63.Text = "";
                }
                else
                {
                    label63.Text = "识别码1-9范围";

                }
            }
            catch (FormatException)
            {
                label63.Text = "识别码0-9范围";

            }
        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox17.Text) >= 1 && Convert.ToInt32(textBox17.Text) <= 100)
                {
                    //操作代码
                    label64.Text = "";
                }
                else
                {
                    label64.Text = "电阻在1-100范围";

                }
            }
            catch (FormatException)
            {
                label64.Text = "电阻在1-100范围";

            }
        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox19.Text) >= 1 && Convert.ToInt32(textBox19.Text) <= 100)
                {
                    //操作代码
                    label64.Text = "";
                }
                else
                {
                    label64.Text = "绝缘下限在1-100范围";

                }
            }
            catch (FormatException)
            {
                label64.Text = "绝缘下限在1-100范围";

            }
        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox18.Text) >= 100 && Convert.ToInt32(textBox18.Text) <= 150)
                {
                    //操作代码
                    label64.Text = "";
                }
                else
                {
                    label64.Text = "吸收比范围在100-150";

                }
            }
            catch (FormatException)
            {
                label64.Text = "吸收比范围在100-150";

            }
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox8.Text) >= 0 && Convert.ToInt32(textBox8.Text) <= 9)
                {
                    //操作代码
                    label63.Text = "";
                }
                else
                {
                    label63.Text = "识别码0-9范围";

                }
            }
            catch (FormatException)
            {
                label63.Text = "识别码0-9范围";

            }
        }

        private void Label17_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Button14_Click(object sender, EventArgs e)
        {
            if (true == Tz3310.InterRuptMe(Parameter.CommanTest.模拟按键中断测量))
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);

                    this.Invoke(new Action(() =>
                    {
                        label63.Text = "模拟按键中断测量失败成功";
                    }));
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);

                    this.Invoke(new Action(() =>
                    {
                        label63.Text = "模拟按键中断测量失败失败";
                    }));
                });


            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (true == Tz3310.InterRuptMe(Parameter.CommanTest.模拟按键中断测量))
            {
                Thread.Sleep(2000);
                MessageBox.Show("模拟按键中断测量成功");
            }
            else
            {
                Thread.Sleep(2000);

                MessageBox.Show("模拟按键中断测量失败");


            }
        }

        private  void Button15_Click(object sender, EventArgs e)
        {
            string[] RecData = Tz3310.ReadTestData(Parameter.TestKind.介质损耗);

            if (RecData != null)
            {
                try
                {
                    for (int i = 0; i < RecData.Length; i++)
                    {
                        if (RecData[i] == null)
                            RecData[i] = "NONE";
                    }
                }
                catch { }


                if (RecData[RecData.Length - 1] == "0")
                {
                    this.Invoke(new Action(() =>
                    {
                        textBox3.Text = RecData[1].ToString();
                        textBox4.Text = RecData[2].ToString();
                        textBox5.Text = RecData[3].ToString();

                    }));

                }
                else if (RecData[RecData.Length - 1] == "1")
                {
                    this.Invoke(new Action(() =>
                    {
                        textBox22.Text = RecData[1].ToString();
                        textBox21.Text = RecData[2].ToString();
                        textBox23.Text = RecData[3].ToString();
                        textBox20.Text = RecData[4].ToString();


                    }));



                }
                else
                {
                    if (RecData[RecData.Length - 1] == "2")
                    {
                        this.Invoke(new Action(() =>
                        {
                            textBox27.Text = RecData[0].ToString();
                            textBox26.Text = RecData[0].ToString();
                            textBox25.Text = RecData[0].ToString();
                            textBox6.Text = RecData[0].ToString();
                            textBox1.Text = RecData[0].ToString();
                            textBox7.Text = RecData[0].ToString();

                        }));
                    }
                }

            }

            else
            {
                textBox3.Text = "none";
                textBox4.Text = "none";
                textBox5.Text = "none";
            }


        }


        public async Task CloseCurrent()
        {
            await Task.Run(() =>
            {
                if (true == Tz3310.ShutDownOutCurrent(0))
                {
                    this.Invoke(new Action(() =>
                    {
                        button16.Text = "正在关闭...";
                        button16.ForeColor = System.Drawing.Color.Red;//改变按钮中文字的颜色


                    }));
                  

                  //string[] recD=  Tz3310.ReadTestData(Parameter.TestKind.读取放电数据);
                    while (true)
                    {
                        string[] RecData = Tz3310.ReadTestData(Parameter.TestKind.读取放电数据);
                        if (RecData != null)
                        {
                            //  continue;

                            try
                            {
                                for (int i = 0; i < RecData.Length; i++)
                                {
                                    if (RecData[i] == null)
                                        RecData[i] = "NONE";
                                }
                            }
                            catch { }


                            if (RecData.Length==4)
                            {
                                try
                                {
                                    if(RecData[0]=="1")
                                    {
                                        this.Invoke(new Action(() =>
                                        {
                                            label73.Text = "正在放电...";
                                            button16.Text = "正在放电...";
                                            //this.Invoke(new Action(() =>
                                            //{
                                                textBox53.Text = RecData[1].ToString();
                                                textBox52.Text = RecData[2].ToString();
                                                textBox51.Text = RecData[3].ToString();

                                            //}));

                                        }));
                                    }
                                    else if(RecData[0] == "2")
                                    {
                                        this.Invoke(new Action(() =>
                                        {
                                            label73.Text = "放电完成";
                                            button16.Text = "放电完成";

                                        }));
                                    }
                                    else if (RecData[0] == "\0")
                                    {
                                        this.Invoke(new Action(() =>
                                        {
                                            label73.Text = "正在读取";
                                            button16.Text = "正在读取";

                                        }));
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() =>
                                        {
                                            label73.Text = "无需放电";
                                            button16.Text = "无需放电";

                                        }));
                                    }

                                }
                                catch (Exception)
                                {


                                }


                            }
                         

                        }

                        else
                        {
                            this.Invoke(new Action(() =>
                            {
                                textBox53.Text = "没有读到数据";
                                textBox52.Text = "没有读到数据";
                                textBox51.Text = "没有读到数据";

                            }));

                        }

                        Thread.Sleep(1000);

                    }

                }




            });
        }

        private  void Button16_Click(object sender, EventArgs e)
        {
            if(true== Tz3310.InterRuptMe(Parameter.CommanTest.判断直流电阻稳定状态))
            {
                Thread.Sleep(3000);
               if(true== Tz3310.ShutDownOutCurrent(0))
                {
                 string[] recbuffer=Tz3310.ReadTestData(Parameter.TestKind.读取放电数据);
                    if(recbuffer!=null)
                    {
                        if (recbuffer[0].ToString() == "1")
                            label73.Text = "正在放电";
                        if (recbuffer[0].ToString() == "2")
                            label73.Text = "放电完成";
                        textBox53.Text = recbuffer[1].ToString();
                        textBox23.Text = recbuffer[2].ToString();
                        textBox51.Text = recbuffer[3].ToString();
                    }

                }
                else
                {
                    MessageBox.Show("电流稳定状态判断失败");
                    //电流稳定状态判断失败

                }


 ;
                }
            else
            {
                //电流稳定状态判断失败
            }
           // Tz3310.InterRuptMe(Parameter.CommanTest.判断直流电阻稳定状态);

           // await CloseCurrent();

        }

        private void Label60_Click(object sender, EventArgs e)
        {

        }

        private void Button17_Click(object sender, EventArgs e)
        {
           if(true == Tz3310.InterRuptMe(Parameter.CommanTest.判断直流电阻稳定状态))
            {

            }

        }

        private void Button19_Click(object sender, EventArgs e)
        {
           if(true == Tz3310.ShutDownOutCurrent(0))
            {

            }
        }

       

        private void Button18_Click_1(object sender, EventArgs e)
        {
          
        }
        private async  Task ReadCurrent()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    Thread.Sleep(4000);

                    this.Invoke(new Action(() =>
                    {
                        string[] recbuffer = Tz3310.ReadTestData(Parameter.TestKind.读取放电数据);
                        if (recbuffer != null)
                        {
                            if (recbuffer[0].ToString() == "1")
                                label73.Text = "正在放电";
                            if (recbuffer[0].ToString() == "2")
                                label73.Text = "放电完成";
                            textBox53.Text = recbuffer[1].ToString();
                            textBox52.Text = recbuffer[2].ToString();
                            textBox51.Text = recbuffer[3].ToString();
                        }
                    }));
                }
          
            });

        }


        private async void Button17_Click_1(object sender, EventArgs e)
        {

            Tz3310.InterRuptMe(Parameter.CommanTest.判断直流电阻稳定状态);

            await ReadCurrent();



        }

        private void Button19_Click_1(object sender, EventArgs e)
        {
            Tz3310.ShutDownOutCurrent(0);
        }

        private void Button20_Click(object sender, EventArgs e)
        {
           string[] yz= Tz3310.ReadTestData(Parameter.TestKind.有载分接);
         
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            //ArrayList tempD = Tz3310.RradWaveform(0);
            //ArrayList t2 = tempD;

            
        }

        public async Task ReadBig()
        {
            await Task.Run(() =>
            {
               // byte[] sendc = { 0x4f, 0, 0x4f };
               // Tz3310.Sendc(sendc);
            });

        }

        internal short[] Wdata = new short[24008];
        public async Task TTT()
        {

            await Task.Run(() => {
               // string[] yz = Tz3310.ReadTestData(Parameter.TestKind.有载分接);

                Wdata= Tz3310.GetWaveFormData;

                //await ReadBig();
                if (Wdata == null)
                    MessageBox.Show("读取超时，请重试");
                else
                // MessageBox.Show(Wdata.Length.ToString());
                {
                 //   t1.Abort();
                    this.Invoke(new Action(() =>
                    {
                        label76.Text = "读取完成";
                    }));
                    Form2 F2 = new Form2(Wdata);
                    F2.StartPosition = FormStartPosition.CenterScreen;
                    F2.ShowDialog();
                }
                    
            });

        }

        Thread t1;

        private async void Button22_Click(object sender, EventArgs e)
        {

            //label66.Text = Rec[0].ToString();
            t1 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            label76.Text = "▌";
                        }));
                        Thread.Sleep(600);

                        this.Invoke(new Action(() =>
                        {
                            label76.Text = "▌▌";
                        }));
                        Thread.Sleep(600); this.Invoke(new Action(() =>
                        {
                            label76.Text = "▌▌▌";
                        }));
                        Thread.Sleep(600); this.Invoke(new Action(() =>
                        {
                            label76.Text = "▌▌▌▌";
                        }));
                        Thread.Sleep(600);
                        this.Invoke(new Action(() =>
                        {
                            label76.Text = "▌▌▌▌▌";
                        }));
                        Thread.Sleep(600);
                        this.Invoke(new Action(() =>
                        {
                            label76.Text = "▌▌▌▌▌▌";
                        }));
                        Thread.Sleep(600);
                        this.Invoke(new Action(() =>
                        {
                            label76.Text = "▌▌▌▌▌▌▌";
                        }));
                        Thread.Sleep(600);
                    }

                    catch { }


                }

            });

            t1.Start();
            await TTT();

            // List<byte> dat = Tz3310.OtherData;
        }

        private void TabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
