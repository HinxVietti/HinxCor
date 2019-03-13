namespace HinxCor.IO
{
    /// <summary>
    /// interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBitConvertable<T>
    {
        /// <summary>
        /// data length
        /// </summary>
        int Length { get; }
        /// <summary>
        /// get data
        /// </summary>
        /// <returns></returns>
        byte[] GetByteArray();
        /// <summary>
        /// get object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        T ToObject(byte[] array);
        /// <summary>
        /// get object
        /// </summary>
        /// <returns></returns>
        T ToObject();

    }
}
