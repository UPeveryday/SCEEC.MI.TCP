using System;
using System.Collections.Generic;
using System.Linq;
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
using SCEEC.NET;
using System.IO.Ports;
using System.Threading;
using LiveCharts;
using LiveCharts.Wpf;

namespace WpfApplication1
{

    class testDataReset
    {
        private float _FRE;
        private float _An;
        private float _Ax;
        private float _Ph;
        private float _Umean;
        // private double _Df;

        private double _Df_tan;
        private double _Df_tan_75;
        private double _Df_tan_Per100;
        private double _Df_tan_75_Per100;
        private double _Pf_cos;
        private double _Pf_cos_75;
        private double _Pf_cos_Per100;
        private double _Pf_cos_75_Per100;
        private double _Urms;
        private double _Urms_sqrt3;
        private double _Urect_mean;
        private double _In_rms;
        private double _Ix_rms;
        private double _Fre;
        private double _Fre_database;
        private double _Zx;
        private double _Yx;
        private double _I_mag_LP;
        private double _sita_Zx;
        private double _Cx;
        private double _S;
        private double _P;
        private double _Q;
        private double _P_25;
        private double _P_10;
        private double _Cn;
        private double _temper;

        private double _I_fe_rp;
        private double _Cp;
        private double _Rp_CR;
        private double _Rs_CR;
        private double _Cs;
        private double _Ls;
        private double _Rs_LR;
        private double _Lp;
        private double _Rp_LR;

        public testDataReset(float FRE, float An, float Ax, float Ph, float Umean, double Cn, double temper)
        {
            this._FRE = FRE;
            // this._age = Age;
            this._An = An;
            this._Ax = Ax;
            this._Ph = Ph;
            this._Umean = Umean;
            this._temper = temper;//提供的值，修改
            this._Cn = Cn;//提供的值，修改

            double w = 2 * Math.PI * FRE;

            this._Df_tan = Math.Tan(Ph);
            this._Df_tan_75 = _Df_tan * Math.Pow(1.3, (Convert.ToDouble((75 - temper)) / 10));//t1为设定的值
            //                                                                                                              // string result = string.Format("{0:0.00%}", percent);//得到5.88%
            this._Df_tan_Per100 = _Df_tan * 100;
            //this._Df_tan_75_Per100 = Convert.ToDouble(string.Format("{0:0.00%}", _Df_tan_75));
            this._Df_tan_75_Per100 = _Df_tan_75 * 100;

            this._Pf_cos = Math.Cos(Math.PI / 2 - Ph);
            this._Pf_cos_75 = Math.Sqrt((_Df_tan_75 * _Df_tan_75) / (_Df_tan_75 * _Df_tan_75 + 1));
            //this._Pf_cos_Per100 = Convert.ToDouble(string.Format("{0:0.00%}", _Pf_cos));
            //this._Pf_cos_75_Per100 = Convert.ToDouble(string.Format("{0:0.00%}", _Pf_cos_75));
            this._Pf_cos_Per100 = _Pf_cos * 100;
            this._Pf_cos_75_Per100 = _Pf_cos_75 * 100;
            this._Urms = An / (2 * Math.PI * FRE * _Cn) / Math.Sqrt(2);
            this._Urms_sqrt3 = _Urms * Math.Sqrt(3);
            this._Urect_mean = _Umean;
            this._In_rms = An / Math.Sqrt(2);
            this._Ix_rms = Ax / Math.Sqrt(2);

            this._Fre = FRE;
            this._Fre_database = FRE;

            this._Zx = An * (2 * Math.PI * FRE * _Cn * Ax);//提供接口
            this._Yx = (2 * Math.PI * FRE * _Cn * Ax) / An;//提供接口
            this._sita_Zx = Ph;
            this._Cn = 100e-12;//提供的值，修改
            
            this._Cx = Ax * _Cn / (An * Math.Cos(Ph));

            this._S = Ax * An / (2 * Math.PI * FRE * _Cn);//Cn
            this._P = Ax * An / (2 * Math.PI * FRE * _Cn) * Math.Cos(Ph);
            this._Q = Ax * An / (2 * Math.PI * FRE * _Cn) * Math.Sin(Ph);
            this._P_10 = Math.PI;//提供的值，修改
            this._P_25 = Math.PI;//提供的值，修改

            this._sita_Zx = Math.PI / 2 - Ph;
            this._I_mag_LP = Ax * Math.Cos(Ph);
            this._I_fe_rp = Ax * Math.Sin(Ph);
            this._Cp = _I_fe_rp;
            this._Rp_CR = An * Math.Sin(Ph) / (w * _Cn * Ax);
            this._Rs_CR = _Cn * (1 + Math.Tan(Ph) * Math.Tan(Ph));
            this._Cs = _Cp * (Math.Tan(Ph) * Math.Tan(Ph) / (1 + Math.Tan(Ph) * Math.Tan(Ph)));
            this._Ls = Math.Sqrt(1 / (_Cs * w * w));
            this._Rs_LR = _Rs_CR;
            this._Lp = Math.Sqrt(1 / (_Cp * w * w));
            this._Rp_LR = _Cp;


        }

        public double Df_tan
        {
            get { return _Df_tan; }

        }
        public float FRE
        {
            get { return _FRE; }
        }
        public float An
        {
            get { return _An; }
        }
        public float Ax
        {
            get { return _Ax; }
        }
        public float Ph
        {
            get { return _Ph; }
        }
        public float Umean
        {
            get { return _Umean; }
        }

        public double I_fe_rp
        {
            get { return _I_fe_rp; }
        }
        public double Cp
        {
            get { return _Cp; }
        }
        public double Rp_CR
        {
            get { return _Rp_CR; }
        }
        public double Rs_CR
        {
            get { return _Rs_CR; }
        }
        public double Cs
        {
            get { return _Cs; }
        }
        public double Ls
        {
            get { return _Ls; }
        }
        public double Rs_LR
        {
            get { return _Rs_LR; }
        }
        public double Lp
        {
            get { return _Lp; }
        }
        public double Rp_LR
        {
            get { return _Rp_LR; }
        }
        //..........................




        public double Zx
        {
            get { return _Zx; }
        }
        public double Yx
        {
            get { return _Yx; }
        }


        public double Df_tan_75
        {
            get { return _Df_tan_75; }
        }
        public double Df_tan_Per100
        {
            get { return _Df_tan_Per100; }
        }
        public double Df_tan_75_Per100
        {
            get { return _Df_tan_75_Per100; }
        }
        public double Pf_cos
        {
            get { return _Pf_cos; }
        }
        public double Pf_cos_75
        {
            get { return _Pf_cos_75; }
        }
        public double Pf_cos_Per100
        {
            get { return _Pf_cos_Per100; }
        }
        public double Pf_cos_75_Per100
        {
            get { return _Pf_cos_75_Per100; }
        }
        public double Urms
        {
            get { return _Urms; }
        }
        public double Urms_sqrt3
        {
            get { return _Urms_sqrt3; }
        }
        public double Urect_mean
        {
            get { return _Urect_mean; }
        }
        public double In_rms
        {
            get { return _In_rms; }
        }
        public double Ix_rms
        {
            get { return _Ix_rms; }
        }
        public double Fre
        {
            get { return _Fre; }
        }
        public double Fre_database
        {
            get { return _Fre_database; }
        }
        public double sita_Zx
        {
            get { return _sita_Zx; }
        }
        public double Cx
        {
            get { return _Cx; }
        }
        public double S
        {
            get { return _S; }
        }
        public double P
        {
            get { return _P; }
        }
        public double Q
        {
            get { return _Q; }
        }
        public double P_25
        {
            get { return _P_25; }
        }
        public double P_10
        {
            get { return _P_10; }
        }
        public double Cn
        {
            get { return _Cn; }
        }
        // _I_mag_LP

        public double temper
        { get { return _temper; } }

        public double I_mag_LP
        {
            get { return _I_mag_LP; }
        }


    }

    //class scope
    //{
    //    private double _scope;

    //    public scope(double scopeData)
    //    {
    //        this._scope = scopeData;
    //    }
    //    public double scopeData
    //    {
    //        get { return _scope; }

    //    }

    //}



    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        SerialClass sc = new SerialClass();



        public MainWindow()
        {
        
            InitializeComponent();

           


        }
        //存储波形数据
       // float[] scopeData;
        public void screceivee(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            //Thread.Sleep(50);
            try
            {
                if(bits[0]==0x41)
                {
                    AnalysisData(bits);
                    dataView();
                }
                else if(bits[0]==0x42)
                {
                    AnalysisData(bits);
                    newline();

                    foreach (float a in scopeData)
                    {
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            // textValue.Text += "原始数据为：" + Encoding.ASCII.GetString(bits).Trim() + "\r\n";
                            // textBlock.Text = a.scopeData.ToString();

                            textBlock.Text += a.ToString()+"\r\n" ;
                        }));

                    }


                    //this.Dispatcher.BeginInvoke(new Action(() =>
                    //{

                    //    // textValue.Text += "原始数据为：" + Encoding.ASCII.GetString(bits).Trim() + "\r\n";
                    //    textBlock.Text += bits.Length.ToString();


                    //}));



                }


            }
            catch
            {

            }
            //textValue.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    textValue.Text += "原始数据为：" + Encoding.ASCII.GetString(bits).Trim() + "\r\n";


            //}));



            //  result.Add(dataTemp);



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> temp5 = new Dictionary<int, string>()
            {
                  {1,"DF(tanδ)"},
                  {2,"DF(tan δ)@75°"},
                  {3,"DF % (tanδ)"},
                  {4,"DF % (tan δ)@75°"},
                  {5,"PF(cos ψ)"},
                  {6,"PF(cos ψ)@75° "},
                  {7,"PF % (cos ψ)"},
                  {8,"PF % (cos ψ)@75°"},
                  {9,"U rms"},
                  {10,"U rms /√3"},
                  {11,"U rect.mean"},
                  {12,"In rms"},
                  {13,"Ix rms"},
                  {14,"频率"},
                  {15,"基频"},
                  {16,"Zx"},
                  {17,"Yx"},
                  {18,"ψ(Zx)"},
                  {19,"I mag(Lp)"},
                  {20,"I fe(Rp)"},
                  {21,"Cx"},
                  {22,"Cn"},
                  {23,"Cp(Zx=Cp||Rp)"},
                  {24,"Rp(Zx=Cp||Rp)"},
                  {25,"Rs(Zx=Cs+Rs)"},
                  {26,"Cs(Zx=Cs+Rs)"},
                  {27,"Ls(Zx=Ls+Rs)"},
                  {28,"Rs(Zx=Ls+Rs)"},
                  {29,"Lp(Zx=Lp || Rp)"},
                  {30,"Rp(Zx=Lp || Rp)"},
                  {31,"视在功率S"},
                  {32,"有功功率P"},
                  {33,"无功功率Q"},
                  {34,"有功功率@2.5KV"},
                  {35,"有功功率@10KV"},
                  {36,"环境温度"},
                  {37,"绝缘温度"},
                  {38,"相对湿度"},
                   {39,"温度修正"},
                {40,"波形"}
            };
            comboBox.ItemsSource = temp5;
            comboBox.SelectedValuePath = "Key";
            comboBox.DisplayMemberPath = "Value";

            comboBox.SelectedIndex = 2;
            comboBox_Copy.ItemsSource = temp5;
            comboBox_Copy.SelectedValuePath = "Key";
            comboBox_Copy.DisplayMemberPath = "Value";

            comboBox_Copy.SelectedIndex = 2;


            // linestart();
        }

        private void open_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sc.closePort();

                sc.setSerialPort("COM6", 115200, 8, 1);
                sc.openPort();
                sc.DataReceived += new SerialClass.SerialPortDataReceiveEventArgs(screceivee);

                textValue_Copy.Text += "窗口打开成功，处于监听状态";

            }
            catch
            {

            }
        }

        //List<testData> testDataList = new List<testData>();
        List<testDataReset> testDataList1 = new List<testDataReset>();
        //  List<scope> scopelist = new List<scope>();
        List<float> scopeData = new List<float>();

        private void DataUi(System.Windows.Controls.ComboBox comb, System.Windows.Controls.TextBox txtb)
        {
           

            try
            {

                //testDataList1.Remove();
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (testDataReset a in testDataList1)
                    {
                        #region
                        switch (comb.SelectedIndex)
                        {
                            case 0:

                                    txtb.Text = a.Df_tan.ToString();
                                 
                                break;
                            case 1:
                              
                                    txtb.Text = a.Df_tan_75.ToString();


                                
                                break;
                            case 2:
                                
                                    txtb.Text = a.Df_tan_Per100.ToString();


                               
                                break;
                            case 3:
                                
                                    txtb.Text = a.Df_tan_75_Per100.ToString();
                                
                                break;
                            case 4:
                                

                                    txtb.Text = a.Pf_cos.ToString();

                                
                                break;
                            case 5:
                               
                                    txtb.Text = a.Pf_cos_75.ToString();


                               
                                break;
                            case 6:
                               

                                    txtb.Text = a.Pf_cos_Per100.ToString();

                               
                                break;
                            case 7:
                               
                                    txtb.Text = a.Pf_cos_75_Per100.ToString();

                                break;
                            case 8:
                               
                                    txtb.Text = a.Urms.ToString();

                               
                                break;
                            case 9:
                               
                                    txtb.Text = a.Urms_sqrt3.ToString();

                                
                                break;
                            case 10:
                               
                                    txtb.Text = a.Urect_mean.ToString();

                              
                                break;
                            case 11:
                                
                                    txtb.Text = a.In_rms.ToString();

                               
                                break;
                            case 12:
                                
                                    txtb.Text = a.Ix_rms.ToString();

                               
                                break;
                            case 13:
                               
                                    txtb.Text = a.Fre.ToString();

                                break;
                            case 14:
                               
                                    txtb.Text = a.Fre_database.ToString();

                               
                                break;
                            case 15:
                                
                                    txtb.Text = a.Zx.ToString();

                                
                                break;
                            case 16:
                               
                                    txtb.Text = a.Yx.ToString();
                                break;
                            case 17:
                               
                                    txtb.Text = a.sita_Zx.ToString();

                               
                                break;
                            case 18://I mag(Lp)
                               
                                    txtb.Text = a.Df_tan_Per100.ToString();

                                break;
                            case 19://I fe(Rp
                               
                                    txtb.Text = a.Df_tan_Per100.ToString();

                                break;
                            case 20:
                                
                                    txtb.Text = a.Cx.ToString();

                                break;
                            case 21:
                               
                                    txtb.Text = a.Cn.ToString();

                                break;
                            case 22://Cp(Zx=Cp||Rp)
                               
                                    txtb.Text = a.Fre.ToString();

                                break;
                            case 23://Rp(Zx=Cp||Rp)
                               
                                    txtb.Text = a.Df_tan_Per100.ToString();
                                break;
                            case 24://Rs(Zx=Cs+Rs)"}
                               
                                    txtb.Text = a.Df_tan_Per100.ToString();
                                break;
                            case 25://Cs(Zx=Cs+Rs)"},
                              
                                    txtb.Text = a.Df_tan_Per100.ToString();
                                break;
                            case 26://"Ls(Zx=Ls+Rs)"},
                               
                                    txtb.Text = a.Df_tan_Per100.ToString();
                                break;
                            case 27://Rs(Zx=Ls+Rs)"},
                               
                                    txtb.Text = a.Df_tan_Per100.ToString();
                                break;
                            case 28://Lp(Zx=Lp || Rp)"},
                              
                                    txtb.Text = a.Df_tan_Per100.ToString();
                                break;
                            case 29://Rp(Zx=Lp || Rp)"}
                             
                                    txtb.Text = a.Df_tan_Per100.ToString();

                                break;
                            case 30:
                              
                                    txtb.Text = a.S.ToString();

                                break;
                            case 31:
                            
                                    txtb.Text = a.S.ToString();

                                break;
                            case 32:
                              
                                    txtb.Text = a.P.ToString();
                                break;
                            case 33:
                             
                                    txtb.Text = a.Q.ToString();

                                break;
                            case 34:
                              
                                    txtb.Text = a.P_10.ToString();
                                break;
                            case 35:
                              
                                    txtb.Text = a.P_25.ToString();
                                break;



                        }
                        #endregion

                    }

                }));


               
                
            }
            catch(Exception ex)
            {
                textBox.Text = ex.Message.ToString();
            }
        }

        public  void dataView()
        {

           
            Thread th1 = new Thread(()=>
            {
               
                    DataUi(comboBox, textBox);

                    DataUi(comboBox_Copy, textBox_Copy);


                //this.Dispatcher.BeginInvoke(new Action(() =>
                //{
                //}));
            });
            th1.IsBackground = true;
            th1.Start();
            Thread th2 = new Thread(() =>
            {
                foreach (testDataReset a in testDataList1)
                {


                    textValue.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //  DataUi(comboBox, textBox);
                        //   DataUi(comboBox_Copy,textBox_Copy);

                        FRE.Text = a.FRE.ToString();
                        An.Text = a.An.ToString();
                        Ax.Text = a.Ax.ToString();
                        Ph.Text = a.Ph.ToString();
                        Umean.Text = a.Umean.ToString();
                        txt_1.Text = a.Rp_LR.ToString();


                    }));
                }

            });

            th2.IsBackground = true;
            th2.Start();

        }

        private void  AnalysisData(byte[] datatest)
        {
            float[] datalist = new float[5];

            if (datatest[0] == 0x41)
            {
                if (datatest[1] == 0x01 || datatest[1] == 0x02 || datatest[1] == 0x03)
                {
                    byte[] FRE_DATA = { datatest[2], datatest[3], datatest[4], datatest[5] };
                    float FRE = BitConverter.ToSingle(FRE_DATA, 0);
                    datalist[0] = FRE;

                    byte[] An_DATA = { datatest[6], datatest[7], datatest[8], datatest[9] };
                    float An = BitConverter.ToSingle(An_DATA, 0);
                    datalist[1] = An;

                    byte[] Ax_DATA = { datatest[10], datatest[11], datatest[12], datatest[13] };
                    float Ax = BitConverter.ToSingle(Ax_DATA, 0);
                    datalist[2] = Ax;

                    byte[] Ph_DATA = { datatest[14], datatest[15], datatest[16], datatest[17] };
                    float Ph = BitConverter.ToSingle(Ph_DATA, 0);
                    datalist[3] = Ph;

                    byte[] Umean_DATA = { datatest[18], datatest[19], datatest[20], datatest[21] };
                    float Umean = BitConverter.ToSingle(Umean_DATA, 0);
                    datalist[4] = Umean;
                    testDataReset testD = new testDataReset(FRE, An, Ax, Ph, Umean, 1.0036e-9, 20);
                    testDataList1.Clear();
                    testDataList1.Add(testD);
                    //  return datalist;

                }
                else
                {
                    // return null;
                }
            }
            if(datatest[0] == 0x42)
            {
                //删除内部数据
                scopeData.Clear();
                if (datatest.Length==5201)
                {
                    for(int i=0;i<1300;i++)
                    {
                        float tempdatascope = BitConverter.ToSingle(datatest, 1+4*i);
                        scopeData.Add(tempdatascope);

                      //  scope scp = new scope(scopeData);
                       // scopelist.Clear();
                      //  scopelist.Add(scp);

                    }


                }

            }

           //  return null;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            byte[] buff = { 0x41, 0x01 };
            sc.SendDataByte(buff, 0, buff.Length);
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            sc.closePort();
            textValue_Copy.Text = "串口关闭成功";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
                byte[] buff = { 0x42 };
                sc.SendDataByte(buff, 0, buff.Length);


           

        }

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        private double _trend;
      //  private double[] temp;
        
        private void newline()
        {
            double[] temp = new double[1300];

     //   float[] sd = scopeData.ToArray();
            foreach(float a in scopeData)
            {
                int i = 0;
                temp[i] = double.Parse(a);
                i++;

            }
        
            
            LineSeries mylineseries = new LineSeries();
            //设置折线的标题
            mylineseries.Title = "Temp";
            // mylineseries.StrokeDashArray = new System.Windows.Media.DoubleCollection { 1 };//虚线
            mylineseries.Stroke = System.Windows.Media.Brushes.Red;
            //折线填充色
            //

            //   mylineseries.Fill = System.Windows.Media.Brushes.Yellow;
            //折线图直线形式
            mylineseries.LineSmoothness = 4;
            //折线图的无点样式

            mylineseries.PointGeometry = null;
            //添加横坐标
            Labels = new List<string> { "1", "2" };
            //添加折线图的数据2
            mylineseries.Values = new ChartValues<double>(temp);
            SeriesCollection = new SeriesCollection { };
            SeriesCollection.Add(mylineseries);
            _trend = 18;
            linestart();
            DataContext = this;

        }

        public void linestart()
        {
            Task.Factory.StartNew(() =>
            {
                var r = new Random();
                while (true)
                {
                    Thread.Sleep(1000);
                    _trend = r.Next(-500, 500);
                    //通过Dispatcher在工作线程中更新窗体的UI元素
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        //更新横坐标时间DateTime.Now.ToString()
                        Labels.Add(DateTime.Now.ToString());
                        //Labels.Add("i");
                        //Labels.Add("g");
                        //Labels.Add("k");
                        //Labels.Add("j");

                        Labels.RemoveAt(0);
                        //更新纵坐标数据
                        SeriesCollection[0].Values.Add(_trend);
                        SeriesCollection[0].Values.RemoveAt(0);
                    });
                }
            });
        }


    }
}
