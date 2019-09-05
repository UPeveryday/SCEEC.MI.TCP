using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCEEC.MI.High_Precision;
namespace SCEEC.NET.TCPSERVER
{
    public static class AnalysisData
    {
        public static float DeelFre(byte[] data)
        {
            try
            {
                if (data != null && data.Length == 5)
                {
                    return (float)Convert.ToDouble(Encoding.ASCII.GetString(new byte[2] { data[1], data[2] }));
                }
            }
            catch
            {
                return 50;
            }
            return 50;
        }

        public static float DeelVolate(byte[] data)
        {
            if (data != null && data.Length == 7)
            {
                byte[] volate = data.Skip(1).Take(4).ToArray();
                return BitConverter.ToSingle(volate, 0);
            }
            return -1;
        }


        public static string[] DeelCn(byte[] data)
        {
            if (data != null && data.Length == 15)
            {
                byte[] Cn = data.Skip(1).Take(3).ToArray();
                byte[] CnTan = data.Skip(4).Take(3).ToArray();
                byte[] Channel = data.Skip(7).Take(6).ToArray();
                return new string[] {Encoding.ASCII.GetString(Cn),
                    Encoding.ASCII.GetString(CnTan),Channel[0].ToString(),Channel[1].ToString(),
                Channel[2].ToString(),Channel[3].ToString(),Channel[4].ToString(),Channel[5].ToString()};//格式待转换
            }
            return null;

        }

        public static string[] DeelFreAndVolate(byte[] data)
        {
            string HighVolate = Encoding.ASCII.GetString(data.Skip(1).Take(7).ToArray());
            string Fre = Encoding.ASCII.GetString(data.Skip(8).Take(7).ToArray());
            string HighCurrent = Encoding.ASCII.GetString(data.Skip(15).Take(7).ToArray());
            string LowVolate = Encoding.ASCII.GetString(data.Skip(22).Take(7).ToArray());
            string LowCurrent = Encoding.ASCII.GetString(data.Skip(29).Take(7).ToArray());
            return new string[] { HighVolate, Fre, HighCurrent, LowVolate, LowCurrent };
        }

        public static string[] TestResult(byte[] data)
        {
            string Volate = Encoding.ASCII.GetString(data.Skip(1).Take(7).ToArray());
            string Fre = Encoding.ASCII.GetString(data.Skip(8).Take(7).ToArray());
            string Cx1 = Encoding.ASCII.GetString(data.Skip(15).Take(7).ToArray());
            string CxTan1 = Encoding.ASCII.GetString(data.Skip(22).Take(7).ToArray());
            string Cx2 = Encoding.ASCII.GetString(data.Skip(29).Take(7).ToArray());
            string CxTan2 = Encoding.ASCII.GetString(data.Skip(36).Take(7).ToArray());
            string Cx3 = Encoding.ASCII.GetString(data.Skip(43).Take(7).ToArray());
            string CxTan3 = Encoding.ASCII.GetString(data.Skip(50).Take(7).ToArray());
            string Cx4 = Encoding.ASCII.GetString(data.Skip(57).Take(7).ToArray());
            string CxTan4 = Encoding.ASCII.GetString(data.Skip(64).Take(7).ToArray());
            string ReadyCx1 = Encoding.ASCII.GetString(data.Skip(71).Take(7).ToArray());
            string ReadyCxTan1 = Encoding.ASCII.GetString(data.Skip(78).Take(7).ToArray());
            string ReadyCx2 = Encoding.ASCII.GetString(data.Skip(85).Take(7).ToArray());
            string ReadyCxTan2 = Encoding.ASCII.GetString(data.Skip(92).Take(7).ToArray());
            return new string[] { Volate, Fre, Cx1, CxTan1, Cx2, CxTan2, Cx3, CxTan3, Cx4, CxTan4, ReadyCx1, ReadyCxTan1, ReadyCx2, ReadyCxTan2 };
        }

        public static byte[] OnlyData(float Num, string unit, int needLength = 7)
        {
            string[] tempdata = Num.ToString().Split('.');
            int MainNumLength = tempdata[0].Length;
            string TempUnit = unit;
            if (unit.Length <= 2 && unit.Length >= 0)
            {
                if (unit.Length == 2)
                    TempUnit = unit;
                else if (unit.Length == 1)
                    TempUnit = " " + unit;
                else
                    TempUnit = " " + " ";
            }
            else
            {
                return new byte[needLength];
            }
            if (MainNumLength < needLength - 2)
            {
                string FomatString = "N" + (needLength - MainNumLength - 3).ToString();
                return Encoding.Default.GetBytes(Num.ToString(FomatString) + TempUnit);
            }
            else
                return new byte[needLength];
        }

        public static byte[] DeelTestResult(byte[] data)
        {
            //Encoding.Default.GetBytes(aaa.ToString("N3"));
            ViewSources vs = new ViewSources(data);
            byte[] Volate = OnlyData(vs.TestVoalte, "KV");
            byte[] Fre = OnlyData(vs.TestFre, "Hz");
            byte[] Cx1 = OnlyData((float)vs.TestCx1, ""); 
            byte[] CxTan1 = OnlyData((float)vs.TestTan1, "");
            byte[] Cx2 = OnlyData((float)vs.TestCx2, "");
            byte[] CxTan2 = OnlyData((float)vs.TestTan2, "");
            byte[] Cx3 = OnlyData((float)vs.TestCx3, "");
            byte[] CxTa3 = OnlyData((float)vs.TestTan3, "");
            byte[] Cx4 = OnlyData((float)vs.TestCx4, "");
            byte[] CxTan4 = OnlyData((float)vs.TestTan4, "");
            byte[] zero = new byte[7];
            byte[] returnbyte = new byte[99];
            returnbyte[0] = 0xfd;
            Volate.CopyTo(returnbyte, 1);
            Fre.CopyTo(returnbyte, 8);
            Cx1.CopyTo(returnbyte, 15);
            CxTan1.CopyTo(returnbyte, 22);
            Cx2.CopyTo(returnbyte, 29);
            CxTan2.CopyTo(returnbyte, 36);
            Cx3.CopyTo(returnbyte, 43);
            CxTa3.CopyTo(returnbyte, 50);
            Cx4.CopyTo(returnbyte, 57);
            CxTan4.CopyTo(returnbyte, 64);
            zero.CopyTo(returnbyte, 71);
            zero.CopyTo(returnbyte, 78);
            zero.CopyTo(returnbyte, 85);
            zero.CopyTo(returnbyte, 92);

            return returnbyte;
        }

        public static byte[] DeelVolateAndFre(byte[] data)
        {
            //Encoding.Default.GetBytes(aaa.ToString("N3"));
            ViewSources vs = new ViewSources(data);
            byte[] Volate = OnlyData(vs.TestVoalte, "KV");
            byte[] Fre = OnlyData(vs.TestFre, "Hz");
            byte[] Cx1 = OnlyData((float)vs.TestCx1, "");
            byte[] CxTan1 = OnlyData((float)vs.TestTan1, "");
            byte[] Cx2 = OnlyData((float)vs.TestCx2, "");
            byte[] CxTan2 = OnlyData((float)vs.TestTan2, "");
            byte[] Cx3 = OnlyData((float)vs.TestCx3, "");
            byte[] CxTa3 = OnlyData((float)vs.TestTan3, "");
            byte[] Cx4 = OnlyData((float)vs.TestCx4, "");
            byte[] CxTan4 = OnlyData((float)vs.TestTan4, "");
            byte[] zero = new byte[7];
            byte[] returnbyte = new byte[99];
            returnbyte[0] = 0xfd;
            Volate.CopyTo(returnbyte, 1);
            Fre.CopyTo(returnbyte, 8);
            Cx1.CopyTo(returnbyte, 15);
            CxTan1.CopyTo(returnbyte, 22);
            Cx2.CopyTo(returnbyte, 29);
            CxTan2.CopyTo(returnbyte, 36);
            Cx3.CopyTo(returnbyte, 43);
            CxTa3.CopyTo(returnbyte, 50);
            Cx4.CopyTo(returnbyte, 57);
            CxTan4.CopyTo(returnbyte, 64);
            zero.CopyTo(returnbyte, 71);
            zero.CopyTo(returnbyte, 78);
            zero.CopyTo(returnbyte, 85);
            zero.CopyTo(returnbyte, 92);

            return returnbyte;
        }


    }
}

