using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using Stimulsoft.Report;
using Stimulsoft.Base.Localization;
using System.Windows.Forms;

namespace HNReport
{
 
    public delegate void OutputReportInfo(string info);

    public class StiReportHelper
    {
        OutputReportInfo outInfo = null;
        public static readonly string ReportDir = null;

        static StiReportHelper()
        {
            ReportDir = Application.StartupPath + "\\Report\\";
            if (!Directory.Exists(ReportDir))
                Directory.CreateDirectory(ReportDir);

            StiOptions.Configuration.SearchLocalizationFromRegistry = false;

            string stiPath = Application.StartupPath + "\\Report\\Localization";

            StiLocalization.DirectoryLocalization = stiPath;
            StiLocalization.Localization = "zh-CHS.xml";

            StiSettings.Set("Localization", "FileName", "zh-CHS.xml");//设置中文菜单
            StiSettings.Save();
        }

        public OutputReportInfo OutputInfo
        {
            set { outInfo = value; }
        }

        public void RunReport(string reportFileName, ReportOperator opera, DataSet reportData,
                    List<KeyValuePair<string, object>> varList)
        {
            if (outInfo != null)
                outInfo("正在装载报表文件:" + ReportDir + reportFileName);


            bool isNewFile = !File.Exists(ReportDir + reportFileName); //是新文件吗

            StiReport report = new StiReport();

            if (ReportDir + reportFileName != null && !isNewFile)
            {
                report.Load(ReportDir + reportFileName);
            }


            if (reportData != null)
                report.RegData(reportData);

            if (outInfo != null)
                outInfo("正在初始化报表 ...");

            AddReportVar(report, varList);

            if (!report.IsStopped)
            {
                if (opera == ReportOperator.Previev)
                {
                    report.Show(true);
                }
                else if (opera == ReportOperator.Print)
                {
                    report.Print(false);
                }
                else if (opera == ReportOperator.Design)
                {
                    if (isNewFile)
                        report.ReportFile = ReportDir + reportFileName;

                    report.Design(true);
                }
            }

            report.Dispose();
            report = null;
            if (outInfo != null)
                outInfo("准备");

        }

        public void RunReport(string reportFile, ReportOperator opera, string dataSetFile,
                    List<KeyValuePair<string, object>> varList)
        {
            DataSet dsData = null;
            if (dataSetFile != null && File.Exists(dataSetFile))
            {
                dsData = new DataSet("数据集");
                dsData.ReadXml(dataSetFile);
            }
            RunReport(reportFile, opera, dsData, varList);
        }

        public void RunReport(string reportFile, ReportOperator opera, DataTable dataTable,
                    List<KeyValuePair<string, object>> varList)
        {
            DataSet dsData = null;
            if (dataTable != null)
            {
                dsData = new DataSet("数据集");
                dsData.Tables.Add(dataTable);
            }
            RunReport(reportFile, opera, dsData, varList);
        }


        private void OnReportRendering(object sender, EventArgs e)
        {
            if (outInfo != null)
            {
                outInfo("初始化页: " + (((StiReport)sender).PageNumber - 1).ToString());
            }
        }

        private void AddReportVar(StiReport report, List<KeyValuePair<string, object>> varList)
        {
            if (varList == null)
                return;

            Stimulsoft.Report.Dictionary.StiVariablesCollection vars = report.Dictionary.Variables;

            foreach (KeyValuePair<string, object> kv in varList)
            {
                if (vars.Contains(kv.Key))
                    vars[kv.Key].Value = Convert.ToString(kv.Value);
                else
                    vars.Add(kv.Key, kv.Value);
            }
        }
    }
}
