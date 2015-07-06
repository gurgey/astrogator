using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ModInt
{
    uint value;
    //public ModInt(float fraction)
    //{
    //    ushort meow = (ushort)fraction;
    ////    value = ushort.MaxValue / meow;
    //}
    public ModInt(uint v)
    {
        value = v;
    }
    public ModInt(float smallFloat, float maxFloat)
    {
        //float ratio = value / maxFloat;
        uint ibfb = uint.MaxValue / (uint)maxFloat;
        value = ibfb * (uint)smallFloat;
    }
    public float ToFloat(float maxFloat)
    {
        float k = (float)value * maxFloat;
        float j = (float)uint.MaxValue;
        return k / j;
    }


    static public ModInt operator +(ModInt t, ModInt other)
    {
        return new ModInt(t.value + other.value);
    }
    static public ModInt operator +(ModInt t, uint other)
    {
        return new ModInt(t.value + other);
    }
    static public ModInt operator -(ModInt t, ModInt other)
    {
        return new ModInt(t.value - other.value);
    }
    static public bool operator <(ModInt a, ModInt b)
    {
        return a.value < b.value;
    }
    static public bool operator <=(ModInt a, ModInt b)
    {
        return a.value <= b.value;
    }
    static public bool operator >(ModInt a, ModInt b)
    {
        return a.value > b.value;
    }
    static public bool operator >=(ModInt a, ModInt b)
    {
        return a.value >= b.value;
    }

}

