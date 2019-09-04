using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SCEEC.MI.TZ3310
{
    public static class DeelDataBase
    {
        public static void InsertDataNewDataBase(DataRow[] rows)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                if ((int)rows[i]["function"] == (int)MeasurementFunction.DCInsulation)
                {
                    DoDCInsulation(rows[i]);
                }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.Capacitance))
                {
                    DoCapacitance(rows[i]);
                }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.DCResistance))
                {
                    DoDcResistance(rows[i]);
                }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.BushingDCInsulation))
                {
                    DoBushingDCInsulation(rows[i]);
                }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.BushingCapacitance))
                {
                    DoBushingCapacitance(rows[i]);
                }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.OLTCSwitchingCharacter))
                {
                    DoOLTESwich(rows[i]);
                }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.DCCharge))
                { }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.OilTemperature))
                { }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.Description))
                { }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.Information))
                { }
                if ((int)rows[i]["function"] == ((int)MeasurementFunction.InternalData))
                { }

            }
        }

        private static void DoDCInsulation(DataRow TestResultRow)
        {
            DataRow row = WorkingSets.local.Insulationresistance_Threewinding.NewRow();
            DataRow rowResult = WorkingSets.local.Insulationresistance_Threewindingresults.NewRow();
            if ((string)TestResultRow["windingtype"] == "0")
                row["High_CentreLowPressure"] = TestResultRow["result_pv2"];
            if ((string)TestResultRow["windingtype"] == "1")
                row["Centre_HighLowPressure"] = TestResultRow["result_pv2"];
            if ((string)TestResultRow["windingtype"] == "2")
                row["Low_HighMediumVoltage"] = TestResultRow["result_pv2"];
            var ji = JobInformation.FromString((string)TestResultRow["waves"]);
            rowResult["TestCode"] = ji.GetHashCode();
            rowResult["Temperature"] = ji.temperature;
            rowResult["Humidity"] = ji.humidity;
            rowResult["OilTemperature"] = ji.oilTemperature;
            WorkingSets.local.Insulationresistance_Threewinding.Rows.Add(row);
            WorkingSets.local.Insulationresistance_Threewindingresults.Rows.Add(row);
            WorkingSets.local.SaveCreateLocateDatabase();
        }
        private static void DoBushingDCInsulation(DataRow TestResultRow)
        {
            DataRow row = WorkingSets.local.Casingtest_Commonbody.NewRow();
            DataRow rowResult = WorkingSets.local.Casingtest_Commonbodyresults.NewRow();

            if ((int)TestResultRow["windingtype"] == 0)
            {
                if ((string)TestResultRow["terimal"] == "0")
                    row["O"] = TestResultRow["result_pv2"];//兆欧表套管
                if ((string)TestResultRow["terimal"] == "A")
                    row["A"] = TestResultRow["result_pv2"];//兆欧表套管
                if ((string)TestResultRow["terimal"] == "B")
                    row["B"] = TestResultRow["result_pv2"];//兆欧表套管
                if ((string)TestResultRow["terimal"] == "C")
                    row["C"] = TestResultRow["result_pv2"];//兆欧表套管
            }
            if ((int)TestResultRow["windingtype"] == 1)
            {
                if ((string)TestResultRow["terimal"] == "0")
                    row["Om"] = TestResultRow["result_pv2"];//兆欧表套管
                if ((string)TestResultRow["terimal"] == "A")
                    row["Am"] = TestResultRow["result_pv2"];//兆欧表套管
                if ((string)TestResultRow["terimal"] == "B")
                    row["Bm"] = TestResultRow["result_pv2"];//兆欧表套管
                if ((string)TestResultRow["terimal"] == "C")
                    row["Cm"] = TestResultRow["result_pv2"];//兆欧表套管
            }
            var ji = JobInformation.FromString((string)TestResultRow["waves"]);
            rowResult["TestCode"] = ji.GetHashCode();
            rowResult["Temperature"] = ji.temperature;
            rowResult["Humidity"] = ji.humidity;
            rowResult["OilTemperature"] = ji.oilTemperature;
            WorkingSets.local.Casingtest_Commonbody.Rows.Add(row);
            WorkingSets.local.Casingtest_Commonbodyresults.Rows.Add(row);
            WorkingSets.local.SaveCreateLocateDatabase();
        }
        private static void DoCapacitance(DataRow TestResultRow)
        {
            DataRow rowResult = WorkingSets.local.Dielectriclossandcapacitance_Threewindingresults.NewRow();
            DataRow CapacitanceRow = WorkingSets.local.Dielectriclossandcapacitance_Threewinding.NewRow();
            DataRow CnRow = WorkingSets.local.Dielectriclossandcapacitance_Threewinding.NewRow();
            if ((int)TestResultRow["windingtype"] == 0)
            {
                CapacitanceRow["HighPressure_CentreLowPressure"] = TestResultRow["result_pv4"];//兆欧表套管
                CnRow["HighPressure_CentreLowPressure"] = TestResultRow["result_pv3"];//兆欧表套管
            }
            if ((int)TestResultRow["windingtype"] == 1)
            {
                CapacitanceRow["MediumVoltage_HighLowPressure"] = TestResultRow["result_pv4"];//兆欧表套管
                CnRow["MediumVoltage_HighLowPressure"] = TestResultRow["result_pv3"];//兆欧表套管
            }
            if ((int)TestResultRow["windingtype"] == 2)
            {
                CapacitanceRow["LowPressure_HighMediumVoltage"] = TestResultRow["result_pv4"];//兆欧表套管
                CnRow["LowPressure_HighMediumVoltage"] = TestResultRow["result_pv3"];//兆欧表套管
            }
            if(TestResultRow["waves"]!=null)
            {
                var ji = JobInformation.FromString((string)TestResultRow["waves"]);
                rowResult["TestCode"] = ji.GetHashCode();
                rowResult["Temperature"] = ji.temperature;
                rowResult["Humidity"] = ji.humidity;
                rowResult["OilTemperature"] = ji.oilTemperature;
            }
            WorkingSets.local.Dielectriclossandcapacitance_Threewinding.Rows.Add(CapacitanceRow);
            WorkingSets.local.Dielectriclossandcapacitance_Threewinding.Rows.Add(CnRow);
            WorkingSets.local.Dielectriclossandcapacitance_Threewindingresults.Rows.Add(CnRow);
            WorkingSets.local.SaveCreateLocateDatabase();
        }
        private static void DoBushingCapacitance(DataRow TestResultRow)
        {
            DataRow CapacitanceRow = WorkingSets.local.Casingtest_Commonbody.NewRow();
            DataRow CnRow = WorkingSets.local.Casingtest_Commonbody.NewRow();
            if ((int)TestResultRow["windingtype"] == 0)
            {
                if ((string)TestResultRow["terimal"] == "0")
                {
                    CapacitanceRow["O"] = TestResultRow["result_pv4"];
                    CnRow["O"] = TestResultRow["result_pv3"];
                }
                if ((string)TestResultRow["terimal"] == "A")
                {
                    CapacitanceRow["A"] = TestResultRow["result_pv4"];
                    CnRow["A"] = TestResultRow["result_pv3"];
                }
                if ((string)TestResultRow["terimal"] == "B")
                {
                    CapacitanceRow["B"] = TestResultRow["result_pv4"];
                    CnRow["B"] = TestResultRow["result_pv3"];
                }
                if ((string)TestResultRow["terimal"] == "C")
                {
                    CapacitanceRow["C"] = TestResultRow["result_pv4"];
                    CnRow["C"] = TestResultRow["result_pv3"];
                }


            }
            if ((string)TestResultRow["windingtype"] == "1")
            {
                if ((string)TestResultRow["terimal"] == "0")
                {
                    CapacitanceRow["Om"] = TestResultRow["result_pv4"];
                    CnRow["Om"] = TestResultRow["result_pv3"];
                }
                if ((string)TestResultRow["terimal"] == "A")
                {
                    CapacitanceRow["Am"] = TestResultRow["result_pv4"];
                    CnRow["Am"] = TestResultRow["result_pv3"];
                }
                if ((string)TestResultRow["terimal"] == "B")
                {
                    CapacitanceRow["Bm"] = TestResultRow["result_pv4"];
                    CnRow["Bm"] = TestResultRow["result_pv3"];
                }

                if ((string)TestResultRow["terimal"] == "C")
                {
                    CapacitanceRow["Cm"] = TestResultRow["result_pv4"];
                    CnRow["Cm"] = TestResultRow["result_pv3"];
                }//兆欧表套管

            }
            DataRow rowResult = WorkingSets.local.Casingtest_Commonbodyresults.NewRow();
            var ji = JobInformation.FromString((string)TestResultRow["waves"]);
            rowResult["TestCode"] = ji.GetHashCode();
            rowResult["Temperature"] = ji.temperature;
            rowResult["Humidity"] = ji.humidity;
            rowResult["OilTemperature"] = ji.oilTemperature;
            WorkingSets.local.Casingtest_Commonbodyresults.Rows.Add(CnRow);
            WorkingSets.local.Casingtest_Commonbody.Rows.Add(CapacitanceRow);
            WorkingSets.local.Casingtest_Commonbody.Rows.Add(CnRow);
            WorkingSets.local.SaveCreateLocateDatabase();
        }
        private static void DoDcResistance(DataRow TestResultRow)
        {
            DataRow rowHigh = WorkingSets.local.Dcresistor_Highpressure.NewRow();
            DataRow rowM = WorkingSets.local.Dcresistor_Mediumvoltage.NewRow();
            DataRow rowLow = WorkingSets.local.Dcresistor_Lowpressure.NewRow();
            if ((string)TestResultRow["terimal"] == "1;2")
            {
                rowHigh["A0"] = TestResultRow["result_pv3"];
                rowHigh["B0"] = TestResultRow["result_pv6"];
                rowHigh["C0"] = TestResultRow["result_pv9"];
            }
            if ((string)TestResultRow["terimal"] == "2;3")
            {
                rowM["A0"] = TestResultRow["result_pv3"];
                rowM["B0"] = TestResultRow["result_pv6"];  
                rowM["C0"] = TestResultRow["result_pv9"];
            }
            if ((string)TestResultRow["terimal"] == "3;1")
            {
                rowLow["A0"] = TestResultRow["result_pv3"];
                rowLow["B0"] = TestResultRow["result_pv6"];
                rowLow["C0"] = TestResultRow["result_pv9"];
            }
            if((string)TestResultRow["terimal"] == null)
            {
                rowHigh["A0"] = TestResultRow["result_pv3"];
                rowHigh["B0"] = TestResultRow["result_pv6"];
                rowHigh["C0"] = TestResultRow["result_pv9"];
                rowM["A0"] = TestResultRow["result_pv3"];
                rowM["B0"] = TestResultRow["result_pv6"];
                rowM["C0"] = TestResultRow["result_pv9"];
                rowLow["A0"] = TestResultRow["result_pv3"];
                rowLow["B0"] = TestResultRow["result_pv6"];
                rowLow["C0"] = TestResultRow["result_pv9"];
            }
            DataRow rowResult = WorkingSets.local.Casingtest_Commonbodyresults.NewRow();
            var ji = JobInformation.FromString((string)TestResultRow["waves"]);
            rowResult["TestCode"] = ji.GetHashCode();
            rowResult["Temperature"] = ji.temperature;
            rowResult["Humidity"] = ji.humidity;
            rowResult["OilTemperature"] = ji.oilTemperature;
            WorkingSets.local.Dcresistor_Highpressureresults.Rows.Add(rowResult);
            WorkingSets.local.Dcresistor_Lowpressureresults.Rows.Add(rowResult);
            WorkingSets.local.Dcresistor_Mediumvoltageresults.Rows.Add(rowResult);
            WorkingSets.local.Dcresistor_Highpressure.Rows.Add(rowHigh);
            WorkingSets.local.Dcresistor_Mediumvoltage.Rows.Add(rowM);
            WorkingSets.local.Dcresistor_Lowpressure.Rows.Add(rowLow);
            WorkingSets.local.SaveCreateLocateDatabase();
        }
        private static void DoOLTESwich(DataRow TestResultRow)
        {
            DataRow rowResult = WorkingSets.local.Tapchangertestresults.NewRow();
            DataRow OLTERow = WorkingSets.local.Tapchangertest.NewRow();
            OLTERow["SwitchingTime"] = TestResultRow["recordtime"];
            OLTERow["testchart"] = TestResultRow["waves"];
            var ji = JobInformation.FromString((string)TestResultRow["waves"]);
            if (ji.temperature != "100")
            {
                rowResult["TestCode"] = ji.GetHashCode();
                rowResult["Temperature"] = ji.temperature;
                rowResult["Humidity"] = ji.humidity;
                rowResult["OilTemperature"] = ji.oilTemperature;
            }
            WorkingSets.local.Tapchangertest.Rows.Add(OLTERow);
            WorkingSets.local.Tapchangertestresults.Rows.Add(rowResult);
            WorkingSets.local.SaveCreateLocateDatabase();
        }

    }
}
