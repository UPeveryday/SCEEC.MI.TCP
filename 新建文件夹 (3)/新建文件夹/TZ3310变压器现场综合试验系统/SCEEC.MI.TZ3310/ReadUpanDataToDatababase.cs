using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public struct DcresistaceUpandata
    {
        public string Data;
        public string Time;
        public string Position;
        public string Windingconfige;
        public string Av;
        public string Bv;
        public string Cv;
        public string Ar;
        public string Br;
        public string Cr;
        public string Ai;
        public string Bi;
        public string Ci;
        public string Error;

    }
    public struct DcInlutionUpandata
    {
        public string Data;
        public string Time;
        public string Position;
        public string PreResistance;
        public string JyxxM;
        public string AbsM;
        public string Volate;
        public string Resistance;
        public string _0_15;
        public string _1_00;
        public string Abs;
        public string Jhzs;
        public string Error;
    }

    public struct CaptanceUpandata
    {
        public string Data;
        public string Time;
        public string Position;
        public string TestKind;
        public string Volate;
        public string Fre;
        public string Cx;
        public string Tan;
        public string Error;
    }
    public struct OltcUpandata
    {
        public string Data;
        public string Time;
        public string Windkind;
        public string windposition;
        public string Current;
        public string Protectresistance;
        public string Starttime;
        public string Awaveform;
        public string Bwaveform;
        public string Cwaveform;
        public string Dwaveform;
        public string Error;

    }
    public struct TestStruct
    {
        public DcresistaceUpandata[] res;
        public DcInlutionUpandata[] dci;
        public CaptanceUpandata[] cap;
        public OltcUpandata[] Oltc;
    }

    public static class ReadUpanDataToDatababase
    {

        private static DcresistaceUpandata[] ReadDcresisitance(string DcPath)
        {
            try
            {
                string[] filenum = Directory.GetFiles(DcPath);
                DcresistaceUpandata[] DcresData = new DcresistaceUpandata[filenum.Length];
                for (int i = 1; i <= filenum.Length; i++)
                {
                    if (File.Exists(DcPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini"))
                    {
                        INIFiLE myini = new INIFiLE(DcPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini");
                        DcresData[i - 1].Data = myini.ReadString("result", "Date", "");
                        DcresData[i - 1].Time = myini.ReadString("result", "Time", "");
                        DcresData[i - 1].Position = myini.ReadString("result", "试验位置", "");
                        DcresData[i - 1].Windingconfige = myini.ReadString("result", "绕组类型", "");
                        DcresData[i - 1].Av = myini.ReadString("result", "VA", "");
                        DcresData[i - 1].Ai = myini.ReadString("result", "IA", "");
                        DcresData[i - 1].Ar = myini.ReadString("result", "RA", "");
                        DcresData[i - 1].Bv = myini.ReadString("result", "VB", "");
                        DcresData[i - 1].Bi = myini.ReadString("result", "IB", "");
                        DcresData[i - 1].Br = myini.ReadString("result", "RB", "");
                        DcresData[i - 1].Cv = myini.ReadString("result", "VC", "");
                        DcresData[i - 1].Ci = myini.ReadString("result", "IC", "");
                        DcresData[i - 1].Cr = myini.ReadString("result", "RC", "");
                        DcresData[i - 1].Error = myini.ReadString("result", "Error", "");
                    }

                }
                return DcresData;
            }
            catch
            {
                throw new Exception("直流电阻地址不存在");
            }

        }
        private static DcInlutionUpandata[] ReadDcinlution(string DciPath)
        {
            try
            {
                string[] filenum = Directory.GetFiles(DciPath);
                DcInlutionUpandata[] DciData = new DcInlutionUpandata[filenum.Length];
                for (int i = 1; i <= filenum.Length; i++)
                {
                    if (File.Exists(DciPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini"))
                    {
                        INIFiLE myini = new INIFiLE(DciPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini");
                        DciData[i - 1].Data = myini.ReadString("result", "Date", "");
                        DciData[i - 1].Time = myini.ReadString("result", "Time", "");
                        DciData[i - 1].Position = myini.ReadString("result", "试验位置", "");
                        DciData[i - 1].PreResistance = myini.ReadString("result", "保护阻值", "");
                        DciData[i - 1].JyxxM = myini.ReadString("result", "绝缘下限", "");
                        DciData[i - 1].AbsM = myini.ReadString("result", "吸收比下限", "");
                        DciData[i - 1].Volate = myini.ReadString("result", "Vo", "");
                        DciData[i - 1].Resistance = myini.ReadString("result", "R", "");
                        DciData[i - 1]._0_15 = myini.ReadString("result", "0:15", "");
                        DciData[i - 1]._1_00 = myini.ReadString("result", "1:00", "");
                        DciData[i - 1].Abs = myini.ReadString("result", "吸收比", "");
                        DciData[i - 1].Jhzs = myini.ReadString("result", "极化指数", "");
                        DciData[i - 1].Error = myini.ReadString("result", "Error", "");
                    }
                }
                return DciData;

            }
            catch { throw new Exception("绝缘电阻地址错误"); }

        }
        private static CaptanceUpandata[] ReadCaptance(string DciPath)
        {
            try
            {
                string[] filenum = Directory.GetFiles(DciPath);
                CaptanceUpandata[] DciData = new CaptanceUpandata[filenum.Length];
                for (int i = 1; i <= filenum.Length; i++)
                {
                    if (File.Exists(DciPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini"))
                    {
                        INIFiLE myini = new INIFiLE(DciPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini");
                        DciData[i - 1].Data = myini.ReadString("result", "Date", "");
                        DciData[i - 1].Time = myini.ReadString("result", "Time", "");
                        DciData[i - 1].Position = myini.ReadString("result", "试验位置", "");
                        DciData[i - 1].TestKind = myini.ReadString("result", "试验模式", "");
                        DciData[i - 1].Volate = myini.ReadString("result", "Vo", "");
                        DciData[i - 1].Fre = myini.ReadString("result", "Fr", "");
                        DciData[i - 1].Cx = myini.ReadString("result", "Cx", "");
                        DciData[i - 1].Tan = myini.ReadString("result", "tan", "");
                        DciData[i - 1].Error = myini.ReadString("result", "Error", "");
                    }
                }
                return DciData;
            }
            catch { throw new Exception("介质损耗地址错误"); }


        }
        private static OltcUpandata[] ReadOltc(string DciPath)
        {
            try
            {
                string[] filenum = Directory.GetFiles(DciPath);
                OltcUpandata[] DciData = new OltcUpandata[filenum.Length];
                for (int i = 1; i <= filenum.Length; i++)
                {
                    if (File.Exists(DciPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini"))
                    {
                        INIFiLE myini = new INIFiLE(DciPath + "\\" + NumCorrection.KeepNum(i.ToString(), 2, HeadOrTail.Head) + ".ini");
                        DciData[i - 1].Data = myini.ReadString("result", "Date", "");
                        DciData[i - 1].Time = myini.ReadString("result", "Time", "");
                        DciData[i - 1].Windkind = myini.ReadString("result", "绕组类型", "");
                        DciData[i - 1].windposition = myini.ReadString("result", "分接位置", "");
                        DciData[i - 1].Current = myini.ReadString("result", "试验电流", "");
                        DciData[i - 1].Protectresistance = myini.ReadString("result", "触发电阻", "");
                        DciData[i - 1].Starttime = myini.ReadString("result", "触发时间", "");

                        for (int j = 1; j < 25; j++)
                        {
                            DciData[i - 1].Awaveform += myini.ReadString("result", "A" +
                                NumCorrection.KeepNum(j.ToString(), 2, HeadOrTail.Head), "");
                        }
                        DciData[i - 1].Awaveform += myini.ReadString("result", "AED", "");
                        for (int j = 1; j < 25; j++)
                        {
                            DciData[i - 1].Bwaveform += myini.ReadString("result", "B" +
                                NumCorrection.KeepNum(j.ToString(), 2, HeadOrTail.Head), "");
                        }
                        DciData[i - 1].Bwaveform += myini.ReadString("result", "BED", "");
                        for (int j = 1; j < 25; j++)
                        {
                            DciData[i - 1].Cwaveform += myini.ReadString("result", "C" +
                                NumCorrection.KeepNum(j.ToString(), 2, HeadOrTail.Head), "");
                        }
                        DciData[i - 1].Cwaveform += myini.ReadString("result", "CED", "");
                        for (int j = 1; j < 25; j++)
                        {
                            DciData[i - 1].Dwaveform += myini.ReadString("result", "D" +
                                NumCorrection.KeepNum(j.ToString(), 2, HeadOrTail.Head), "");
                        }
                        DciData[i - 1].Dwaveform += myini.ReadString("result", "DED", "");
                        DciData[i - 1].Error = myini.ReadString("result", "Error", "");
                    }
                }
                return DciData;
            }
            catch { throw new Exception("有载分接地址错误"); }



        }

        /// <summary>
        /// 获取Upan测试结果，不测填null
        /// </summary>
        /// <param name="res">直流电阻ini文件地址</param>
        /// <param name="dci">绝缘电阻ini文件地址</param>
        /// <param name="cap">介质损耗ini文件地址</param>
        /// <param name="oltc">有载分接ini文件地址</param>
        /// <returns> upan测试下单的数据</returns>
        public static TestStruct RetuenTestForUpanData(string res, string dci, string cap, string oltc)
        {
            TestStruct testStruct = new TestStruct();
            if (!File.Exists(res))
                WriteDataToFile.DeelDirectoryInfo(res, Mode.Create);
            if (!File.Exists(dci))
                WriteDataToFile.DeelDirectoryInfo(dci, Mode.Create);
            if (!File.Exists(cap))
                WriteDataToFile.DeelDirectoryInfo(cap, Mode.Create);
            if (!File.Exists(oltc))
                WriteDataToFile.DeelDirectoryInfo(oltc, Mode.Create);
            if (res != null)
                testStruct.res = ReadDcresisitance(res);
            if (dci != null)
                testStruct.dci = ReadDcinlution(dci);
            if (cap != null)
                testStruct.cap = ReadCaptance(cap);
            if (oltc != null)
                testStruct.Oltc = ReadOltc(oltc);
            return testStruct;
        }

    }


}
