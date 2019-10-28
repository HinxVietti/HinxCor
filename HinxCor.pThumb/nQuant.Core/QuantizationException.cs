namespace nQuant
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class QuantizationException : System.ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public QuantizationException(string message) : base(message) { }
    }
}
