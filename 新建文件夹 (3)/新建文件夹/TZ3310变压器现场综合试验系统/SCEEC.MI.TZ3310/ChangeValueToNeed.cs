using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public static class ChangeValueToNeed
    {
        public static string DcResistans_To_75(Numerics.PhysicalVariable Value)
        {
            double TempValue = Convert.ToDouble(Value.value * 310 / 255);
            return Numerics.NumericsConverter.Value2Text(TempValue, 2, -23, " ", "F", false, false);
        }

        public static string UnBalance(double a, double b, double c)
        {
            return ((((b - c) / ((a + b + c) / 3))) * 100).ToString("F2") + "%";
        }

        public static string MutualDifference(double a, double b)
        {
            return (((a - b) / Math.Min(a, b)) * 100.0).ToString("F2") + "%";
        }

        public static string MaxMutualDifference(double a, double b, double c)
        {
            double md = (a - b) / Math.Min(a, b);
            md = Math.Max(md, (b - c) / Math.Min(b, c));
            md = Math.Max(md, (c - a) / Math.Min(c, a));
            return (md * 100).ToString("F2") + "%";
        }

        

    }
}
