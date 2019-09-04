using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public static class GetParameter
    {
        // static JobList job = new JobList();

        //TestingWorkerSender worker = new TestingWorkerSender();

        public static Parameter.JYDZVoilt GetPraDCInsulationVoltage(JobList job)
        {

            int DCInsulationVoltage = job.Parameter.DCInsulationVoltage / 100;
            switch (DCInsulationVoltage)
            {
                case 2: return Parameter.JYDZVoilt._0_25KV;
                case 5: return Parameter.JYDZVoilt._0_5KV;
                case 6: return Parameter.JYDZVoilt._0_6KV;
                case 7: return Parameter.JYDZVoilt._0_7KV;
                case 8: return Parameter.JYDZVoilt._0_8KV;
                case 9: return Parameter.JYDZVoilt._0_9KV;
                case 10: return Parameter.JYDZVoilt._1_0KV;
                case 15: return Parameter.JYDZVoilt._1_5KV;
                case 20: return Parameter.JYDZVoilt._2_0KV;
                case 25: return Parameter.JYDZVoilt._2_5KV;
                case 30: return Parameter.JYDZVoilt._3_0KV;
                case 35: return Parameter.JYDZVoilt._3_5KV;
                case 40: return Parameter.JYDZVoilt._4_0KV;
                case 45: return Parameter.JYDZVoilt._4_5KV;
                case 50: return Parameter.JYDZVoilt._5_0KV;
                case 55: return Parameter.JYDZVoilt._5_5KV;
                case 60: return Parameter.JYDZVoilt._6_0KV;
                case 65: return Parameter.JYDZVoilt._6_5KV;
                case 70: return Parameter.JYDZVoilt._7_0KV;
                case 75: return Parameter.JYDZVoilt._7_5KV;
                case 80: return Parameter.JYDZVoilt._8_0KV;
                case 85: return Parameter.JYDZVoilt._8_5KV;
                case 90: return Parameter.JYDZVoilt._9_0KV;
                case 95: return Parameter.JYDZVoilt._9_5KV;
                case 100: return Parameter.JYDZVoilt._10_0KV;
                default: return Parameter.JYDZVoilt._5_0KV;
            }

        }

        public static double GetPraDCInsulationResistance(JobList job)
        {
            double DCInsulationResistance = job.Parameter.DCInsulationResistance;
            if (DCInsulationResistance != 0) return DCInsulationResistance;
            else return DCInsulationResistance = 5;
        }

        public static double GetPraDCInsulationAbsorptionRatio(JobList job)
        {
            double DCInsulationAbsorptionRatio = job.Parameter.DCInsulationAbsorptionRatio * 100;
            if (DCInsulationAbsorptionRatio != 0) return DCInsulationAbsorptionRatio;
            else return DCInsulationAbsorptionRatio = 130;
        }

        public static Parameter.JSVoilt GetPraCapacitanceVoltage(JobList job)
        {
            int CapacitanceVoltage = job.Parameter.CapacitanceVoltage / 100;
            switch (CapacitanceVoltage)
            {
                case 5: return Parameter.JSVoilt._0_5KV;
                case 6: return Parameter.JSVoilt._0_6KV;
                case 7: return Parameter.JSVoilt._0_7KV;
                case 8: return Parameter.JSVoilt._0_8KV;
                case 9: return Parameter.JSVoilt._0_9KV;
                case 10: return Parameter.JSVoilt._1_0KV;
                case 15: return Parameter.JSVoilt._1_5KV;
                case 20: return Parameter.JSVoilt._2_0KV;
                case 25: return Parameter.JSVoilt._2_5KV;
                case 30: return Parameter.JSVoilt._3_0KV;
                case 35: return Parameter.JSVoilt._3_5KV;
                case 40: return Parameter.JSVoilt._4_0KV;
                case 45: return Parameter.JSVoilt._4_5KV;
                case 50: return Parameter.JSVoilt._5_0KV;
                case 55: return Parameter.JSVoilt._5_5KV;
                case 60: return Parameter.JSVoilt._6_0KV;
                case 65: return Parameter.JSVoilt._6_5KV;
                case 70: return Parameter.JSVoilt._7_0KV;
                case 75: return Parameter.JSVoilt._7_5KV;
                case 80: return Parameter.JSVoilt._8_0KV;
                case 85: return Parameter.JSVoilt._8_5KV;
                case 90: return Parameter.JSVoilt._9_0KV;
                case 95: return Parameter.JSVoilt._9_5KV;
                case 100: return Parameter.JSVoilt._10_0KV;
                default: return Parameter.JSVoilt._5_0KV;
            }

        }

        public static Parameter.ZldzCurrent GetPraDCResistanceCurrent(JobList job)
        {
            int DCResistanceCurrent = job.Parameter.DCResistanceCurrent;
            switch (DCResistanceCurrent)
            {
                case 1: return Parameter.ZldzCurrent._1A;
                case 3: return Parameter.ZldzCurrent._3A;
                case 10: return Parameter.ZldzCurrent._10A;
                default: return Parameter.ZldzCurrent._1A;
            }

        }

        public static string GetFreQuency(Parameter.JSFrequency jSFrequency)
        {
            if (jSFrequency == Parameter.JSFrequency._45HZ) return "45Hz";
            if (jSFrequency == Parameter.JSFrequency._45To_55HZ) return "50Hz";
            if (jSFrequency == Parameter.JSFrequency._46HZ) return "46Hz";
            if (jSFrequency == Parameter.JSFrequency._47HZ) return "47Hz";
            if (jSFrequency == Parameter.JSFrequency._48HZ) return "48Hz";
            if (jSFrequency == Parameter.JSFrequency._49HZ) return "49Hz";
            if (jSFrequency == Parameter.JSFrequency._49To_51HZ) return "50Hz";
            if (jSFrequency == Parameter.JSFrequency._50HZ) return "50Hz";
            if (jSFrequency == Parameter.JSFrequency._51HZ) return "51Hz";
            if (jSFrequency == Parameter.JSFrequency._52HZ) return "52Hz";
            if (jSFrequency == Parameter.JSFrequency._53HZ) return "53Hz";
            if (jSFrequency == Parameter.JSFrequency._54HZ) return "54Hz";
            if (jSFrequency == Parameter.JSFrequency._55HZ) return "55Hz";
            if (jSFrequency == Parameter.JSFrequency._55To_65HZ) return "60Hz";
            if (jSFrequency == Parameter.JSFrequency._56HZ) return "56Hz";
            if (jSFrequency == Parameter.JSFrequency._57HZ) return "57Hz";
            if (jSFrequency == Parameter.JSFrequency._58HZ) return "58Hz";
            if (jSFrequency == Parameter.JSFrequency._59HZ) return "59Hz";
            if (jSFrequency == Parameter.JSFrequency._59To_61HZ) return "60Hz";
            if (jSFrequency == Parameter.JSFrequency._60HZ) return "60Hz";
            if (jSFrequency == Parameter.JSFrequency._61HZ) return "61Hz";
            if (jSFrequency == Parameter.JSFrequency._62HZ) return "62Hz";
            if (jSFrequency == Parameter.JSFrequency._63HZ) return "63Hz";
            if (jSFrequency == Parameter.JSFrequency._64HZ) return "64Hz";
            if (jSFrequency == Parameter.JSFrequency._65HZ) return "65Hz";

            return "55Hz"; 

        }


        public static string GetPraDCInsulationVoltageNum(JobList job)
        {

            int DCInsulationVoltage = job.Parameter.DCInsulationVoltage / 100;
            switch (DCInsulationVoltage)
            {
                case 2: return "00";
                case 5: return "01";
                case 6: return "02";
                case 7: return "03";
                case 8: return "04";
                case 9: return "05";
                case 10: return "06";
                case 15: return "07";
                case 20: return "08";
                case 25: return "09";
                case 30: return "0A";
                case 35: return "0B";
                case 40: return "0C";
                case 45: return "0D";
                case 50: return "0E";
                case 55: return "0F";
                case 60: return "10";
                case 65: return "11";
                case 70: return "12";
                case 75: return "13";
                case 80: return "14";
                case 85: return "15";
                case 90: return "16";
                case 95: return "17";
                case 100: return "18";
                default: return "0E";
            }

        }

        public static string GetPraCapacitanceVoltageNum(JobList job)
        {
            int CapacitanceVoltage = job.Parameter.CapacitanceVoltage / 100;
            switch (CapacitanceVoltage)
            {
                case 5: return "00";
                case 6: return "01";
                case 7: return "02";
                case 8: return "03";
                case 9: return "04";
                case 10: return "05";
                case 15: return "06";
                case 20: return "07";
                case 25: return "08";
                case 30: return "09";
                case 35: return "0A";
                case 40: return "0B";
                case 45: return "0C";
                case 50: return "0D";
                case 55: return "0E";
                case 60: return "0F";
                case 65: return "10";
                case 70: return "11";
                case 75: return "12";
                case 80: return "13";
                case 85: return "14";
                case 90: return "15";
                case 95: return "16";
                case 100: return "17";
                default: return "0D";
            }

        }
    }
}
