using System;
using System.Collections.Generic;
using System.Text;

namespace SCEEC.Numerics.Quantities
{
    /// <summary>
    /// 物理量单位名称
    /// </summary>
    public enum Unit
    {
        None = 0,
        Volt = 1,
        Ampere = 2,
        Ohm = 3,
        Coulomb = 4,
        Second = 5,
        Watt = 6,
        Farads = 7,
        CyclesPerSecond = 8,
        Kelvin = 9,
        degreeCentigrade = 10,
        Meter = 11
    }

    /// <summary>
    /// 物理量单位符号
    /// </summary>
    public enum Symbol
    {
        None = 0,
        V = 1,
        A = 2,
        Ω = 3,
        C = 4,
        s = 5,
        W = 6,
        F = 7,
        Hz = 8,
        K = 9,
        _C = 10,
        m = 11
    }

    /// <summary>
    /// 物理量类别名称
    /// </summary>
    public enum QuantityName
    {
        None = 0,
        Voltage = 1,
        Current = 2,
        Resistance = 3,
        Charge = 4,
        Time = 5,
        Power = 6,
        Capacitance = 7,
        Frequency = 8,
        Temperature = 9,
        CentigradeTemperature = 10,
        Length = 11
    }

}
