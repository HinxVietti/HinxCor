using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// 
/// </summary>
public static class RenderingHelper
{
    /// <summary>
    /// 快速复制像素;使用请确保裁剪空间大于等于目标大小
    /// </summary>
    /// <param name="origin">图像元</param>
    /// <param name="destinate">接受目标</param>
    /// <param name="format">扫描的方式</param>
    /// <param name="startpoint">起始点(从x,y像素开始复制)</param>
    public static unsafe void FastCloneBitmapTo(this Bitmap origin, Bitmap destinate, PixelFormat format, Point startpoint)
    {
        var nsize = startpoint + destinate.Size;
        if (origin.Width < nsize.X) throw new System.ArgumentOutOfRangeException(" Clip args out of range..");
        if (origin.Height < nsize.Y) throw new System.ArgumentOutOfRangeException(" Clip args out of range..");

        var originlock = origin.LockBits(new Rectangle(startpoint, destinate.Size), ImageLockMode.ReadWrite, format);
        var destlock = destinate.LockBits(new Rectangle(0, 0, destinate.Width, destinate.Height), ImageLockMode.ReadWrite, format);

        byte* _origin = (byte*)originlock.Scan0;
        byte* _dest = (byte*)destlock.Scan0;
        int pixedCount = destlock.Stride * destinate.Height;
        for (int i = 0; i < pixedCount; i++)
            _dest[i] = _origin[i];

        destinate.UnlockBits(destlock);
        origin.UnlockBits(originlock);
    }
}

