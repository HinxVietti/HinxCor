namespace HinxCor.CryptoSer
{
    /// <summary>
    /// 盐析
    /// </summary>
    public class SaltSetting
    {
        /// <summary>
        /// 头部大小 --3
        /// </summary>
        public static int HeadSize
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// 尾部大小 --3
        /// </summary>
        public static int TailSize
        {
            get
            {
                return 5;
            }
        }
    }
}