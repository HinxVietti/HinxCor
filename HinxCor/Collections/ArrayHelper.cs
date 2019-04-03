using System;
using System.Collections;
using System.Collections.Generic;

public static class ArrayHelper
{


    /// <summary>
    /// 返回满足condition的数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static T[] GetArrayWhere<T>(this T[] array, Func<T, bool> condition)
    {
        List<T> result = new List<T>();
        for (int i = 0; i < array.Length; i++)
            if (condition(array[i])) result.Add(array[i]);
        return result.ToArray();
    }

    /// <summary>
    /// 移除满足condition的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ts"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static List<T> RemoveWhere<T>(this List<T> ts, Func<T, bool> condition)
    {
        List<T> toremove = new List<T>();
        for (int i = 0; i < ts.Count; i++)
            if (condition(ts[i]))
                toremove.Add(ts[i]);

        for (int i = 0; i < toremove.Count; i++)
            ts.Remove(toremove[i]);
        return ts;
    }

    /// <summary>
    /// 移除那些不达标(满足condition)的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cs"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static ICollection<T> RemoveWhere<T>(this ICollection<T> cs, Func<T, bool> condition)
    {
        List<T> tdcs = new List<T>();
        foreach (var item in cs)
            if (condition(item)) tdcs.Add(item);

        for (int i = 0; i < tdcs.Count; i++)
            cs.Remove(tdcs[i]);
        return cs;
    }


}

