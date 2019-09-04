using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public struct RecordNum
    {
        public int DcReNum;
        public int DcInNUm;
        public int CaPaNum;
        public int OltcNum;
        public bool DcReNumEnable;
        public bool DcInNUmEnable;
        public bool CaPaNumEnable;
        public bool OltcNumEnable;
    }

    public class ResultOfUPan
    {
        internal List<string> DcResistance;
        internal List<string> DCInsulation;
        internal List<string> Capacitance;
        internal List<string> OLTc;
        public ResultOfUPan(List<string> RES, List<string> DCI, List<string> CAP, List<string> OL)
        {
            this.DCInsulation = DCI;
            this.DcResistance = RES;
            this.Capacitance = CAP;
            this.OLTc = OL;
        }

        public List<string> dci
        {
            get { return DCInsulation; }
        }
        public List<string> res
        {
            get { return DcResistance; }
        }
        public List<string> cap
        {
            get { return Capacitance; }
        }
        public List<string> oltc
        {
            get { return OLTc; }
        }

    }
    public class UPanList
    {
        public bool IsUpan = false;

        // public static JobList Job;
        public static RecordNum rc;
        public static List<string> res = new List<string>();
        public static List<string> dci = new List<string>();
        public static List<string> cap = new List<string>();
        public static List<string> Oltc = new List<string>();


        public static List<string> res1 = new List<string>();
        public static List<string> dci1 = new List<string>();
        public static List<string> cap1 = new List<string>();
        public static List<string> Oltc1 = new List<string>();

        private int NumKind = 0;
        private int OltcNum = 0;

        /// <summary>
        /// 初始化内存
        /// </summary>
        public void Reinitialize()
        {
            rc.CaPaNum = 0;
            rc.DcReNum = 0;
            rc.DcInNUm = 0;
            rc.OltcNum = 0;
            NumKind = 0;
            OltcNum = 0;
            rc.DcReNumEnable = false;
            rc.DcInNUmEnable = false;
            rc.CaPaNumEnable = false;
            rc.OltcNumEnable = false;
            IsUpan = false;
            res.Clear();
            dci.Clear();
            cap.Clear();
            Oltc.Clear();
            res1.Clear();
            dci1.Clear();
            cap1.Clear();
            Oltc1.Clear();
        }
        private string CreateDcResistancePra(MeasurementItemStruct mi, JobList job)
        {
            if (mi.Terimal != null)
            {
                Parameter.ZldzStation Dcposition;
                string WindingConfig = "00";
                string Windingkind = "00";
                string DcResistanceCurrent = "00";
                if (mi.Winding == WindingType.HV)
                {
                    Dcposition = Parameter.ZldzStation.高压全部 + (((int)mi.Terimal[0]) % 4);//1
                }
                else if (mi.Winding == WindingType.MV)
                {
                    Dcposition = Parameter.ZldzStation.中压全部 + (((int)mi.Terimal[0]) % 4);//5
                }
                else
                {
                    Dcposition = Parameter.ZldzStation.低压全部 + (((int)mi.Terimal[0]) % 4);//9
                }
                if ((int)mi.WindingConfig == 0) WindingConfig = "00";
                if ((int)mi.WindingConfig == 1) WindingConfig = "01";
                if ((int)mi.WindingConfig == 2) WindingConfig = "02";
                if ((int)mi.WindingConfig == 3) WindingConfig = "03";
                if (GetParameter.GetPraDCResistanceCurrent(job) == Parameter.ZldzCurrent._1A) DcResistanceCurrent = "00";
                if (GetParameter.GetPraDCResistanceCurrent(job) == Parameter.ZldzCurrent._3A) DcResistanceCurrent = "01";
                if (GetParameter.GetPraDCResistanceCurrent(job) == Parameter.ZldzCurrent._10A) DcResistanceCurrent = "02";
                switch (Dcposition)
                {
                    case Parameter.ZldzStation.高压AB_A:
                        Windingkind = "01";
                        break;
                    case Parameter.ZldzStation.高压BC_B:
                        Windingkind = "02";
                        break;
                    case Parameter.ZldzStation.高压CA_C:
                        Windingkind = "03";
                        break;
                    case Parameter.ZldzStation.中压AB_A:
                        Windingkind = "05";
                        break;
                    case Parameter.ZldzStation.中压BC_B:
                        Windingkind = "06";
                        break;
                    case Parameter.ZldzStation.中压CA_C:
                        Windingkind = "07";
                        break;
                    case Parameter.ZldzStation.低压AB_A:
                        Windingkind = "09";
                        break;
                    case Parameter.ZldzStation.低压BC_B:
                        Windingkind = "0A";
                        break;
                    case Parameter.ZldzStation.低压CA_C:
                        Windingkind = "0B";
                        break;
                    default:
                        Windingkind = "01";
                        break;
                }
                rc.DcReNumEnable = true;
                rc.DcReNum++;
                return DcResistanceCurrent + Windingkind + WindingConfig;


            }
            else
            {
                string WindingConfig = "00";
                string Windingkind = "00";
                string DcResistanceCurrent = "00";
                if ((int)mi.WindingConfig == 0) WindingConfig = "00";
                if ((int)mi.WindingConfig == 1) WindingConfig = "01";
                if ((int)mi.WindingConfig == 2) WindingConfig = "02";
                if ((int)mi.WindingConfig == 3) WindingConfig = "03";

                if (mi.Winding == WindingType.HV) Windingkind = "00";
                if (mi.Winding == WindingType.MV) Windingkind = "01";
                if (mi.Winding == WindingType.LV) Windingkind = "02";

                if (GetParameter.GetPraDCResistanceCurrent(job) == Parameter.ZldzCurrent._1A) DcResistanceCurrent = "00";
                if (GetParameter.GetPraDCResistanceCurrent(job) == Parameter.ZldzCurrent._3A) DcResistanceCurrent = "01";
                if (GetParameter.GetPraDCResistanceCurrent(job) == Parameter.ZldzCurrent._10A) DcResistanceCurrent = "02";
                rc.DcReNum++;
                rc.DcReNumEnable = true;
                return DcResistanceCurrent + Windingkind + WindingConfig;
            }

        }
        private string CreateDCInsulationPra(MeasurementItemStruct mi, JobList job)
        {
            if (mi.Winding.ToJYDZstation() == Parameter.JYDZstation.高压绕组 ||
               mi.Winding.ToJYDZstation() == Parameter.JYDZstation.中压绕组 ||
               mi.Winding.ToJYDZstation() == Parameter.JYDZstation.低压绕组)
            {
                string DCInsulationVoltage = GetParameter.GetPraDCInsulationVoltageNum(job);
                string Windingkind = "00";//默认00
                if (mi.Winding == WindingType.HV) Windingkind = "00";
                if (mi.Winding == WindingType.MV) Windingkind = "01";
                if (mi.Winding == WindingType.LV) Windingkind = "02";
                rc.DcInNUm++;
                rc.DcInNUmEnable = true;

                return DCInsulationVoltage + Windingkind + "02" + "64" + "64";
            }
            else
            {
                Parameter.JYDZstation position;
                string DCInsulationVoltage = GetParameter.GetPraDCInsulationVoltageNum(job);
                string Windingkind = null;
                if (mi.Winding == WindingType.HV)
                {
                    position = (Parameter.JYDZstation)(Parameter.JYDZstation.高压套管A + (((int)mi.Terimal[0] + 3) % 4));
                }
                else
                {
                    position = (Parameter.JYDZstation)(Parameter.JYDZstation.中压套管A + (((int)mi.Terimal[0] + 3) % 4));
                }
                switch (position)
                {
                    case Parameter.JYDZstation.高压套管A:
                        Windingkind = "03";
                        break;
                    case Parameter.JYDZstation.高压套管B:
                        Windingkind = "04";
                        break;
                    case Parameter.JYDZstation.高压套管C:
                        Windingkind = "05";
                        break;
                    case Parameter.JYDZstation.高压套管0:
                        Windingkind = "06";
                        break;
                    case Parameter.JYDZstation.中压套管A:
                        Windingkind = "07";
                        break;
                    case Parameter.JYDZstation.中压套管B:
                        Windingkind = "08";
                        break;
                    case Parameter.JYDZstation.中压套管C:
                        Windingkind = "09";
                        break;
                    case Parameter.JYDZstation.中压套管0:
                        Windingkind = "0A";
                        break;
                    default:
                        Windingkind = "03";
                        break;
                }
                rc.DcInNUm++;
                rc.DcInNUmEnable = true;

                return DCInsulationVoltage + Windingkind + "02" + "64" + "64";
            }


        }
        //介损供体
        private string CreateCapacitancePra(MeasurementItemStruct mi, JobList job)
        {
            if (mi.Winding.ToJSstation() == Parameter.JSstation.高压绕组 ||
                mi.Winding.ToJSstation() == Parameter.JSstation.中压绕组 ||
                mi.Winding.ToJSstation() == Parameter.JSstation.低压绕组)
            {
                string CapacitanceVoltage = "00";
                CapacitanceVoltage = GetParameter.GetPraCapacitanceVoltageNum(job);
                string Fre = "01";
                string Wt = "01";
                rc.CaPaNum++;
                rc.CaPaNumEnable = true;
                return CapacitanceVoltage + Fre + Wt + NumCorrection.KeepNum(((int)mi.Winding.ToJSstation()).ToString(), 2, HeadOrTail.Head);
            }
            else
            {
                Parameter.JSstation Jsposition;
                string Windingkind = null;

                if (mi.Winding == WindingType.HV)
                {
                    Jsposition = Parameter.JSstation.高压套管A + (((int)mi.Terimal[0] + 3) % 4);
                }
                else
                {
                    Jsposition = Parameter.JSstation.中压套管A + (((int)mi.Terimal[0] + 3) % 4);
                }
                switch (Jsposition)
                {
                    case Parameter.JSstation.高压套管A:
                        Windingkind = "03";
                        break;
                    case Parameter.JSstation.高压套管B:
                        Windingkind = "04";
                        break;
                    case Parameter.JSstation.高压套管C:
                        Windingkind = "05";
                        break;
                    case Parameter.JSstation.高压套管0:
                        Windingkind = "06";
                        break;
                    case Parameter.JSstation.中压套管A:
                        Windingkind = "07";
                        break;
                    case Parameter.JSstation.中压套管B:
                        Windingkind = "08";
                        break;
                    case Parameter.JSstation.中压套管C:
                        Windingkind = "09";
                        break;
                    case Parameter.JSstation.中压套管0:
                        Windingkind = "0A";
                        break;
                    default:
                        Windingkind = "03";
                        break;
                }
                string CapacitanceVoltage = GetParameter.GetPraCapacitanceVoltageNum(job);
                string Fre = "01";
                string Wt = "00";
                rc.CaPaNum++;
                rc.CaPaNumEnable = true;

                return CapacitanceVoltage + Fre + Wt + Windingkind;

            }

        }
        private string CreateOLTcPra(MeasurementItemStruct mi, JobList job, int oltcnum)
        {
            string Position = "00";
            if (mi.Winding == WindingType.HV)
                Position = "00";
            else
                Position = "01";
            rc.OltcNum++;
            rc.OltcNumEnable = true;

            return "00" + Position + NumCorrection.KeepNum(oltcnum.ToString(), 2, HeadOrTail.Head) + "05" + "20";

        }

        private string ParsingWindConfig(JobList job)
        {
            TransformerWindingConfigStruct wt = job.Transformer.WindingConfig;
            string wk1, wk2, wk3;
            switch (wt.HV)
            {
                case TransformerWindingConfigName.Yn:
                    wk1 = "00";
                    break;
                case TransformerWindingConfigName.Y:
                    wk1 = "01";
                    break;
                case TransformerWindingConfigName.D:
                    wk1 = "02";
                    break;
                case TransformerWindingConfigName.Zn:
                    wk1 = "03";
                    break;
                default:
                    wk1 = "00";
                    break;
            }
            switch (wt.MV)
            {
                case TransformerWindingConfigName.Yn:
                    wk2 = "00";
                    break;
                case TransformerWindingConfigName.Y:
                    wk2 = "01";
                    break;
                case TransformerWindingConfigName.D:
                    wk2 = "02";
                    break;
                case TransformerWindingConfigName.Zn:
                    wk2 = "03";
                    break;
                default:
                    wk2 = "00";
                    break;
            }
            switch (wt.LV)
            {
                case TransformerWindingConfigName.Yn:
                    wk3 = "00";
                    break;
                case TransformerWindingConfigName.Y:
                    wk3 = "01";
                    break;
                case TransformerWindingConfigName.D:
                    wk3 = "02";
                    break;
                case TransformerWindingConfigName.Zn:
                    wk3 = "03";
                    break;
                default:
                    wk3 = "00";
                    break;
            }
            return wk1 + wk2 + wk3;
        }

        private string ParsingWingKing(JobList job)
        {
            string wk1, wk2;
            TransformerWindingConfigStruct wt = job.Transformer.WindingConfig;
            switch (wt.MVLabel)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    wk1 = "0" + wt.MVLabel;
                    break;
                case 10:
                    wk1 = "0A";
                    break;
                default:
                    wk1 = "0A";
                    break;
            }
            switch (wt.LVLabel)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    wk2 = "0" + wt.LVLabel;
                    break;
                case 10:
                    wk2 = "0A";
                    break;
                default:
                    wk2 = "0A";
                    break;
            }
            return wk1 + wk2;
        }

        public string ParsingNum(int Num)
        {
            if (Num >= 0 && Num < 16)
            {
                return "0" + NumCorrection.Ten2Hex(Num.ToString());
            }
            if (Num >= 16 && Num < 255)
            {
                return NumCorrection.Ten2Hex(Num.ToString());
            }
            else
                return "11";
        }
        /// <summary>
        /// 序列化job类到本地
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public void saveJob(JobList job, string NeedSavePath)
        {
            FileStream fs = new FileStream(NeedSavePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, job);
            fs.Close();
        }
        /// <summary>
        /// 读取本地序列化对象
        /// </summary>
        /// <param name="jobPath"></param>
        /// <returns></returns>
        public JobList ReadJob(string jobPath)
        {
            // FileStream fs = new FileStream(@"D:\Jobserial.dat", FileMode.Create);
            FileStream fs = new FileStream(jobPath, FileMode.Open);
            BinaryFormatter bf1 = new BinaryFormatter();
            JobList job = (JobList)bf1.Deserialize(fs);// as JobList;
            fs.Flush();
            fs.Close();
            return job;
        }

        public void savListmi(List<MeasurementItemStruct> lm, string NeedSavePath)
        {
            FileStream fs = new FileStream(NeedSavePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, lm);
            fs.Close();
        }
        public List<MeasurementItemStruct> ReadListmi(string listmipath)
        {
            // FileStream fs = new FileStream(@"D:\Jobserial.dat", FileMode.Create);
            FileStream fs = new FileStream(listmipath, FileMode.Open);
            BinaryFormatter bf1 = new BinaryFormatter();
            return (List<MeasurementItemStruct>)bf1.Deserialize(fs);// as JobList;
        }
        /// <summary>
        /// 扫描文件导入数据库
        /// </summary>
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
                    num = ct.Length;
                    WriteDataToFile.UpadataStringOfFile(drives[drives.Length - 1] + "List.lis", 0,
                        NumCorrection.KeepNum(num.ToString(), 2, HeadOrTail.Head));
                }
                foreach (var Tn in TaskNames)
                {
                    string taskPath = drives[drives.Length - 1] + Tn;
                    int hashcode = UseUpanDoWork.LocalUsb.ReadJob(taskPath + "\\" + "Jobtask.dat").Information.GetHashCode();
                    if (false == WorkingSets.local.rowsIsOfResuly(hashcode.ToString()))
                    {
                        Insertdatabyupan inser = new Insertdatabyupan(taskPath);
                        inser.InsertUpandatatodatabase();
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public string[] GetTaskName()
        {
            String[] drives = Environment.GetLogicalDrives();
            if (File.Exists(drives[drives.Length - 1] + "List.lis"))
            {
                string filecontent = WriteDataToFile.ReadFile(drives[drives.Length - 1] + "List.lis");
                filecontent = filecontent.Remove(filecontent.Length - 1);
                string[] ct = filecontent.Split(';');
                char[] f1 = ct[0].ToArray();
                string[] TaskNames = filecontent.Remove(0, 2).Split(';');//任务名称集合

                int num = Convert.ToInt32("0x" + f1[0].ToString() + f1[1].ToString(), 16);//预定任务数量
                if (num != ct.Length)
                {
                    //错误
                    num = ct.Length;
                    WriteDataToFile.UpadataStringOfFile(drives[drives.Length - 1] + "List.lis", 0,
                        NumCorrection.KeepNum(num.ToString(), 2, HeadOrTail.Head));
                }
                return TaskNames;
            }
            return null;
        }
        public string CreateTrans(JobList job)
        {
            TransformerWindingConfigStruct wt = job.Transformer.WindingConfig;
            int PhyNum = job.Transformer.PhaseNum;

            string TransName = NumCorrection.KeepNumSpace(job.Transformer.SerialNo, 64, HeadOrTail.Tail);
            string TransID = NumCorrection.KeepNumSpace(job.Transformer.ID.ToString(), 10, HeadOrTail.Tail);
            string PhyNumStr = string.Empty;
            if (PhyNum == 2)
                PhyNumStr = "01";
            else
                PhyNumStr = "00";
            string WindConFig = ParsingWindConfig(job);//2/3
            string KindNum = ParsingWingKing(job);
            string HTamMessage;
            if (job.Transformer.OLTC.WindingPosition == WindingType.HV)
            {
                HTamMessage = ParsingNum(job.Transformer.OLTC.TapNum) +
                      ParsingNum(job.Transformer.OLTC.TapMainNum) + "00" +
                      NumCorrection.DoubleToHexNum(job.Transformer.OLTC.Step);
            }
            else
            {
                HTamMessage = "0000000000";
            }
            string MTamMessage;
            if (job.Transformer.OLTC.WindingPosition == WindingType.MV)
            {
                MTamMessage = ParsingNum(job.Transformer.OLTC.TapNum) +
                      ParsingNum(job.Transformer.OLTC.TapMainNum) + "00" +
                      NumCorrection.DoubleToHexNum(job.Transformer.OLTC.Step);
            }
            else
            {
                MTamMessage = "0000000000";
            }
            //job.Transformer.OLTC.Step==0.0124;
            string p1 = NumCorrection.Ten2Hex(((job.Transformer.PowerRating.HV) * 1000).ToString());
            string p2 = NumCorrection.Ten2Hex(((job.Transformer.PowerRating.MV) * 1000).ToString());
            string p3 = NumCorrection.Ten2Hex(((job.Transformer.PowerRating.LV) * 1000).ToString());
            string PowerRating = NumCorrection.KeepNum(p1, 8, HeadOrTail.Head) + NumCorrection.KeepNum(p2, 8, HeadOrTail.Head) + NumCorrection.KeepNum(p3, 8, HeadOrTail.Head);
            string v1 = NumCorrection.Ten2Hex(((job.Transformer.VoltageRating.HV) * 1000).ToString());
            string v2 = NumCorrection.Ten2Hex(((job.Transformer.VoltageRating.MV) * 1000).ToString());
            string v3 = NumCorrection.Ten2Hex(((job.Transformer.VoltageRating.LV) * 1000).ToString());
            string VoltageRating = NumCorrection.KeepNum(v1, 8, HeadOrTail.Head) + NumCorrection.KeepNum(v2, 8, HeadOrTail.Head) + NumCorrection.KeepNum(v3, 8, HeadOrTail.Head);
            return TransName + TransID + PhyNumStr
                + WindConFig + KindNum + HTamMessage + MTamMessage + PowerRating + VoltageRating;
            //  string 
        }

        public string CreateTestList()
        {
            #region Enable
            string[] TestNum = new string[8];
            if (rc.CaPaNumEnable == true)
                TestNum[0] = "01";
            else
                TestNum[0] = "00";
            if (rc.DcReNumEnable == true)
                TestNum[3] = "01";
            else
                TestNum[3] = "00";
            if (rc.DcInNUmEnable == true)
                TestNum[1] = "01";
            else
                TestNum[1] = "00";
            if (rc.OltcNumEnable == true)
                TestNum[2] = "01";
            else
                TestNum[2] = "00";
            TestNum[4] = "00";
            TestNum[5] = "00";
            TestNum[6] = "00";
            TestNum[7] = "00";
            #endregion
            string TestListTask = CreateSortTest();
            int TestCount = res.Count + dci.Count + cap.Count + Oltc.Count;

            return NumCorrection.KeepNum(NumCorrection.Ten2Hex(TestCount.ToString()), 2, HeadOrTail.Head) + string.Join("", TestNum) + TestListTask;
        }


        private string CreateSortTest()
        {
            string[] NeedSortBuffet = new string[res1.Count + cap1.Count + dci1.Count + Oltc1.Count];
            if (res1 != null)
            {
                for (int i = 0; i < res1.Count; i++)
                {
                    if (NumCorrection.GetNumForstring(res1[i]) >= 0)
                    {

                        string TaskKind = "04";
                        string TaskNum = NumCorrection.KeepNum(NumCorrection.Ten2Hex(i.ToString()), 2, HeadOrTail.Head);//resisitance 对应任务号
                        string DcresEnable = "01";
                        NeedSortBuffet[NumCorrection.GetNumForstring(res1[i])] = TaskKind + TaskNum + DcresEnable;
                    }
                }
            }
            if (cap1 != null)
            {
                for (int i = 0; i < cap1.Count; i++)
                {
                    if (NumCorrection.GetNumForstring(cap1[i]) >= 0)
                    {
                        string TaskKind = "01";
                        string TaskNum = NumCorrection.KeepNum(NumCorrection.Ten2Hex(i.ToString()), 2, HeadOrTail.Head);//resisitance 对应任务号
                        string DcresEnable = "01";
                        NeedSortBuffet[NumCorrection.GetNumForstring(cap1[i])] = TaskKind + TaskNum + DcresEnable;

                        // NeedSortBuffet[NumCorrection.GetNumForstring(cap1[i])] = cap[i];

                    }
                }
            }
            if (dci1 != null)
            {
                for (int i = 0; i < dci1.Count; i++)
                {
                    if (NumCorrection.GetNumForstring(dci1[i]) >= 0)
                    {
                        string TaskKind = "02";
                        string TaskNum = NumCorrection.KeepNum(NumCorrection.Ten2Hex(i.ToString()), 2, HeadOrTail.Head);//resisitance 对应任务号
                        string DcresEnable = "01";
                        NeedSortBuffet[NumCorrection.GetNumForstring(dci1[i])] = TaskKind + TaskNum + DcresEnable;
                        //  NeedSortBuffet[NumCorrection.GetNumForstring(dci1[i])] = dci[i];

                    }
                }
            }
            if (Oltc1 != null)
            {
                for (int i = 0; i < Oltc1.Count; i++)
                {
                    if (NumCorrection.GetNumForstring(Oltc1[i]) >= 0)
                    {
                        string TaskKind = "03";
                        string TaskNum = NumCorrection.KeepNum(NumCorrection.Ten2Hex(i.ToString()), 2, HeadOrTail.Head);//resisitance 对应任务号
                        string DcresEnable = "01";
                        NeedSortBuffet[NumCorrection.GetNumForstring(Oltc1[i])] = TaskKind + TaskNum + DcresEnable;
                        // NeedSortBuffet[NumCorrection.GetNumForstring(Oltc1[i])] = Oltc[i];

                    }
                }
            }
            for (int i = 0; i < NeedSortBuffet.Length; i++)
            {
                if (NeedSortBuffet[i] == null)
                    NeedSortBuffet[i] = "00";
            }
            return string.Join("", NeedSortBuffet);

        }


        //先创建测量数据再创建测量列表
        public void StartBuiltTestData(MeasurementItemStruct mi, JobList job)
        {
            switch (mi.Function)
            {
                case MeasurementFunction.DCInsulation:
                    dci.Add(CreateDCInsulationPra(mi, job));
                    dci1.Add(NumCorrection.KeepNum(NumKind.ToString(), 2, HeadOrTail.Head) + CreateDCInsulationPra(mi, job));
                    NumKind++;
                    break;
                case MeasurementFunction.BushingDCInsulation:
                    dci.Add(CreateDCInsulationPra(mi, job));
                    dci1.Add(NumCorrection.KeepNum(NumKind.ToString(), 2, HeadOrTail.Head) + CreateDCInsulationPra(mi, job));
                    NumKind++;
                    break;
                case MeasurementFunction.Capacitance:
                    cap.Add(CreateCapacitancePra(mi, job));
                    cap1.Add(NumCorrection.KeepNum(NumKind.ToString(), 2, HeadOrTail.Head) + CreateCapacitancePra(mi, job));
                    NumKind++;
                    break;
                case MeasurementFunction.BushingCapacitance:
                    cap.Add(CreateCapacitancePra(mi, job));
                    cap1.Add(NumCorrection.KeepNum(NumKind.ToString(), 2, HeadOrTail.Head) + CreateCapacitancePra(mi, job));
                    NumKind++;
                    break;
                case MeasurementFunction.DCResistance://直流电阻
                    res.Add(CreateDcResistancePra(mi, job));
                    res1.Add(NumCorrection.KeepNum(NumKind.ToString(), 2, HeadOrTail.Head) + CreateDcResistancePra(mi, job));
                    NumKind++;
                    break;
                case MeasurementFunction.OLTCSwitchingCharacter:
                    Oltc.Add(CreateOLTcPra(mi, job, OltcNum));
                    Oltc1.Add(NumCorrection.KeepNum(NumKind.ToString(), 2, HeadOrTail.Head) + CreateOLTcPra(mi, job, OltcNum));
                    NumKind++;
                    OltcNum++;
                    break;
            }
        }
        public List<ResultOfUPan> ReturnTestResult()
        {
            ResultOfUPan result = new ResultOfUPan(res, dci, cap, Oltc);
            List<ResultOfUPan> ts = new List<ResultOfUPan>();
            ts.Add(result);
            return ts;
        }

        ~UPanList()
        {
            GC.Collect();
        }

    }
    public static class UseUpanDoWork
    {
        public static UPanList LocalUsb = new UPanList();
    }


}
