using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{

    public enum HeadOrTail { Head,Tail}
    public static class NumCorrection
    {
        /// <summary>
        /// 从十进制转换到十六进制
        /// </summary>
        /// <param name="ten"></param>
        /// <returns></returns>
        public static string Ten2Hex(string ten)
        {
            ulong tenValue = Convert.ToUInt64(ten);
            ulong divValue, resValue;
            string hex = "";
            do
            {
                //divValue = (ulong)Math.Floor(tenValue / 16);

                divValue = (ulong)Math.Floor((decimal)(tenValue / 16));

                resValue = tenValue % 16;
                hex = tenValue2Char(resValue) + hex;
                tenValue = divValue;
            }
            while (tenValue >= 16);
            if (tenValue != 0)
                hex = tenValue2Char(tenValue) + hex;
            return hex;
        }

        private static string tenValue2Char(ulong ten)
        {
            switch (ten)
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
                    return ten.ToString();
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return "";
            }
        }

        public static string KeepNum(string NumStr, int NeedNum,HeadOrTail headOrTail )
        {
            if (NumStr != null)
            {
                if (NumStr.Length < NeedNum)
                {
                    int NeedZero = NeedNum - NumStr.Length;
                    string Temp = "0";
                    for (int i = 0; i < NeedZero - 1; i++)
                    {
                        Temp += "0";
                    }
                    if (headOrTail == HeadOrTail.Head)
                        return Temp + NumStr;
                    else
                        return NumStr + Temp;
                }
                else if (NumStr.Length == NeedNum)
                {
                    return NumStr;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public static string KeepNumSpace(string NumStr, int NeedNum, HeadOrTail headOrTail)
        {
            if (NumStr != null)
            {
                if (NumStr.Length < NeedNum)
                {
                    int NeedZero = NeedNum - NumStr.Length;
                    string Temp = " ";
                    for (int i = 0; i < NeedZero - 1; i++)
                    {
                        Temp += " ";
                    }
                    if (headOrTail == HeadOrTail.Head)
                        return Temp + NumStr;
                    else
                        return NumStr + Temp;
                }
                else if (NumStr.Length == NeedNum)
                {
                    return NumStr;
                }
                else
                {
                    throw new Exception("变压器型号大于10位");
                }
            }

            return null;
        }
        /// <summary>
        /// 获取string数据两个字符转化为int
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static int GetNumForstring(string temp)
        {
            if(temp.Length>2)
            {
                char[] buffer = temp.ToArray();
                string a = buffer[0].ToString() + buffer[1].ToString();
                return Convert.ToInt32(a);
            }
            return -1;
        }

        /// <summary>
        /// 把1-9999代表0-99.99%,保留4位
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string DoubleToHexNum(double Num)
        {
            int Inum =(int)( Num * 10000);
            if (Inum > 0 && Inum < 9999)
                return KeepNum(Ten2Hex(Inum.ToString()), 4, HeadOrTail.Head);
            else
            {
                return "0064";
            }
        }


    }
}
