
using System;
using System.Text;
/// <summary>
/// 获取全局唯一ID；
/// 优点，简洁；
/// 缺点，依赖System，依赖时间戳，依赖Seeds
/// </summary>
public sealed class UidGenerator
{
    static Random rom;

    /// <summary>
    /// 获取一个小于16777215的整数
    /// </summary>
    /// <returns></returns>
    public static int GetRamId()
    {
        var t = DateTime.Now.ToFileTimeUtc();
        if (rom == null)
        {
            int seeds = (int)(t / 1 % 100);
            rom = new Random(seeds);
        }
        return rom.Next(0xffffff);
    }

    /// <summary>
    /// 获取一个指定长度的随机字符串
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRamString(int length)
    {
        var rrx = Convert.ToString(GetRamId());
        if (rrx.Length < length)
        {
            var t = DateTime.Now.ToFileTimeUtc();
            rrx = Convert.ToString(t, 16) + rrx;
        }
        if (rrx.Length < length)
            return rrx.Remove(0, rrx.Length - length);
        if (rrx.Length == length) return rrx;
        //return rrx;
        StringBuilder sb = new StringBuilder();
        int lr = length - rrx.Length;
        for (int i = 0; i < lr; i++)
            sb.Append("Q");
        sb.Append(rrx);
        return sb.ToString();
    }

    /// <summary>
    /// 设置随机种子
    /// </summary>
    /// <param name="seed"></param>
    public static void SetSeeds(int seed)
    {
        rom = new System.Random(seed);
    }

    /// <summary>
    /// 获取一个Id
    /// </summary>
    /// <returns></returns>
    public static string GetId()
    {
        return GetId("0000000000000000");
    }
    /// <summary>
    /// 指定机器识别码，获取Id
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static string GetId(string guid)
    {
        var t = System.DateTime.Now.ToFileTimeUtc();
        if (rom == null)
        {
            int seeds = (int)(t / 1 % 100);
            rom = new System.Random(seeds);
        }
        var r = rom.Next(0xFFFFF);
        var rrx = System.Convert.ToString(r, 16);
        return string.Format("{0}-{1}-{2}", guid.Remove(5), System.Convert.ToString(t, 16).Insert(10, "-").Insert(5, "-"), rrx);
    }

    /// <summary>
    /// 获取一个短Id
    /// </summary>
    /// <returns></returns>
    public static string GetShortId()
    {
        return GetShortId(12);
    }
    /// <summary>
    /// 指定机器识别码，获取一个短Id
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static string GetShortId(string guid)
    {
        return GetShortId(guid, 12);
    }
    /// <summary>
    /// 获取一个短Id,指定前面截去长度
    /// </summary>
    /// <returns></returns>
    public static string GetShortId(int subL)
    {
        return GetId().Substring(subL);
    }
    /// <summary>
    /// 指定机器识别码，获取一个短Id，,指定前面截去长度
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="subL"></param>
    /// <returns></returns>
    public static string GetShortId(string guid, int subL)
    {
        if (string.IsNullOrEmpty(guid)) guid = "0000000000000000000000000000000000";
        subL += 3;
        return guid.Remove(5) + "-" + GetId().Substring(subL);
    }

}
