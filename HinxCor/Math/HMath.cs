using System;
using System.Collections.Generic;

namespace HinxCor
{

    public class HMath
    {
        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                min = min + max;
                max = min - max;
                min = min - max;
            }
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static float Clamp01(float value)
        {
            return Clamp(value, 0, 1);
        }
        public static int Clamp01(int value)
        {
            return Clamp(value, 0, 1);
        }
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
            {
                min = min + max;
                max = min - max;
                min = min - max;
            }
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
    }
}

