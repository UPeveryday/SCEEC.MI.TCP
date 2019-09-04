using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public class Insertdatabyupan
    {
        internal string Listmipath;
        internal string Taskpath;
        internal string respath;
        internal string dcipath;
        internal string cappath;
        internal string oltcpath;
        internal TestStruct Upandata;
        internal JobList jobInfo;
        internal List<MeasurementItemStruct> ListMi;
        // TestStruct Upandata = ReadUpanDataToDatababase.RetuenTestForUpanData();

        //public Insertdatabyupan()
        //{
        //    //@"D:\\ZLDZ", @"D:\\JYDZ", @"D:\\JZSH", @"D:\\YZFJ"
        //    this.respath = @"D:\\ZLDZ";
        //    this.dcipath = @"D:\\JYDZ";
        //    this.cappath = @"D:\\JZSH";
        //    this.oltcpath = @"D:\\YZFJ";
        //    this.Upandata = ReadUpanDataToDatababase.RetuenTestForUpanData(respath, dcipath, cappath, oltcpath);
        //    this.jobInfo = UseUpanDoWork.LocalUsb.ReadJob(taskpath + "\\Jobtask");
        //}
        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="testpath"></param>
        public Insertdatabyupan(string testpath)
        {
            this.Listmipath = testpath + "\\Listmitask.dat";
            this.respath = testpath + "\\ZLDZ";
            this.dcipath = testpath + "\\JYDZ";
            this.cappath = testpath + "\\JZSH";
            this.oltcpath = testpath + "\\YZFJ";
            this.Taskpath = testpath + "\\Jobtask.dat";
            this.Upandata = ReadUpanDataToDatababase.RetuenTestForUpanData(respath, dcipath, cappath, oltcpath);
            this.jobInfo = UseUpanDoWork.LocalUsb.ReadJob(Taskpath);
            this.jobInfo = UpdataJob(testpath + "\\Information.ini");
            UseUpanDoWork.LocalUsb.saveJob(this.jobInfo, Taskpath);//更新job单到本地文件夹
            this.ListMi = UseUpanDoWork.LocalUsb.ReadListmi(Listmipath);
        }
#if true
        private DataRow UsbToRow(DcresistaceUpandata res)
        {
            System.Data.DataRow row = WorkingSets.local.TestResults.NewRow();
            try { row["testname"] = jobInfo.Information.testingName; } catch { }
            try { row["testid"] = jobInfo.Information.GetHashCode(); } catch { }
            try { row["transformerid"] = jobInfo.Transformer.ID; } catch { }
            try { row["mj_id"] = jobInfo.id; } catch { }
            try { row["mj_id"] = 1; } catch { }
            //row["testname"] =
            try { row["function"] = 3; } catch { }
            try { row["windingtype"] = GetIniFileData.GetResPositionTerimal(res).windingtype; } catch { }
            if (GetIniFileData.GetResPositionTerimal(res).Termal != null)
            {
                try { row["terimal"] = GetIniFileData.GetResPositionTerimal(res).Termal; } catch { }
            }
            try { row["windingconfig"] = GetIniFileData.GetResPositionTerimal(res).windingconfig; } catch { }
            //if (TapLabel != null)
            //{
            //    if (TapLabel.Length == 2)
            //        try { row["taplabel"] = TapLabel[0] + ";" + TapLabel[1]; } catch { }
            //    else if (Terimal.Length == 1)
            //        try { row["taplabel"] = TapLabel[0]; } catch { }
            //    else
            //        try { row["taplabel"] = string.Empty; } catch { }
            //}
            try { row["failed"] = 0; } catch { }
            try { row["completed"] = 1; } catch { }
            try { row["result_pv1"] = res.Av; } catch { }
            try { row["result_pv2"] = res.Ai; } catch { }
            try { row["result_pv3"] = res.Ar; } catch { }
            try { row["result_pv4"] = res.Bv; } catch { }
            try { row["result_pv5"] = res.Bi; } catch { }
            try { row["result_pv6"] = res.Br; } catch { }
            try { row["result_pv7"] = res.Cv; } catch { }
            try { row["result_pv8"] = res.Ci; } catch { }
            try { row["result_pv9"] = res.Cr; } catch { }
            try { row["recordtime"] = res.Time; } catch { }
            return row;
        }
#endif
#if true
        private DataRow UsbToRow(CaptanceUpandata cap)
        {
            System.Data.DataRow row = WorkingSets.local.TestResults.NewRow();
            try { row["testname"] = jobInfo.Information.testingName; } catch { }
            try { row["testid"] = jobInfo.Information.GetHashCode(); } catch { }
            try { row["transformerid"] = jobInfo.Transformer.ID; } catch { }
            try { row["mj_id"] = jobInfo.id; } catch { }
            try { row["mj_id"] = 1; } catch { }
            //row["testname"] =
            try { row["function"] = GetIniFileData.GetCapNum(cap).Function; } catch { }
            try { row["windingtype"] = GetIniFileData.GetCapNum(cap).windingtype; } catch { }
            try { row["terimal"] = GetIniFileData.GetCapNum(cap).Termal; } catch { }
            try { row["windingconfig"] = GetIniFileData.GetCapNum(cap).windingconfig; } catch { }
            try { row["failed"] = 0; } catch { }
            try { row["completed"] = 1; } catch { }
            try { row["result_pv1"] = cap.Fre; } catch { }
            try { row["result_pv2"] = cap.Volate; } catch { }
            try { row["result_pv3"] = cap.Cx; } catch { }
            try { row["result_pv4"] = cap.Tan; } catch { }
            try { row["recordtime"] = cap.Time; } catch { }
            return row;

        }
#endif

        private DataRow UsbToRow(DcInlutionUpandata dci)
        {
            System.Data.DataRow row = WorkingSets.local.TestResults.NewRow();
            try { row["testname"] = jobInfo.Information.testingName; } catch { }
            try { row["testid"] = jobInfo.Information.GetHashCode(); } catch { }
            try { row["transformerid"] = jobInfo.Transformer.ID; } catch { }
            try { row["mj_id"] = jobInfo.id; } catch { }
            try { row["mj_id"] = 1; } catch { }

            //row["testname"] =
            try { row["function"] = GetIniFileData.GetDciNum(dci).Function; } catch { }
            try { row["windingtype"] = GetIniFileData.GetDciNum(dci).windingtype; } catch { }
            try { row["terimal"] = GetIniFileData.GetDciNum(dci).Termal; } catch { }
            try { row["windingconfig"] = GetIniFileData.GetDciNum(dci).windingconfig; } catch { }
            try { row["failed"] = 0; } catch { }
            try { row["completed"] = 1; } catch { }
            try { row["recordtime"] = DateTime.Now; } catch { }

            try { row["result_pv1"] = dci.Volate; } catch { }
            try { row["result_pv2"] = dci.Resistance; } catch { }
            try { row["result_pv3"] = dci.Abs; } catch { }
            try { row["result_pv4"] = dci.Jhzs; } catch { }
            return row;

        }
        private DataRow UsbToRow(OltcUpandata oltc)
        {
            System.Data.DataRow row = WorkingSets.local.TestResults.NewRow();

            try { row["testname"] = jobInfo.Information.testingName; } catch { }
            try { row["testid"] = jobInfo.Information.GetHashCode(); } catch { }
            try { row["transformerid"] = jobInfo.Transformer.ID; } catch { }
            try { row["mj_id"] = jobInfo.id; } catch { }
            try { row["mj_id"] = 1; } catch { }

            try { row["function"] = GetIniFileData.GetOltcNum(oltc).Function; } catch { }
            try { row["windingtype"] = GetIniFileData.GetOltcNum(oltc).windingtype; } catch { }
            try { row["windingconfig"] = GetIniFileData.GetOltcNum(oltc).windingconfig; } catch { }
            try { row["failed"] = 0; } catch { }
            try { row["completed"] = 1; } catch { }
            try { row["recordtime"] = DateTime.Now; } catch { }
            string waves = oltc.Awaveform + oltc.Bwaveform + oltc.Cwaveform + oltc.Dwaveform;
            byte[] byteArray = Encoding.Default.GetBytes(waves);
            try { row["waves"] = Convert.ToBase64String(byteArray); } catch { }
            return row;
        }
        /// <summary>
        /// information 函数存储基本信息
        /// </summary>
        /// <returns></returns>
        private DataRow UpanToinformationRow()
        {
            System.Data.DataRow row = WorkingSets.local.TestResults.NewRow();
            row["testname"] = jobInfo.Information.testingName;
            row["testid"] = jobInfo.Information.GetHashCode();
            try { row["mj_id"] = jobInfo.id; } catch { }
            try { row["mj_id"] = 1; } catch { }

            try { row["transformerid"] = jobInfo.Transformer.ID; } catch { }
            row["function"] = 20;
            row["failed"] = 0;
            row["completed"] = 1;
            row["waves"] = jobInfo.Information.ToString();
            return row;
        }

        /// <summary>
        /// 更新jobinf单
        /// </summary>
        /// <param name="taskinformationpath"></param>
        /// <returns></returns>
        public JobList UpdataJob(string taskinformationpath)
        {
            INIFiLE iNI = new INIFiLE(taskinformationpath);

            if (!DateTime.TryParse(iNI.ReadString("information", "测试时间", ""), out jobInfo.Information.testingTime))
            {
                jobInfo.Information.testingTime = DateTime.Now;
            }
            jobInfo.Information.testingName = iNI.ReadString("information", "试验名称", "");
            jobInfo.Information.tester = iNI.ReadString("information", "试验人员", "");
            jobInfo.Information.testingAgency = iNI.ReadString("information", "试验单位", "");
            jobInfo.Information.auditor = iNI.ReadString("information", "审核人", "");
            jobInfo.Information.approver = iNI.ReadString("information", "批准人", "");
            jobInfo.Information.principal = iNI.ReadString("information", "负责人", "");
            jobInfo.Information.weather = iNI.ReadString("information", "天气", "");
            jobInfo.Information.temperature = iNI.ReadString("information", "温度", "");
            jobInfo.Information.humidity = iNI.ReadString("information", "湿度", "");
            jobInfo.Information.oilTemperature = iNI.ReadInt("information", "变压器油温(整数)", 23);
            return jobInfo;
        }
        


        public void InsertUpandatatodatabase()
        {
            // WorkingSets.local.refreshTestResults();
            foreach (DcresistaceUpandata s in Upandata.res)
            {
                WorkingSets.local.TestResults.Rows.Add(UsbToRow(s));
                WorkingSets.local.saveTestResults();
            }
            foreach (DcInlutionUpandata s in Upandata.dci)
            {
                WorkingSets.local.TestResults.Rows.Add(UsbToRow(s));
                WorkingSets.local.saveTestResults();
            }
            foreach (CaptanceUpandata s in Upandata.cap)
            {
                WorkingSets.local.TestResults.Rows.Add(UsbToRow(s));
                WorkingSets.local.saveTestResults();
            }
            foreach (OltcUpandata s in Upandata.Oltc)
            {
                WorkingSets.local.TestResults.Rows.Add(UsbToRow(s));
                WorkingSets.local.saveTestResults();
            }

            WorkingSets.local.TestResults.Rows.Add(UpanToinformationRow());
            WorkingSets.local.saveTestResults();

        }



    }
}
