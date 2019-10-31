using NAudio.Wave;
using System;
using System.Collections.Generic;

/// <summary>
/// Obsolete
/// </summary>
public class AudioStream : WaveStream
{
    /// <summary>
    /// 
    /// </summary>
    public override WaveFormat WaveFormat => null;

    /// <summary>
    /// 
    /// </summary>
    public override long Length => throw new NotImplementedException();
    /// <summary>
    /// 
    /// </summary>
    public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }
}

