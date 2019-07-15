using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThumbnailSharp;

namespace _demo_thumbnailcsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenFileDialog openf = new OpenFileDialog();
            openf.Filter = "|*.jpg;*.png";
            openf.ShowDialog();
            Image img = Image.FromFile(openf.FileName);


        }

        private async void workImageURL(string url)
        {
            byte[] resultBytes = await new ThumbnailCreator().CreateThumbnailBytesAsync(
                thumbnailSize: 250,
                urlAddress: new Uri("http://www.sample-image.com/image.jpg", UriKind.Absolute),
                imageFormat: Format.Jpeg);
            // or
            Stream resultStream = await new ThumbnailCreator().CreateThumbnailStreamAsync(
                thumbnailSize: 250,
                urlAddress: new Uri("http://www.sample-image.com/image.png", UriKind.Absolute),
                imageFormat: Format.Png);
        }

        private void workImageLocal(string fileName)
        {
            byte[] resultBytes = new ThumbnailCreator().CreateThumbnailBytes(
                thumbnailSize: 300,
                imageFileLocation: @"C:\images\image.bmp",
                imageFormat: Format.Bmp);
            //or
            Stream resultStream = new ThumbnailCreator().CreateThumbnailStream(
                thumbnailSize: 300,
                imageFileLocation: @"C:\images\image.bmp",
                imageFormat: Format.Bmp);

        }

        private void workImageStream(string fileName)
        {
            byte[] resultBytes = new ThumbnailCreator().CreateThumbnailBytes(
                thumbnailSize: 300,
                imageStream: new FileStream(@"C:\images\image.jpg", FileMode.Open, FileAccess.ReadWrite),
                imageFormat: Format.Jpeg);
            //or
            Stream resultStream = new ThumbnailCreator().CreateThumbnailStream(
                thumbnailSize: 300,
                imageStream: new FileStream(@"C:\images\image.jpg", FileMode.Open, FileAccess.ReadWrite),
                imageFormat: Format.Jpeg);
        }

        private void workImageStream(byte[] buffer)
        {
            //byte[] buffer = GetImageBytes(); //this is just fictitious method to get image data in bytes

            byte[] resultBytes = new ThumbnailCreator().CreateThumbnailBytes(
                thumbnailSize: 300,
                imageBytes: buffer,
                imageFormat: Format.Gif
            );
            //or
            Stream resultStream = new ThumbnailCreator().CreateThumbnailStream(
                thumbnailSize: 300,
                imageBytes: buffer,
                imageFormat: Format.Tiff
            );

        }
    }
}
