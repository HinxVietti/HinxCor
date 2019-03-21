namespace HinxCor.CryptoSer
{
    /// <summary>
    /// 字节工具包
    /// </summary>
    public interface IBytesUtil
    {
        /// <summary>
        /// string2byteArray
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        byte[] FromString(string text);

        /// <summary>
        /// base64-2-byteArray
        /// </summary>
        /// <param name="base64Text"></param>
        /// <returns></returns>
        byte[] FromBase64(string base64Text);

        /// <summary>
        /// ByteArray2String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string ToString(byte[] bytes);

        /// <summary>
        /// ByteArray2Base64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string ToBase64(byte[] bytes);

        /// <summary>
        /// byteArray2HexString
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string ToHex(byte[] bytes);

        /// <summary>
        /// combline byteArray
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        byte[] Combine(params byte[][] arrays);
    }
}