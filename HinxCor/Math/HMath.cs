using System;
using System.Collections.Generic;

namespace HinxCor
{
    /// <summary>
    /// 数学方法帮助类
    /// </summary>
    public class HMath
    {
        /// <summary>
        /// 范围规范数据
        /// </summary>
        /// <param name="value">传入数据</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>大于等于最大值,小于等于最小值的数值</returns>
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
        /// <summary>
        /// 范围规范数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns>返回一个≥0,≤1的数值</returns>
        public static float Clamp01(float value)
        {
            return Clamp(value, 0, 1);
        }
        /// <summary>
        /// 范围规范数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns>返回一个≥0,≤1的数值</returns>
        public static int Clamp01(int value)
        {
            return Clamp(value, 0, 1);
        }
        /// <summary>
        /// 范围规范数据
        /// </summary>
        /// <param name="value">传入数据</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>大于等于最大值,小于等于最小值的数值</returns>
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

