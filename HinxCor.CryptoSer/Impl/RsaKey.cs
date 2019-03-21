namespace HinxCor.CryptoSer
{
    /// <summary>
    /// RSA
    /// </summary>
    public class RsaKey
    {
        /// <summary>
        /// 私有部分 --内部解析内容
        /// </summary>
        public string Private { get; set; }

        /// <summary>
        /// 共有部分 --发布出去的内容
        /// </summary>
        public string Public { get; set; }
    }
}