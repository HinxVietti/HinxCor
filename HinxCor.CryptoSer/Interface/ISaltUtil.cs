﻿namespace HinxCor.CryptoSer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISaltUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        byte[] GenerateSalt(int size); 
    }
}