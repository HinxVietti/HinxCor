using nQuant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

public static class PNGCompression
{
    private static WuQuantizer singleton;
    public static WuQuantizer quantizer => singleton == null ? singleton = new WuQuantizer() : singleton;

    public static Bitmap CompressBitmap(Bitmap bmp)
    {
        quantizer.QuantizeImage(bmp);
        return bmp;
    }

    public async static Task<byte[]> CompressPNG(Stream s)
    {
        return await Task.Run(() =>
        {
            var img = quantizer.QuantizeImage((Bitmap)Image.FromStream(s));
            var ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            return ms.ToArray();
        });
    }

    public static Task<byte[]> CompressPNG(byte[] bmpData)
    {
        var ms = new MemoryStream(bmpData);
        return CompressPNG(ms);
    }



}

