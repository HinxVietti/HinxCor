using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace GDIPlus
{
    /// <summary>
    ///高速bitmap
    /// </summary>
    public class FastBitmap : IDisposable, ICloneable
    {
        private Bitmap bmp;
        internal BitmapData bmpData;

        private FastBitmap()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bitmap"></param>
        public FastBitmap(Bitmap bitmap)
        {
            this.bmp = bitmap;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="format"></param>
        public FastBitmap(int width, int height, PixelFormat format)
        {
            this.bmp = new Bitmap(width, height, format);
        }

        /// <summary>
        /// 克隆FastBitmap
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new FastBitmap { bmp = (Bitmap)bmp.Clone() };
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            this.Unlock();
            if (disposing)
            {
                this.bmp.Dispose();
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        ~FastBitmap()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// 获取强调
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public byte GetIntensity(int x, int y)
        {
            Color pixel = this.GetPixel(x, y);
            return (byte)((((pixel.R * 0.3) + (pixel.G * 0.59)) + (pixel.B * 0.11)) + 0.5);
        }

        /// <summary>
        /// 获取像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public unsafe Color GetPixel(int x, int y)
        {
            if (this.bmpData.PixelFormat == PixelFormat.Format32bppArgb)
            {
                byte* numPtr = (byte*)((((int)this.bmpData.Scan0) + (y * this.bmpData.Stride)) + (x * 4));
                return Color.FromArgb(numPtr[3], numPtr[2], numPtr[1], numPtr[0]);
            }
            if (this.bmpData.PixelFormat == PixelFormat.Format24bppRgb)
            {
                byte* numPtr2 = (byte*)((((int)this.bmpData.Scan0) + (y * this.bmpData.Stride)) + (x * 3));
                return Color.FromArgb(numPtr2[2], numPtr2[1], numPtr2[0]);
            }
            return Color.Empty;
        }

        /// <summary>
        /// 锁定内存(只有锁定到系统内存数据之后才能操作)
        /// </summary>
        public void Lock()
        {
            this.bmpData = this.bmp.LockBits(new Rectangle(0, 0, this.bmp.Width, this.bmp.Height), ImageLockMode.ReadWrite, this.bmp.PixelFormat);
        }

        /// <summary>
        /// 保存到本地文件
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            this.bmp.Save(filename);
        }

        /// <summary>
        /// 保存到本地文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void Save(string filename, ImageFormat format)
        {
            this.bmp.Save(filename, format);
        }

        /// <summary>
        /// 设置内存区域的颜色
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        public unsafe void SetPixel(int x, int y, Color c)
        {
            if (this.bmpData.PixelFormat == PixelFormat.Format32bppArgb)
            {
                byte* numPtr = (byte*)((((int)this.bmpData.Scan0) + (y * this.bmpData.Stride)) + (x * 4));
                numPtr[0] = c.B;
                numPtr[1] = c.G;
                numPtr[2] = c.R;
                numPtr[3] = c.A;
            }
            if (this.bmpData.PixelFormat == PixelFormat.Format24bppRgb)
            {
                var res = ((((int)this.bmpData.Scan0) + (y * this.bmpData.Stride)) + (x * 3));
                byte* numPtr2 = (byte*)res;
                numPtr2[0] = c.B;
                numPtr2[1] = c.G;
                numPtr2[2] = c.R;
            }
        }

        /// <summary>
        /// 接触锁定,释放BMPData
        /// </summary>
        public void Unlock()
        {
            if (this.bmpData != null)
            {
                this.bmp.UnlockBits(this.bmpData);
                this.bmpData = null;
            }
        }

        /// <summary>
        /// bmp get and set.
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return this.bmp;
            }
            set
            {
                if (value != null)
                {
                    this.bmp = value;
                }
            }
        }

        /// <summary>
        /// get height
        /// </summary>
        public int Height
        {
            get
            {
                return this.bmp.Height;
            }
        }

        /// <summary>
        /// get width
        /// </summary>
        public int Width
        {
            get
            {
                return this.bmp.Width;
            }
        }
    }
}

