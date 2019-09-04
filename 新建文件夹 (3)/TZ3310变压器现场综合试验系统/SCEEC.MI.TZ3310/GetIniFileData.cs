using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{

    public  struct WindingDiffer
    {
        public int windingtype;//高中低
        public int windingconfig;//YN
        public string Termal;//YN
    }

    public struct Capdiffer
    {
        public int Function;//
        public int windingtype;//高中低
        public int windingconfig;//YN
        public string Termal;//0abc
    }

    public struct dcidiffer
    {
        public int Function;//14
        public int windingtype;//高中低
        public int windingconfig;//YN
        public string Termal;//0abc
    }

    public struct Oltcdiffer
    {
        public int Function;//14
        public int windingtype;//高中低
        public int windingconfig;//YN
        public string TapLabel;//0abc
    }
    public static class GetIniFileData
    {
        public static Capdiffer GetCapNum(CaptanceUpandata cap)
        {
            Capdiffer capdiffer = new Capdiffer();
            if (cap.Position == "高压绕组")
            {
                capdiffer.windingconfig = 0;
                capdiffer.Function = 2;
                capdiffer.windingtype = 0;
                capdiffer.Termal = null;
                return capdiffer;
            }
            if (cap.Position == "中压绕组")
            {
                capdiffer.windingconfig = 0;
                capdiffer.Function = 2;
                capdiffer.windingtype = 1;
                capdiffer.Termal = null;
                return capdiffer;
            }
            if (cap.Position == "低压绕组")
            {
                capdiffer.windingconfig = 0;
                capdiffer.Function = 2;
                capdiffer.windingtype = 2;
                capdiffer.Termal = null;
                return capdiffer;
            }
            if (cap.Position == "高压套管A")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 0;//高中低
                capdiffer.Termal = "1";//OABC
                return capdiffer;
            }
            if (cap.Position == "高压绕组B")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 0;//高中低
                capdiffer.Termal = "2";//OABC
                return capdiffer;
            }
            if (cap.Position == "高压绕组C")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 0;//高中低
                capdiffer.Termal = "3";//OABC
                return capdiffer;
            }
            if (cap.Position == "高压绕组0")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 0;//高中低
                capdiffer.Termal = "0";//OABC
                return capdiffer;
            }
            if (cap.Position == "中压套管A")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 1;//高中低
                capdiffer.Termal = "1";//OABC
                return capdiffer;
            }
            if (cap.Position == "中压绕组B")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 1;//高中低
                capdiffer.Termal = "2";//OABC
                return capdiffer;
            }
            if (cap.Position == "中压绕组C")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 1;//高中低
                capdiffer.Termal = "3";//OABC
                return capdiffer;
            }
            if (cap.Position == "中压绕组0")
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 5;//函数
                capdiffer.windingtype = 1;//高中低
                capdiffer.Termal = "0";//OABC
                return capdiffer;
            }
            else
            {
                capdiffer.windingconfig = 0;//Yn
                capdiffer.Function = 2;//函数
                capdiffer.windingtype = 1;//高中低
                capdiffer.Termal = "0";//OABC
                return capdiffer;

            }//默认

        }
        public static dcidiffer GetDciNum(DcInlutionUpandata dci)
        {
            dcidiffer dcidiffer = new dcidiffer();
            dcidiffer.windingconfig = 0;
            dcidiffer.Function = 1;
            dcidiffer.windingtype = 0;
            dcidiffer.Termal = null;
            if (dci.Position == "高压绕组")
            {
                dcidiffer.windingconfig = 0;
                dcidiffer.Function = 1;
                dcidiffer.windingtype = 0;
                dcidiffer.Termal = null;
                return dcidiffer;
            }
            if (dci.Position == "中压绕组")
            {
                dcidiffer.windingconfig = 0;
                dcidiffer.Function = 1;
                dcidiffer.windingtype = 1;
                dcidiffer.Termal = null;
                return dcidiffer;
            }
            if (dci.Position == "低压绕组")
            {
                dcidiffer.windingconfig = 0;
                dcidiffer.Function = 1;
                dcidiffer.windingtype = 2;
                dcidiffer.Termal = null;
                return dcidiffer;
            }
            if (dci.Position == "高压套管A")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 0;//高中低
                dcidiffer.Termal = "1";//OABC
                return dcidiffer;
            }
            if (dci.Position == "高压绕组B")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 0;//高中低
                dcidiffer.Termal = "2";//OABC
                return dcidiffer;
            }
            if (dci.Position == "高压绕组C")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 0;//高中低
                dcidiffer.Termal = "3";//OABC
                return dcidiffer;
            }
            if (dci.Position == "高压绕组0")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 0;//高中低
                dcidiffer.Termal = "0";//OABC
                return dcidiffer;
            }
            if (dci.Position == "中压套管A")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 1;//高中低
                dcidiffer.Termal = "1";//OABC
                return dcidiffer;
            }
            if (dci.Position == "中压绕组B")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 1;//高中低
                dcidiffer.Termal = "2";//OABC
                return dcidiffer;
            }
            if (dci.Position == "中压绕组C")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 1;//高中低
                dcidiffer.Termal = "3";//OABC
                return dcidiffer;
            }
            if (dci.Position == "中压绕组0")
            {
                dcidiffer.windingconfig = 0;//Yn
                dcidiffer.Function = 4;//函数
                dcidiffer.windingtype = 1;//高中低
                dcidiffer.Termal = "0";//OABC
                return dcidiffer;
            }

            return dcidiffer;
        }
        public static WindingDiffer GetResPositionTerimal(DcresistaceUpandata res)
        {
            WindingDiffer wd = new WindingDiffer();
            wd.windingtype = 0;
            wd.windingconfig = 0;
            wd.Termal = null;
            if (res.Position == "高压全部")
            { 
                wd.windingtype = 0;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = null;
                return wd;
            }
            if (res.Position == "高压AB_A")
            {
                wd.windingtype = 0;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "1;2";
                return wd;
            }
            if (res.Position == "高压BC_B")
            {
                wd.windingtype = 0;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "2;3";
                return wd;
            }
            if (res.Position == "高压CA_C")
            {
                wd.windingtype = 0;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "3;1";
                return wd;
            }
            if (res.Position == "中压全部")
            {
                wd.windingtype = 1;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = null;
                return wd;
            }
            if (res.Position == "中压AB_A")
            {
                wd.windingtype = 1;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "1;2";
                return wd;
            }
            if (res.Position == "中压BC_B")
            {
                wd.windingtype = 1;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "2;3";
                return wd;
            }
            if (res.Position == "中压CA_C")
            {
                wd.windingtype = 1;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "3;1";
                return wd;
            }
            if (res.Position == "低压全部")
            {
                wd.windingtype = 2;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = null;
                return wd;
            }
            if (res.Position == "低压AB_A")
            {
                wd.windingtype = 2;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "1;2";
                return wd;
            }
            if (res.Position == "低压BC_B")
            {
                wd.windingtype = 2;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "2;3";
                return wd;
            }
            if (res.Position == "低压CA_C")
            {
                wd.windingtype = 2;
                wd.windingconfig = GetResWindkind(res);
                wd.Termal = "3;1";
                return wd;
            }

            return wd;
        }

        public static Oltcdiffer GetOltcNum(OltcUpandata oltc)
        {
            Oltcdiffer od = new Oltcdiffer();
            od.Function = 6;
            od.windingconfig = GetWindkind(oltc.Windkind);
            od.windingtype = 0;
            od.TapLabel = oltc.windposition;
            return od;

        }
        public static int GetResWindkind(DcresistaceUpandata res)
        {
            if (res.Windingconfige == "Yn型")
                return 0;
            if (res.Windingconfige == "Y型")
                return 1;
            if (res.Windingconfige == "D型")
                return 2;
            if (res.Windingconfige == "Zn型")
                return 3;
            return -1;
        }

        public static int GetWindkind(string Windingconfige)
        {
            if (Windingconfige == "Yn型")
                return 0; 
            if (Windingconfige == "Y型")
                return 1;
            if (Windingconfige == "D型")
                return 2;
            if (Windingconfige == "Zn型")
                return 3;
            return -1;
        }
        public static int GetOltcPosition(string Windingconfige)
        {
            if (Windingconfige == "高压侧")
                return 0;
            if (Windingconfige == "中压侧")
                return 1;
            if (Windingconfige == "低压侧")
                return 2;
            return 0;
        }
    }
}
