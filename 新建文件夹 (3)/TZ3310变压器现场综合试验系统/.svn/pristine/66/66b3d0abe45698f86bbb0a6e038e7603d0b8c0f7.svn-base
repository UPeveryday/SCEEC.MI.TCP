using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCEEC.MI.TZ3310;
using System.Data;
namespace SCEEC.TTM
{ 
    public static class Measurement
    {
        public static void DoWork(ref TestingWorkerSender sender)
        {
            
            MeasurementInterface.DoWork(ref sender);
            if ((sender.MeasurementItems[sender.CurrentItemIndex].completed) && (!sender.MeasurementItems[sender.CurrentItemIndex].failed))
            {
                //WorkingSets.local.refreshTestResults();
                int ft = (int)sender.MeasurementItems[sender.CurrentItemIndex].Function;
                if ((ft > 0) && (ft < 10))
                    WorkingSets.local.TestResults.Rows.Add(sender.MeasurementItems[sender.CurrentItemIndex].ToDataRow(sender.job));
                WorkingSets.local.saveTestResults();
                sender.CurrentItemIndex++;
            }

        }
        public static bool CancelWork(ref TestingWorkerSender sender)
        {
            return MeasurementInterface.CancelWork(ref sender);
        }
    }
}
