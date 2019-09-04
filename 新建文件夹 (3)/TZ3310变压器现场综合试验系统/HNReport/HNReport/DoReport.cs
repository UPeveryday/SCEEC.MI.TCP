using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNReport
{
    public enum ReportOperator { Print, Previev, Design }
    public static class DoReport
    {
        /// <summary>
        /// 显示报告
        /// </summary>
        /// <param name="oper">显示方式</param>
        /// <param name="testCode">二维码</param>
        /// <param name="testKind">试品类型</param>
        public static void Run(ReportOperator oper,string testCode,string testKind = "配电变压器")
        {            
            DataSet dsReport = GetReportDataSet(testCode);
            StiReportHelper reportHelper = new StiReportHelper();
            string reportFileName = testKind + ".mrt"; 
            reportHelper.RunReport(reportFileName, oper, dsReport, null);
        }
        /// <summary>
        /// 整合报告数据
        /// </summary>
        /// <param name="testCode">二维码</param>
        /// <returns>报告数据</returns>
        private static DataSet GetReportDataSet(string testCode)
        {
            DataSet resData = new DataSet();
            //DataTable dt = new DataTable();            //新建对象
            //dt.Columns.Add("姓名", typeof(string));   //新建第一列
            //dt.Columns.Add("年龄", typeof(int));      //新建第二列
            //dt.Rows.Add("张三",23);                 //新建第一行，并赋值
            //dt.Rows.Add("李四",25);                 //新建第二行，并赋值
            //resData.Tables.Add(dt);

            MYDataBaseHelper mydataHeper = new MYDataBaseHelper();
            string sql = $"select * from parameter_information where TestCode= '{testCode}'";
            DataTable param1 = mydataHeper.GetDataTable(sql, "参数信息");
            if (param1.Rows.Count == 0)
            {
                throw new Exception($"参数信息表中不存在二维码为:{testCode}的数据！");
            }
            resData.Tables.Add(param1);

            sql = $"select * from sample_information where TestCode= '{testCode}'";
            DataTable param2 = mydataHeper.GetDataTable(sql, "试品信息");
            resData.Tables.Add(param2);

            sql = $"select * from conclusion where TestCode= '{testCode}'";
            DataTable param3 = mydataHeper.GetDataTable(sql, "试验结论");
            resData.Tables.Add(param3);

            sql = $"select * from casingtest_commonbody where TestCode= '{testCode}'";
            DataTable param4 = mydataHeper.GetDataTable(sql, "套管试验");
            resData.Tables.Add(param4);

            sql = $"select * from casingtest_commonbodyresults where TestCode= '{testCode}'";
            DataTable param5 = mydataHeper.GetDataTable(sql, "套管试验结果");
            resData.Tables.Add(param5);

            sql = $"select * from dcresistor_highpressure where TestCode= '{testCode}'";
            DataTable param6 = mydataHeper.GetDataTable(sql, "直流电阻高压");
            resData.Tables.Add(param6);

            sql = $"select * from dcresistor_highpressureresults where TestCode= '{testCode}'";
            DataTable param7 = mydataHeper.GetDataTable(sql, "直流电阻高压结果");
            resData.Tables.Add(param7);

            sql = $"select * from dcresistor_lowpressure where TestCode= '{testCode}'";
            DataTable param8 = mydataHeper.GetDataTable(sql, "直流电阻低压");
            resData.Tables.Add(param8);

            sql = $"select * from dcresistor_lowpressureresults where TestCode= '{testCode}'";
            DataTable param9 = mydataHeper.GetDataTable(sql, "直流电阻低压结果");
            resData.Tables.Add(param9);

            sql = $"select * from dcresistor_mediumvoltage where TestCode= '{testCode}'";
            DataTable param10 = mydataHeper.GetDataTable(sql, "直流电阻中压");
            resData.Tables.Add(param10);

            sql = $"select * from dcresistor_mediumvoltageresults where TestCode= '{testCode}'";
            DataTable param11 = mydataHeper.GetDataTable(sql, "直流电阻中压结果");
            resData.Tables.Add(param11);

            sql = $"select * from dielectriclossandcapacitance_threewinding where TestCode= '{testCode}'";
            DataTable param12 = mydataHeper.GetDataTable(sql, "介质损耗和电容三绕组");
            resData.Tables.Add(param12);

            sql = $"select * from dielectriclossandcapacitance_threewindingresults where TestCode= '{testCode}'";
            DataTable param13 = mydataHeper.GetDataTable(sql, "介质损耗和电容三绕组结果");
            resData.Tables.Add(param13);

            sql = $"select * from insulationresistance_threewinding where TestCode= '{testCode}'";
            DataTable param14 = mydataHeper.GetDataTable(sql, "绕组绝缘电阻三绕组");
            resData.Tables.Add(param14);

            sql = $"select * from insulationresistance_threewindingresults where TestCode= '{testCode}'";
            DataTable param15 = mydataHeper.GetDataTable(sql, "绕组绝缘电阻三绕组结果");
            resData.Tables.Add(param15);

            sql = $"select * from tapchangertest where TestCode= '{testCode}'";
            DataTable param16 = mydataHeper.GetDataTable(sql, "分接开关试验");
            resData.Tables.Add(param16);

            sql = $"select * from tapchangertestresults where TestCode= '{testCode}'";
            DataTable param17 = mydataHeper.GetDataTable(sql, "分接开关试验结果");
            resData.Tables.Add(param17);

            sql = $"select * from windingcoreinsulationresistance where TestCode= '{testCode}'";
            DataTable param18 = mydataHeper.GetDataTable(sql, "绕组铁芯绝缘电阻");
            resData.Tables.Add(param18);

            sql = $"select * from windingcoreinsulationresistanceresults where TestCode= '{testCode}'";
            DataTable param19 = mydataHeper.GetDataTable(sql, "绕组铁芯绝缘电阻结果");
            resData.Tables.Add(param19);
            return resData;
        }
    }
}
