using SCEEC.MI.High_Precision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.NET.TCPSERVER
{
    public class TestMesseages
    {
        private double TestCn { get; set; }
        private double TestCnTan { get; set; }
        private bool IsRunning { get; set; }
        private bool IsSendData { get; set; }
        public ViewSources ViewData { get; set; }
        public bool ISStart = false;
        public delegate void SendTestResult(AsyncTCPServer tCPServer, byte[] testresult);
        public event SendTestResult SendData;

        private AsyncTCPServer tCPServer;
        public byte[] data { get; set; }

        public TestMesseages(AsyncTCPServer tCPServer, byte[] data)
        {
            this.tCPServer = tCPServer;
            this.data = data;
        }
        public void ReturnMessages()
        {
            if (data != null)
            {
                switch (data[0])
                {
                    case 0x02:
                        TestClass.SerchSend(tCPServer, data);
                        break;
                    case 0xcc:
                        if (data[1] == 0x90)
                            TestClass.Connec(tCPServer, data);
                        else
                        {
                            if (0x07 != TestResult.WorkTest.ChangeVolate(AnalysisData.DeelVolate(data)))
                                TestClass.SetPar(tCPServer, data, true);
                        }
                        break;
                    case 0xbc:
                        TestClass.DisConnec(tCPServer, data);
                        break;
                    case 0x41:
                        TestResult.WorkTest.ChangeFre(AnalysisData.DeelFre(data));
                        TestClass.SetPar(tCPServer, data, true);
                        break;
                    case 0x42:
                        TestResult.WorkTest.ChangeVolate(AnalysisData.DeelVolate(data));//不确定是否是ASkll码
                        TestClass.SetPar(tCPServer, data, true);
                        break;
                    case 0xac:
                        // TestResult.WorkTest.ChangeFre(1f);//无启动电源选项
                        TestClass.SetPar(tCPServer, data, true);
                        break;
                    case 0xed:
                        bool Istrue = false;
                        if (0x0a == TestResult.WorkTest.startDownVolate())
                            Istrue = false;
                        else
                            Istrue = true;
                        TestClass.SetPar(tCPServer, data, Istrue);
                        break;
                    case 0x32:
                        TestResult.WorkTest.ChangeTestCn((float)Convert.ToDouble(AnalysisData.DeelCn(data)[0]));
                        TestResult.WorkTest.ChangeTestCn((float)Convert.ToDouble(AnalysisData.DeelCn(data)[1]));//CnTan协议无法测量
                        TestClass.SetPar(tCPServer, data, true);
                        break;
                    case 0x3a:
                        if (0x04 != TestResult.WorkTest.StartTest())//启动测量
                        {
                            IsRunning = true;
                            TestResult.WorkTest.OutTestResult += WorkTest_OutTestResult1;
                            ISStart = true;
                        }
                        else
                        {
                            IsRunning = false;
                            ISStart = false;
                        }
                        TestClass.SetPar(tCPServer, data, ISStart);
                        break;
                    case 0xda:
                        // AnalysisData.DeelFreAndVolate(data);//发送需要的和中数据
                        TestClass.QueryFreAndVolate(tCPServer, data, new byte[36]);//电压频率,问题，高压侧电压低压侧电压
                        break;
                    case 0xff:
                        if (IsRunning)
                            TestClass.QueryTestState(tCPServer, data, new byte[] { 0xac, 0xac });
                        else
                            TestClass.QueryTestState(tCPServer, data, new byte[] { 0xee, 0xee });
                        break;
                    case 0xfd:
                        // TestClass.QueryTestResult(tCPServer, data, AnalysisData.DeelTestResult(TestResultData));//Test
                        ISStart = true;
                        SendData += TestMesseages_SendData;
                        break;
                    case 0xbd:
                        //反接板状态，查询协议确实
                        TestClass.QueryDisStata(tCPServer, data, true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void TestMesseages_SendData(AsyncTCPServer tCPServer, byte[] testresult)
        {
            TestClass.QueryTestResult(tCPServer, testresult, AnalysisData.DeelTestResult(testresult));

        }

        private void WorkTest_OutTestResult1(byte[] result)
        {
            if (ISStart)
            {
                SendData(tCPServer, result);
                ISStart = false;
            }
            // ViewSources vs = new ViewSources(result);
        }

        ~TestMesseages()
        {
            GC.Collect();
           // tCPServer.Dispose();
        }
    }

}
