using SCEEC.Numerics.Quantities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCEEC.Numerics
{
    /// <summary>
    /// 单位转换器
    /// </summary>
    public static class QuantitiesConverter
    {
        /// <summary>
        /// 将文本转化为物理量单位
        /// </summary>
        /// <param name="symbol">代表物理量单位的文本</param>
        /// <returns>物理量单位</returns>
        public static Symbol String2Symbol(string symbol)
        {
            if (symbol.EndsWith("V")) return Symbol.V;
            if (symbol.EndsWith("A")) return Symbol.A;
            if (symbol.EndsWith("Ω")) return Symbol.Ω;
            if (symbol.EndsWith("C")) return Symbol.C;
            if (symbol.EndsWith("s")) return Symbol.s;
            if (symbol.EndsWith("W")) return Symbol.W;
            if (symbol.EndsWith("F")) return Symbol.F;
            if (symbol.EndsWith("Hz")) return Symbol.Hz;
            if (symbol.EndsWith("K")) return Symbol.K;
            if (symbol.EndsWith("℃")) return Symbol._C;
            if (symbol.EndsWith("m")) return Symbol.m;
            return Symbol.None;
        }
    }
}
