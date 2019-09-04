using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using SCEEC.MI.TZ3310;

namespace WpfApp3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void UpdataJob(string taskinformationpath, ref JobList jobInfo)
        {
            INIFiLE iNI = new INIFiLE(taskinformationpath);
            jobInfo.Information.testingTime = DateTime.Now;
            jobInfo.Information.testingName = iNI.ReadString("information", "试验名称", "");
            jobInfo.Information.tester = iNI.ReadString("information", "试验人员", "");
            jobInfo.Information.testingAgency = iNI.ReadString("information", "试验单位", "");
            jobInfo.Information.auditor = iNI.ReadString("information", "审核人", "");
            jobInfo.Information.approver = iNI.ReadString("information", "批准人", "");
            jobInfo.Information.principal = iNI.ReadString("information", "负责人", "");
            jobInfo.Information.weather = iNI.ReadString("information", "天气", "");
            jobInfo.Information.temperature = iNI.ReadString("information", "温度", "");
            jobInfo.Information.humidity = iNI.ReadString("information", "湿度", "");
            jobInfo.Information.oilTemperature = iNI.ReadInt("information", "变压器油温(整数)", 0);
        }

        public DataRow[] Testresultrows()
        {
            if (!WorkingSets.local.saveTestResults())
            {
                string err = WorkingSets.local.LocalSQLClient.ErrorText;
                err = err;
            }
            return WorkingSets.local.TestResults.Select("testid = '" + "-194100477" + "'");
        }
        public void ScanDirInfo()
        {

            String[] drives = Environment.GetLogicalDrives();
            if (File.Exists(drives[drives.Length - 1] + "List.lis"))
            {
                string filecontent = WriteDataToFile.ReadFile(drives[drives.Length - 1] + "List.lis");
                filecontent = filecontent.Remove(filecontent.Length - 1);
                string[] ct = filecontent.Split(';');
                char[] f1 = ct[0].ToArray();
                string[] TaskNames = filecontent.Remove(0, 2).Split(';');//任务名称集合
                int num = Convert.ToInt32(f1[0].ToString() + f1[1].ToString());//预定任务数量
                if (num != ct.Length)
                {
                    //错误
                }
                WorkingSets.local.refreshTestResults();
                foreach (var Tn in TaskNames)
                {
                    string taskPath = drives[drives.Length - 1] + "\\" + Tn;
                    //  int hashcode = UseUpanDoWork.LocalUsb.ReadJob(taskPath + "Jobtask.dat").Information.GetHashCode();
                    DataRow[] data = Testresultrows();
                    foreach (var outi in data)
                    {
                    }

                    Insertdatabyupan inser = new Insertdatabyupan(taskPath);
                    inser.InsertUpandatatodatabase();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScanDirInfo();
        }


        public void LocalJob(JobList job, string NeedSavePath)
        {
            FileStream fs = new FileStream(@"D:\Jobserial.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, job);
            fs.Close();
        }

        public JobList ReadJob(string jobPath)
        {
            // FileStream fs = new FileStream(@"D:\Jobserial.dat", FileMode.Create);
            FileStream fs = new FileStream(jobPath, FileMode.Open);
            BinaryFormatter bf1 = new BinaryFormatter();
            return (JobList)bf1.Deserialize(fs);// as JobList;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //FileStream fs = new FileStream(@"D:\serializePeople.dat", FileMode.Create);
            //JobList job = new JobList();
            //job.Bushing.Capacitance = true;
            //job.id = 99;
            //LocalJob(job, null);
            //Insertdatabyupan inser = new Insertdatabyupan(@"D:\Ta2211457\ZLDZ", @"D:\Ta2211457\JYDZ", @"D:\Ta2211457\JZSH",
            //    @"D:\Ta2211457\YZFJ", @"D:\Ta2211457", @"D:\Ta2211457\Listmitask.dat");
            //inser.InsertUpandatatodatabase();
            // INIFiLE iNI = new INIFiLE(@"D:\Ta2211354\Information.ini");
            //jobInfo.Information.testingTime = DateTime.Now;
            //jobInfo.Information.testingName = iNI.ReadString("information", "试验名称", "");
            //jobInfo.Information.tester = iNI.ReadString("information", "试验人员", "");
            //jobInfo.Information.testingAgency = iNI.ReadString("information", "试验单位", "");
            //jobInfo.Information.auditor = iNI.ReadString("information", "审核人", "");
            //jobInfo.Information.approver = iNI.ReadString("information", "批准人", "");
            //jobInfo.Information.principal = iNI.ReadString("information", "负责人", "");
            //jobInfo.Information.weather = iNI.ReadString("information", "天气", "");
            //jobInfo.Information.temperature = iNI.ReadString("information", "温度", "");
            //jobInfo.Information.humidity = iNI.ReadString("information", "湿度", "");
            // int a = iNI.ReadInt("information", "变压器油温(整数)", 0);
            //BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(fs, job);
            //fs.Close();
            // t1.Text = a.ToString();
            //FileStream fs = new FileStream(@"D:\Ta2211551\Jobtask.dat", FileMode.Open);
            //BinaryFormatter bf1 = new BinaryFormatter();
            //JobList job1 = (JobList)bf1.Deserialize(fs);// as JobList;

            //t1.Text = (job1.id).ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<string> tp = new List<string>();
            string[] names = { "ta1", "ta2", "ta1", "ta2", "ta3" ,"ta4"};
            for(int j=0;j<names.Length;j++)
                for(int i=j+1;i<names.Length-j-1;i++)
                {
                   
                    if(names[j]==names[i])
                    {
                        tp.Add(names[i]);
                    }
                }

            List<string> list = new List<string>();
            for (int i = 0; i < names.Length-1; i++)//遍历数组成员
            {
                if (list.IndexOf(names[i].ToLower()) == -1)//对每个成员做一次新数组查询如果没有相等的则加到新数组
                    list.Add(names[i]);

            }

           // return list.ToArray();


        }
    }
}
