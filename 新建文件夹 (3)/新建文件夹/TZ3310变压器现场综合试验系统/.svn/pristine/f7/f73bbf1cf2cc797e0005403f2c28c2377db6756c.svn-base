using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TZ3310调试程序
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        short[] Tempdata = new short[24008];
        public Form2(short[] Tdata)
        {
            InitializeComponent();

            Tempdata = Tdata;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;


            #region
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.BurlyWood;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.BurlyWood;
            chart1.Series[0].IsVisibleInLegend = false;
            this.chart1.ChartAreas[0].AxisY.IsReversed = true;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.AutoScroll = false;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.AutoScroll = false;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.Size = 15;
            chart1.ChartAreas[0].AxisY.ScrollBar.Size = 15;
            chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chart1.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.SkyBlue;
            chart1.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chart1.ChartAreas[0].AxisY.ScrollBar.ButtonColor = Color.SkyBlue;
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = double.NaN;
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 1;
            chart1.ChartAreas[0].AxisY.ScaleView.SmallScrollSize = double.NaN;
            chart1.ChartAreas[0].AxisY.ScaleView.SmallScrollMinSize = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.Title = "m Ω";
            try
            {
                chart1.Series.Add("A相");
                chart1.Series["A相"].ChartType = SeriesChartType.Line;
                chart1.Series["A相"].Color = Color.Red;

            }
            catch
            {

                MessageBox.Show("line已经绘制完成");
            }
            for (int i = 0; i < 6000; i++)
            {
                if (Tempdata[6000].ToString() == "1")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 500 / 32768);
                if (Tempdata[6000].ToString() == "2")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 1000 / 32768);
                if (Tempdata[6000].ToString() == "3")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 5000 / 32768);
                if (Tempdata[6000].ToString() == "4")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 10000 / 32768);
                if (Tempdata[6000].ToString() == "5")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 50000 / 32768);
                chart1.Series["A相"].Points.AddXY(i + 1, Tempdata[i]);


            }
            #endregion
            #region
            var chartT2 = chart2.ChartAreas[0];
            chartT2.AxisX.IntervalType = DateTimeIntervalType.Number;
            chartT2.AxisX.LabelStyle.Format = "";
            chartT2.AxisY.LabelStyle.Format = "";
            chartT2.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.BurlyWood;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.BurlyWood;
            chart2.Series[0].IsVisibleInLegend = false;
            this.chart2.ChartAreas[0].AxisY.IsReversed = true;
            chart2.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorX.AutoScroll = false;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorY.AutoScroll = false;
            chart2.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart2.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.Size = 15;
            chart2.ChartAreas[0].AxisY.ScrollBar.Size = 15;
            chart2.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chart2.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.SkyBlue;
            chart2.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chart2.ChartAreas[0].AxisY.ScrollBar.ButtonColor = Color.SkyBlue;
            chart2.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = double.NaN;
            chart2.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 1;
            chart2.ChartAreas[0].AxisY.ScaleView.SmallScrollSize = double.NaN;
            chart2.ChartAreas[0].AxisY.ScaleView.SmallScrollMinSize = 1;
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart2.ChartAreas[0].AxisY.Title = "m Ω";
            try
            {

                chart2.Series.Add("B相");
                chart2.Series["B相"].ChartType = SeriesChartType.Line;
                chart2.Series["B相"].Color = Color.Green;


            }
            catch
            {

                MessageBox.Show("line已经绘制完成");
            }

            for (int i = 6002; i < 12002; i++)
            {
                if (Tempdata[12002].ToString() == "1")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 500 / 32768);
                if (Tempdata[12002].ToString() == "2")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 1000 / 32768);
                if (Tempdata[12002].ToString() == "3")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 5000 / 32768);
                if (Tempdata[12002].ToString() == "4")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 10000 / 32768);
                if (Tempdata[12002].ToString() == "5")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 50000 / 32768);
                chart2.Series["B相"].Points.AddXY(i - 6002 + 1, Tempdata[i]);


            }


            #endregion
            #region

            var chartT3 = chart3.ChartAreas[0];
            chartT3.AxisX.IntervalType = DateTimeIntervalType.Number;
            chartT3.AxisX.LabelStyle.Format = "";
            chartT3.AxisY.LabelStyle.Format = "";
            chartT3.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.BurlyWood;
            chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.BurlyWood;
            chart3.Series[0].IsVisibleInLegend = false;
            this.chart3.ChartAreas[0].AxisY.IsReversed = true;
            chart3.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart3.ChartAreas[0].CursorX.AutoScroll = false;
            chart3.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart3.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart3.ChartAreas[0].CursorY.AutoScroll = false;
            chart3.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart3.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            chart3.ChartAreas[0].AxisX.ScrollBar.Size = 15;
            chart3.ChartAreas[0].AxisY.ScrollBar.Size = 15;
            chart3.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chart3.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.SkyBlue;
            chart3.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            chart3.ChartAreas[0].AxisY.ScrollBar.ButtonColor = Color.SkyBlue;
            chart3.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = double.NaN;
            chart3.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 1;
            chart3.ChartAreas[0].AxisY.ScaleView.SmallScrollSize = double.NaN;
            chart3.ChartAreas[0].AxisY.ScaleView.SmallScrollMinSize = 1;
            chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart3.ChartAreas[0].AxisY.Title = "m Ω";
            try
            {

                chart3.Series.Add("C相");
                chart3.Series["C相"].ChartType = SeriesChartType.Line;
                //绘制曲线图
                // chart1.Series["line1"].ChartType = SeriesChartType.Spline;
                // chart1.Series["line1"].XValueMember = Tempdata;
                chart3.Series["C相"].Color = Color.Black;
            }
            catch
            {

                MessageBox.Show("line已经绘制完成");
            }
            for (int i = 12004; i < 18004; i++)
            {
                if (Tempdata[18004].ToString() == "1")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 500 / 32768);
                if (Tempdata[18004].ToString() == "2")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 1000 / 32768);
                if (Tempdata[18004].ToString() == "3")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 5000 / 32768);
                if (Tempdata[18004].ToString() == "4")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 10000 / 32768);
                if (Tempdata[18004].ToString() == "5")
                    Tempdata[i] = Convert.ToInt16(Tempdata[i] * 50000 / 32768);
                chart3.Series["C相"].Points.AddXY(i - 12004 + 1, Tempdata[i]);


            }
            #endregion

           

        }

        Thread t1;
        private void Button1_Click(object sender, EventArgs e)
        {
           
            //chart1.Series.Clear();
            //chart2.Series.Clear();
            //chart3.Series.Clear();

            t1 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌";
                        }));
                        Thread.Sleep(200);

                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌▌";
                        }));
                        Thread.Sleep(200); this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌▌▌";
                            panel2.Visible = true;
                            panel1.Visible = true;
                            panel3.Visible = true;
                            panel4.Visible = true;
                            panel5.Visible = true;
                            panel6.Visible = true;
                        }));
                        Thread.Sleep(200); this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌▌▌▌";
                        }));
                        Thread.Sleep(200);
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌▌▌▌▌";
                        }));
                        Thread.Sleep(200);
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌▌▌▌▌▌";
                        }));
                        Thread.Sleep(200);
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌▌▌▌▌▌▌";
                        }));
                        Thread.Sleep(200);
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "▌▌▌▌▌▌▌▌";
                        }));
                        Thread.Sleep(200);
                    }

                    catch { }


                }

            });

            t1.Start();

            SetChart1();
            SetChart2();
            SetChart3();


        }
        //  private Point mousePoint = new Point();
        private int aX, aX1;

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {

            panel1.BringToFront();


            if (e.Button == MouseButtons.Left)
            {
                panel1.Left = panel1.Left + (e.X - aX);
            }

            SetChart1();
            SetChart2();
            SetChart3();



        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            aX1 = e.X;

        }

        private int p2_x { get; set; } = 0;
        private int p2_Y { get; set; } = 0;
        private int p1_x { get; set; } = 0;
        private int p1_Y { get; set; } = 0;
        private int p3_x { get; set; } = 0;
        private int p3_Y { get; set; } = 0;
        private int p4_x { get; set; } = 0;
        private int p4_Y { get; set; } = 0;
        private int p5_x { get; set; } = 0;
        private int p5_Y { get; set; } = 0;
        private int p6_x { get; set; } = 0;
        private int p6_Y { get; set; } = 0;
        private void Panel2_MouseMove(object sender, MouseEventArgs e)
        {


            panel2.BringToFront();


            if (e.Button == MouseButtons.Left)
            {
             
                panel2.Left = panel2.Left + (e.X - aX1);

                SetChart1();


            }
        }

        //Point lastPoint = new Point();//上次点的坐标
        //ToolTip tp = new ToolTip();//tooltip展示条
        //                           //绘制竖线坐标
        //Point p1 = new Point(0, 0);
        //Point p2 = new Point(0, 0);

        private void Chart1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Chart1_MouseMove_1(object sender, MouseEventArgs e)
        {
            HitTestResult myTestResult = chart1.HitTest(e.X, e.Y);

            if (myTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                this.Cursor = Cursors.Cross;
                int i = myTestResult.PointIndex;
                DataPoint dp = myTestResult.Series.Points[i];
                //   DataPoint dp1 = myTestResult.Series.Points.Max();

                double doubleXValue = (dp.XValue);
                double doubleYValue = dp.YValues[0];


                Convert.ToInt32(doubleXValue);
                Convert.ToInt32(doubleYValue);

                // label1.Text = doubleXValue.ToString() + doubleYValue.ToString();
                //自我实现值的显示     
                label3.Text = doubleXValue.ToString() + "," + doubleYValue.ToString();



            }
            else
            {
                this.Cursor = Cursors.Default;
            }

            #region
            //  chart1.Refresh();//刷新chart，使用clear会使chart上的图形完全清空
            //Pen pen = new Pen(Color.Red);
            // //Graphics g = chart1.CreateGraphics();
            // string seriesInfo = ""; //tooltip文本
            // if (e.Location != lastPoint)//如果在上次点的位置不进行操作,此处操作会引发chart控件的refresh操作造成界面闪烁
            // {
            //     for (int y = 0; y <= chart1.Size.Height; y++)//线条范围进行碰撞检测
            //     {
            //         HitTestResult result = chart1.HitTest(e.X, y);
            //         if (result.ChartElementType == ChartElementType.DataPoint)
            //         {
            //             foreach (DataPoint dpp in result.Series.Points)//数据点默认样式 使用索引的方式修改偶尔会出现无法正常修改完
            //             {
            //                 dpp.MarkerStyle = MarkerStyle.None;
            //                 dpp.MarkerColor = Color.White;
            //                 dpp.MarkerSize = 0;
            //             }
            //             int i = result.PointIndex;
            //             DataPoint dp = result.Series.Points[i];
            //             dp.MarkerStyle = MarkerStyle.Circle;//捕获到数据点的样式
            //             dp.MarkerColor = Color.Orange;
            //             dp.MarkerSize = 5;
            //             //获取数据点的相对坐标
            //             p1 = new Point((int)chart1.ChartAreas[0].AxisX.ValueToPixelPosition(dp.XValue), 0);
            //             p2 = new Point((int)chart1.ChartAreas[0].AxisX.ValueToPixelPosition(dp.XValue), chart1.Size.Height);
            //             X1 = (int)chart1.ChartAreas[0].AxisX.ValueToPixelPosition(dp.XValue);
            //             Y1 = Convert.ToInt32(dp.YValues[0]);
            //               seriesInfo = string.Format("Y:{0}  X:{1} ", dp.YValues[0], dp.XValue);
            //             break;
            //         }
            //     }
            //     tp.AutoPopDelay = 5000;//展示tooltip
            //     tp.ShowAlways = false;
            //     tp.IsBalloon = true;
            //     tp.SetToolTip(chart1, seriesInfo);
            // }
            // lastPoint = e.Location;//记录本次位置
            //// g.DrawLine(pen, p1, p2);//绘制竖线


            #endregion


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Tupdata.Start();
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();

        }



        private void Chart2_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult myTestResult = chart1.HitTest(e.X, e.Y);
            if (myTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                this.Cursor = Cursors.Cross;
                int i = myTestResult.PointIndex;
                DataPoint dp = myTestResult.Series.Points[i];

                double doubleXValue = (dp.XValue);
                double doubleYValue = dp.YValues[0];
                //X1 = Convert.ToInt32(doubleXValue);
                // Y1 = Convert.ToInt32(doubleXValue);
                // label1.Text = doubleXValue.ToString() + doubleYValue.ToString();
                //自我实现值的显示     
                label2.Text = doubleXValue.ToString() + "," + doubleYValue.ToString();
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Chart3_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult myTestResult = chart1.HitTest(e.X, e.Y);
            if (myTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                this.Cursor = Cursors.Cross;
                int i = myTestResult.PointIndex;
                DataPoint dp = myTestResult.Series.Points[i];

                double doubleXValue = (dp.XValue);
                double doubleYValue = dp.YValues[0];
                Convert.ToInt32(doubleXValue);
                Convert.ToInt32(doubleYValue);
                // label1.Text = doubleXValue.ToString() + doubleYValue.ToString();
                //自我实现值的显示     
                label4.Text = doubleXValue.ToString() + "," + doubleYValue.ToString();
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        private int ax4, ax3, ax5, ax6;
        private void Panel4_MouseDown(object sender, MouseEventArgs e)
        {

            //md 
            ax4 = e.X;
            //
        }

        private void Panel4_MouseMove(object sender, MouseEventArgs e)
        {
            panel4.BringToFront();


            if (e.Button == MouseButtons.Left)
            {
                panel4.Left = panel4.Left + (e.X - ax4);
            }
            SetChart1();
            SetChart2();
            SetChart3();
            //
        }

        private void Panel3_MouseMove(object sender, MouseEventArgs e)
        {
            panel3.BringToFront();


            if (e.Button == MouseButtons.Left)
            {
                panel3.Left = panel3.Left + (e.X - ax3);
            }

            SetChart1();
            SetChart2();
            SetChart3();

            //
        }

        private void Panel3_MouseDown(object sender, MouseEventArgs e)
        {

            //md  
            ax3 = e.X;
            //
        }

        private void Panel5_MouseMove(object sender, MouseEventArgs e)
        {
            panel5.BringToFront();


            if (e.Button == MouseButtons.Left)
            {
                panel5.Left = panel5.Left + (e.X - ax5);
            }
            //md  aX1 = e.X;
            SetChart1();
            SetChart2();
            SetChart3();
            //
        }

        private int XViewMax { get; set; }
        private int XViewMin { get; set; }
        private int YViewMin { get; set; }
        private int YViewMax { get; set; }



        private void SetChart1()
        {

            XViewMax = Convert.ToInt32(chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum);
            XViewMin = Convert.ToInt32(chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum);
            YViewMax = Convert.ToInt32(chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum);
            YViewMin = Convert.ToInt32(chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum);


            int Xva1 = XViewMin + (panel1.Left - 92) * (XViewMax - XViewMin) / 500;
            // label5.Text = "X:" + Xva.ToString() + "\t\n" + "Y:" + chart1.Series[1].Points[Xva].YValues[0];
            p1_x = Xva1;
            try
            {
                p1_Y = Convert.ToInt32(chart1.Series[1].Points[Xva1].YValues[0]);

            }
            catch
            { }
            int Xva2 = XViewMin + (panel2.Left - 92) * (XViewMax - XViewMin) / 500;
            p2_x = Xva2;
            try
            {
                p2_Y = Convert.ToInt32(chart1.Series[1].Points[Xva2].YValues[0]);

            }
            catch
            {


            }
            label5.Text = "时间差:" + Convert.ToString(Math.Abs(p1_x - p2_x)*50)+"ms" + "\r\t" + "电阻差:" + Convert.ToString(Math.Abs(p1_Y - p2_Y))+ "mΩ";
        }
        private void SetChart2()
        {
            XViewMax = Convert.ToInt32(chart2.ChartAreas[0].AxisX.ScaleView.ViewMaximum);
            XViewMin = Convert.ToInt32(chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum);
            YViewMax = Convert.ToInt32(chart2.ChartAreas[0].AxisY.ScaleView.ViewMaximum);
            YViewMin = Convert.ToInt32(chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum);
            int Xva3 = XViewMin + (panel3.Left - 92) * (XViewMax - XViewMin) / 500;
            // label5.Text = "X:" + Xva.ToString() + "\t\n" + "Y:" + chart1.Series[1].Points[Xva].YValues[0];
            p3_x = Xva3;
            try
            {
                p3_Y = Convert.ToInt32(chart2.Series[1].Points[Xva3].YValues[0]);

            }
            catch
            { }
            int Xva4 = XViewMin + (panel4.Left - 92) * (XViewMax - XViewMin) / 500;
            p4_x = Xva4;
            try
            {
                p4_Y = Convert.ToInt32(chart2.Series[1].Points[Xva4].YValues[0]);

            }
            catch
            {


            }
           // label6.Text = "X差值:" + Convert.ToString(Math.Abs(p3_x - p4_x)) + "\r\t" + "Y差值:" + Convert.ToString(Math.Abs(p3_Y - p4_Y));
            label6.Text = "时间差:" + Convert.ToString(Math.Abs(p3_x - p4_x) * 50) + "ms" + "\r\t" + "电阻差:" + Convert.ToString(Math.Abs(p3_Y - p4_Y)) + "mΩ";

        }
        private void SetChart3()
        {
            
            XViewMax = Convert.ToInt32(chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum);
            XViewMin = Convert.ToInt32(chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum);
            YViewMax = Convert.ToInt32(chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum);
            YViewMin = Convert.ToInt32(chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum);
            int Xva5 = XViewMin + (panel5.Left - 92) * (XViewMax - XViewMin) / 500;
            // label5.Text = "X:" + Xva.ToString() + "\t\n" + "Y:" + chart1.Series[1].Points[Xva].YValues[0];
            p5_x = Xva5;
            try
            {
                p5_Y = Convert.ToInt32(chart3.Series[1].Points[Xva5].YValues[0]);

            }
            catch
            { }
            int Xva6 = XViewMin + (panel6.Left - 92) * (XViewMax - XViewMin) / 500;
            p6_x = Xva6;
            try
            {
                p6_Y = Convert.ToInt32(chart3.Series[1].Points[Xva6].YValues[0]);

            }
            catch
            {


            }
          //  label7.Text = "X差值:" + Convert.ToString(Math.Abs(p5_x - p6_x)) + "\r\t" + "Y差值:" + Convert.ToString(Math.Abs(p5_Y - p6_Y));
            label7.Text = "时间差:" + Convert.ToString(Math.Abs(p5_x - p6_x) * 50) + "ms" + "\r\t" + "电阻差:" + Convert.ToString(Math.Abs(p5_Y - p6_Y)) + "mΩ";

        }
        private void Chart1_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            chart2.ChartAreas[0].AxisX.ScaleView = chart1.ChartAreas[0].AxisX.ScaleView;
            chart2.ChartAreas[0].AxisX.Interval = chart1.ChartAreas[0].AxisX.Interval;
            chart2.ChartAreas[0].IsSameFontSizeForAllAxes = chart1.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart2.ChartAreas[0].CursorX = chart1.ChartAreas[0].CursorX;

            chart3.ChartAreas[0].AxisX.ScaleView = chart1.ChartAreas[0].AxisX.ScaleView;
            chart3.ChartAreas[0].AxisX.Interval = chart1.ChartAreas[0].AxisX.Interval;
            chart3.ChartAreas[0].IsSameFontSizeForAllAxes = chart1.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart3.ChartAreas[0].CursorX = chart1.ChartAreas[0].CursorX;



            SetChart1();
            SetChart2();
            SetChart3();
            //int Xva3 = XViewMin + (panel4.Left - 92) * (XViewMax - XViewMin) / 500;
            // label6.Text = "X:" + Xva.ToString() + "\t\n" + "Y:" + chart2.Series[1].Points[Xva].YValues[0];


           

           

        }

        private void Chart2_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView = chart2.ChartAreas[0].AxisX.ScaleView;
            chart1.ChartAreas[0].AxisX.Interval = chart2.ChartAreas[0].AxisX.Interval;
            chart1.ChartAreas[0].IsSameFontSizeForAllAxes = chart2.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart1.ChartAreas[0].CursorX = chart2.ChartAreas[0].CursorX;


            chart3.ChartAreas[0].AxisX.ScaleView = chart2.ChartAreas[0].AxisX.ScaleView;
            chart3.ChartAreas[0].AxisX.Interval = chart2.ChartAreas[0].AxisX.Interval;
            chart3.ChartAreas[0].IsSameFontSizeForAllAxes = chart2.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart3.ChartAreas[0].CursorX = chart2.ChartAreas[0].CursorX;



            SetChart1();
            SetChart2();
            SetChart3();



        }

        private void Chart3_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView = chart3.ChartAreas[0].AxisX.ScaleView;
            chart1.ChartAreas[0].AxisX.Interval = chart3.ChartAreas[0].AxisX.Interval;
            chart1.ChartAreas[0].IsSameFontSizeForAllAxes = chart3.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart1.ChartAreas[0].CursorX = chart3.ChartAreas[0].CursorX;


            chart2.ChartAreas[0].AxisX.ScaleView = chart3.ChartAreas[0].AxisX.ScaleView;
            chart2.ChartAreas[0].AxisX.Interval = chart3.ChartAreas[0].AxisX.Interval;
            chart2.ChartAreas[0].IsSameFontSizeForAllAxes = chart3.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart2.ChartAreas[0].CursorX = chart3.ChartAreas[0].CursorX;
         

            SetChart3();
            SetChart2();
            SetChart1();

        }

        private void Chart1_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            chart2.ChartAreas[0].AxisX.ScaleView = chart1.ChartAreas[0].AxisX.ScaleView;
            chart2.ChartAreas[0].AxisX.Interval = chart1.ChartAreas[0].AxisX.Interval;
            chart2.ChartAreas[0].IsSameFontSizeForAllAxes = chart1.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart2.ChartAreas[0].CursorX = chart1.ChartAreas[0].CursorX;


            chart3.ChartAreas[0].AxisX.ScaleView = chart1.ChartAreas[0].AxisX.ScaleView;
            chart3.ChartAreas[0].AxisX.Interval = chart1.ChartAreas[0].AxisX.Interval;
            chart3.ChartAreas[0].IsSameFontSizeForAllAxes = chart1.ChartAreas[0].IsSameFontSizeForAllAxes;
            chart3.ChartAreas[0].CursorX = chart1.ChartAreas[0].CursorX;


            SetChart3();
            SetChart2();
            SetChart1();

        }

        private void Chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void Chart1_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult myTestResult = chart1.HitTest(e.X, e.Y);

            if (myTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                //  this.Cursor = Cursors.Cross;
                int i = myTestResult.PointIndex;
                DataPoint dp = myTestResult.Series.Points[i];

                double doubleXValue = (dp.XValue);
                double doubleYValue = dp.YValues[0];

                // panel1.Left=
                // MessageBox.Show(doubleXValue.ToString());
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }



        private void Panel2_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void Chart2_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {

            SetChart3();
            SetChart2();
            SetChart1();
        }

        private void Chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {


            SetChart3();
            SetChart2();
            SetChart1();
        }

        private void Chart1_SelectionRangeChanged(object sender, CursorEventArgs e)
        {


            SetChart3();
            SetChart2();
            SetChart1();
        }

        private void Chart1_SelectionRangeChanging(object sender, CursorEventArgs e)
        {


            SetChart3();
            SetChart2();
            SetChart1();
        }

        private void Chart2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Chart2_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();

        }

        private void Chart2_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Chart2_AxisViewChanged(object sender, ViewEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Chart3_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Chart3_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Chart3_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Chart3_AxisViewChanged(object sender, ViewEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Panel3_MouseUp(object sender, MouseEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Panel4_MouseUp(object sender, MouseEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Panel5_MouseUp(object sender, MouseEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Panel6_MouseUp(object sender, MouseEventArgs e)
        {
            SetChart1();
            SetChart2();
            SetChart3();
        }

        private void Panel5_MouseDown(object sender, MouseEventArgs e)
        {

            //md  
            ax5 = e.X;
            //
        }

        private void Panel6_MouseDown(object sender, MouseEventArgs e)
        {

            //md 
            ax6 = e.X;
            //
        }

        private void Panel6_MouseMove(object sender, MouseEventArgs e)
        {
            panel6.BringToFront();


            if (e.Button == MouseButtons.Left)
            {
                panel6.Left = panel6.Left + (e.X - ax6);
            }
            //md  aX1 = e.X;
            SetChart1();
            SetChart2();
            SetChart3();

            //
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            aX = e.X;

        }
    }
}
