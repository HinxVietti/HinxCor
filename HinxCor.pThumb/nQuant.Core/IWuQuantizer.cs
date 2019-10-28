using System.Drawing;

namespace nQuant
{
    /// <summary>
    /// interface
    /// </summary>
    public interface IWuQuantizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alphaThreshold"></param>
        /// <param name="alphaFader"></param>
        /// <returns></returns>
        Image QuantizeImage(Bitmap image, int alphaThreshold, int alphaFader);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alphaThreshold"></param>
        /// <param name="alphaFader"></param>
        /// <param name="maxColors"></param>
        /// <returns></returns>
        Image QuantizeImage(Bitmap image, int alphaThreshold, int alphaFader, int maxColors);
    }
}