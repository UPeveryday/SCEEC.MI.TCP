using SCEEC.MI.TZ3310;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;
using System.Collections.Generic;

namespace SCEEC.TTM
{
    /// <summary>
    /// ExportDataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExportDataWindow : Window
    {
        private readonly string ResultName;
        public ExportDataWindow(string s)
        {
            InitializeComponent();
            ResultName = s;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinimumButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximumButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum.png", UriKind.Relative));
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum2.png", UriKind.Relative));
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
           // SaveAllWaveForm();
           // closeWithConfirm();
            this.Close();
        }
        void closeWithConfirm()
        {
           // InsertDataTodatabase.UpdataDatabase(ResultName);
            try { InsertDataTodatabase.UpdataDatabase(ResultName); } catch { }
            InsertDataTodatabase.ShowExport(ResultName);

        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        
        private void DataViewGrid()
        {
            //WorkingSets.local.CreateLocalDatabase();
            //DataTable DcInsulution = WorkingSets.local.LocalSQLClient.getDataTable("tz3310", "testresult");
            ////DataSet ds = WorkingSets.local.LocalSQLClient.getDataSetView("tz3310", "casingtest_commonbody");
            ////DataView dv = new DataView(ds.Tables["casingtest_commonbody"]);
            //DataView dv = new DataView(DcInsulution);
            //DataGrid_DataBase.ItemsSource = dv;
            //try {

            //    ResultListBox.ItemsSource = TestingWorkerUtility.getFinalResultsText(WorkingSets.local.status);
            //}
            //catch { }

            var tws = WorkingSets.local.getTestResults(ResultName);
            List<string> resultList = new List<string>();
            foreach (var mi in tws.MeasurementItems)
            {
                resultList.Add(mi.ResultText);
            }
            ResultListBox.ItemsSource = resultList.ToArray();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataViewGrid();
        }

        

        public void SaveAllWaveForm()
        {
            var tws = WorkingSets.local.getTestResults(ResultName);
            int ms = tws.MeasurementItems.Length;
            for (int i = 0; i < ms; i++)
            {
                short[] Waved = tws.MeasurementItems[i].Result.waves;
                if (Waved != null)
                {
                    int[] deelwave1 = new int[6000];
                    int[] deelwave2 = new int[6000];
                    int[] deelwave3 = new int[6000];
                    for (int j = 0; j < Waved.Length; j++)
                    {
                        if (j >= 0 && j < 6000)
                            deelwave1[j] = Convert.ToInt32(Waved[j]);
                        if (j >= 6002 && j < 12002)
                            deelwave2[j-6002] = Convert.ToInt32(Waved[j]);
                        if (j >= 12004 && j < 18004)
                            deelwave3[j-12004] = Convert.ToInt32(Waved[j]);

                    }
                    DrawWave(deelwave1, "第" + i.ToString() + "分接位置A相");
                    DrawWave(deelwave2, "第" + i.ToString() + "分接位置B相");
                    DrawWave(deelwave3, "第" + i.ToString() + "分接位置C相");
                }

            }

        }
        private void DrawWave(int[] waveform, string NumPic)
        {
            //获取本地图片文件（单纯的调用uri会存在后续代码有文件被占用的隐患。在此做了间接转换，方便释放资源）
            //BitmapImage lb = new BitmapImage();
            //lb = new BitmapImage(new Uri("D:\\123.jpg", UriKind.Absolute));
            BinaryReader binReader = new BinaryReader(File.Open(@"C:\Users\上海思创TZ3310变压器综合试验系统\source\repos\生产本地波形图片\生产本地波形图片\Pictures\白.jpg", FileMode.Open));
            FileInfo fileInfo = new FileInfo(@"C:\Users\上海思创TZ3310变压器综合试验系统\source\repos\生产本地波形图片\生产本地波形图片\Pictures\白.jpg");
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(bytes);
            bitmap.EndInit();

            Image Toimage = new Image();
            Toimage.Source = bitmap.Clone();

            #region 创建虚拟画布
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            //创建矩形区域
            ImageBrush imgbrush = new ImageBrush();
            imgbrush.ImageSource = Toimage.Source;
            Rect rect = new Rect(new Point(0, 0), new Size(600, 1200));
            drawingContext.DrawRectangle(imgbrush, (Pen)null, rect);
            //创建画笔
            Pen pen = new Pen();
            pen.Brush = new SolidColorBrush(Color.FromRgb(1, 50, 0));
            pen.Thickness = 2;
            //写入指定位置文本
            for (int i = 0; i < 13; i++)
            {
                FormattedText formattedText = new FormattedText(((i * 1000) - 6000).ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 10, Brushes.Black);
                drawingContext.DrawText(formattedText, new Point(10, 100 * i));
            }
            for (int i = 0; i < 13; i++)
            {
                FormattedText formattedText = new FormattedText((i * 1000).ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 10, Brushes.Black);
                drawingContext.DrawText(formattedText, new Point(100 * i, 580));
            }
            //画坐标
            drawingContext.DrawLine(pen, new Point(0, 600), new Point(600, 600));
            drawingContext.DrawLine(pen, new Point(0, 0), new Point(0, 1200));

            int[] WaveFormData = new int[6000];//数据除以十
            for (int i = 0; i < 6000; i++)
            {
                if (i != WaveFormData.Length - 1)
                    drawingContext.DrawLine(pen, new Point(i, 600 + WaveFormData[i]), new Point(i + 1, WaveFormData[i + 1] + 600));
            }

            drawingContext.Close();
            #endregion
            //截虚拟画布并生成为本地图片文件
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)600, (int)1200, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            string file = @"D:\" + NumPic + ".jpg";
            string Extension = System.IO.Path.GetExtension(file).ToLower();
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (Stream stm = File.Create(file))
            {
                encoder.Save(stm);
            }
        }
    }
}
