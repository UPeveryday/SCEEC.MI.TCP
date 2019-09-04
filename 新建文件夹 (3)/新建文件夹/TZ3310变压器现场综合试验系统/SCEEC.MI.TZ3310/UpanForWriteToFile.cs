using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SCEEC.MI.TZ3310
{
    public static class UpanForWriteToFile
    {
        /// <summary>
        /// 将四个功能的测量等功能写入文件
        /// </summary>
        /// <param name="respath"></param>
        /// <param name="dcipath"></param>
        /// <param name="cappath"></param>
        /// <param name="oltcpath"></param>
        private static void CreateWorkList(string respath, string dcipath, string cappath, string oltcpath)
        {

            var result = UseUpanDoWork.LocalUsb.ReturnTestResult();
            string dciComman = NumCorrection.KeepNum(NumCorrection.Ten2Hex(result[0].dci.Count().ToString()),
                2, HeadOrTail.Head) + string.Join("", result[0].dci);
            string resComman = NumCorrection.KeepNum(NumCorrection.Ten2Hex(result[0].res.Count().ToString()),
                2, HeadOrTail.Head) + string.Join("", result[0].res);
            string capComman = NumCorrection.KeepNum(NumCorrection.Ten2Hex(result[0].cap.Count().ToString()),
                2, HeadOrTail.Head) + string.Join("", result[0].cap);
            string oltcComman = NumCorrection.KeepNum(NumCorrection.Ten2Hex(result[0].oltc.Count().ToString()),
                2, HeadOrTail.Head) + string.Join("", result[0].oltc);
            if (dciComman.Length != 2)
                WriteDataToFile.WriteFile(dcipath, dciComman);
            else
            {
                WriteDataToFile.WriteFile(dcipath, "");
            }
            if (resComman.Length != 2)
                WriteDataToFile.WriteFile(respath, resComman);
            else
            {
                WriteDataToFile.WriteFile(respath, "");
            }
            if (capComman.Length != 2)
                WriteDataToFile.WriteFile(cappath, capComman);
            else
            {
                WriteDataToFile.WriteFile(cappath, "");
            }
            if (oltcComman.Length != 2)
                WriteDataToFile.WriteFile(oltcpath, oltcComman);
            else
            {
                WriteDataToFile.WriteFile(oltcpath, "");
            }

        }
        /// <summary>
        /// 创建变压器的数据列表文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="job"></param>
        private static void CreateTransList(string path, JobList job)
        {
            var transdata = UseUpanDoWork.LocalUsb.CreateTrans(job);
            WriteDataToFile.WriteFile(path, transdata);
        }
        /// <summary>
        /// 创建需要测试的list列表文件，有顺序
        /// </summary>
        /// <param name="path"></param>
        private static void CreateTestList(string path)
        {
            var testlist = UseUpanDoWork.LocalUsb.CreateTestList();
            WriteDataToFile.WriteFile(path, testlist);

        }

        /// <summary>
        /// 创建List.lis文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Content"></param>
        private static void CreateListlis(string path, string Content)
        {
            string LisContent = string.Empty;
            WriteDataToFile.WriteFile(path, Content + ";");
            if (File.Exists(path))
                LisContent = WriteDataToFile.ReadFile(path);
            string JustLisContent = LisContent.Remove(0, 2);
            string[] tempdata = JustLisContent.Split(';');
            List<string> list = new List<string>();
            list.Clear();
            for (int i = 0; i < tempdata.Length - 1; i++)//遍历数组成员
            {
                if (list.IndexOf(tempdata[i]) == -1)//对每个成员做一次新数组查询如果没有相等的则加到新数组
                    list.Add(tempdata[i]);
            }
            WriteDataToFile.FileBaseDeel(path, MyFileMode.Clear);
            string[] tda = list.ToArray();
            string outdata = string.Empty;
            foreach (string a in tda)
                outdata += (a + ";");
            WriteDataToFile.WriteFile(path, NumCorrection.KeepNum(NumCorrection.Ten2Hex(list.Count.ToString()),
                2, HeadOrTail.Head) + outdata);
            // WriteDataToFile.UpadataStringOfFile(path, 0, NumCorrection.KeepNum(num.ToString(), 2, HeadOrTail.Head));
        }
        /// <summary>
        /// 创建所有的文件和任务单
        /// </summary>
        /// <param name="basepath"></param>
        /// <param name="ListlisName"></param>
        /// <param name="job"></param>
        public static void writefile(string basepath, string ListlisName, JobList job, List<MeasurementItemStruct> miList)
        {
            if (!File.Exists(basepath))//根文件
                WriteDataToFile.DeelDirectoryInfo(basepath, Mode.Create);
            string testlispath = basepath + "\\" + ListlisName;
            if (!File.Exists(testlispath))//testlist文件
            {
                WriteDataToFile.FileBaseDeel(testlispath, MyFileMode.Create);
                WriteDataToFile.WriteFile(testlispath, "00");
            }
            string TaskPath = basepath + "Ta" + DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            if (!File.Exists(TaskPath))
                WriteDataToFile.DeelDirectoryInfo(TaskPath, Mode.Create);
            string transpath = TaskPath + "\\TRANS.lis";
            string DcresistancePath = TaskPath + "\\ZLDZ.lis";
            string DciPath = TaskPath + "\\JYDZ.lis";
            string caopath = TaskPath + "\\JZSH.lis";
            string Oltcpayh = TaskPath + "\\YZFJ.lis";
            string TestLispath = TaskPath + "\\" + "TEST.lis";
            if (File.Exists(transpath))
                WriteDataToFile.FileBaseDeel(transpath, MyFileMode.Clear);
            if (File.Exists(DcresistancePath))
                WriteDataToFile.FileBaseDeel(DcresistancePath, MyFileMode.Clear);
            if (File.Exists(DciPath))
                WriteDataToFile.FileBaseDeel(DciPath, MyFileMode.Clear);
            if (File.Exists(caopath))
                WriteDataToFile.FileBaseDeel(caopath, MyFileMode.Clear);
            if (File.Exists(Oltcpayh))
                WriteDataToFile.FileBaseDeel(Oltcpayh, MyFileMode.Clear);
            if (File.Exists(TestLispath))
                WriteDataToFile.FileBaseDeel(TestLispath, MyFileMode.Clear);
            CreateListlis(testlispath, "Ta" + DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString());
            CreateTransList(transpath, job);
            CreateWorkList(DcresistancePath, DciPath, caopath, Oltcpayh);
            CreateTestList(TestLispath);
            CreateBaseMessageIni(TaskPath + "\\Information.ini");//保存用户信息
            UseUpanDoWork.LocalUsb.saveJob(job, TaskPath + "\\Jobtask.dat");//保存job单
            UseUpanDoWork.LocalUsb.savListmi(miList, TaskPath + "\\Listmitask.dat");
            Insertdatabyupan Ins = new Insertdatabyupan(TaskPath);
            job = Ins.UpdataJob(TaskPath);
            UseUpanDoWork.LocalUsb.saveJob(job, TaskPath + "\\Jobtask.dat");//保存job单


        }

        /// <summary>
        /// 创建测试信息
        /// </summary>
        /// <param name="BaseMessagepath">文件名，.ini文件</param>
        public static void CreateBaseMessageIni(string BaseMessagepath)
        {
            if (!File.Exists(BaseMessagepath))
            {
                INIFiLE myini = new INIFiLE(BaseMessagepath);
                myini.WriteString("information", "注意事项", "试验名称为必填项,删除”NULL“写入需要的信息");
                myini.WriteString("information", "测试时间", DateTime.Now.ToString("yyyy-MM-dd"));
                myini.WriteString("information", "试验名称", "NULL");
                myini.WriteString("information", "试验人员", "NULL");
                myini.WriteString("information", "试验单位", "NULL");
                myini.WriteString("information", "审核人", "NULL");
                myini.WriteString("information", "批准人", "NULL");
                myini.WriteString("information", "负责人", "NULL");
                myini.WriteString("information", "天气", "NULL");
                myini.WriteString("information", "温度", "NULL");
                myini.WriteString("information", "湿度", "NULL");
                myini.WriteInt("information", "变压器油温(整数)", 23);
            }
        }
    }
}
