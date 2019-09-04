using System;
using System.Collections.Generic;
using System.Text;

namespace SCEEC.UNSAFE
{
    public class Unsafe
    {

        public  float ToFloat(byte[] data)
        {
            float a = 0;
            byte i;
            byte[] x = data;
            unsafe
            {
                void* pf;
                fixed (byte* px = x)
                {
                    pf = &a;
                    for (i = 0; i < data.Length; i++)
                    {
                        *((byte*)pf + i) = *(px + i);
                    }
                }
            }


            return a;
        }

        public  byte[] ToByte(float data)
        {
            unsafe
            {
                byte* pdata = (byte*)&data;
                byte[] byteArray = new byte[sizeof(float)];
                for (int i = 0; i < sizeof(float); ++i)
                    byteArray[i] = *pdata++;

                return byteArray;
            }
        }
    }
}
