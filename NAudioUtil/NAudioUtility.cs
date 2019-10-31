using NAudio.Wave;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 播放工具,可以做成单例
/// </summary>
public class NAudioUtility
{
    /// <summary>
    /// 播放音乐片段, 阻塞线程
    /// </summary>
    /// <param name="fileName"></param>
    public static void PlayAudio(string fileName)
    {
        FileInfo file = new FileInfo(fileName);
        if (file.Exists == false) return;
        using (var audioFile = new AudioFileReader(fileName))
        {
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                    Thread.Sleep(1000);
            }
        }
    }

    /// <summary>
    /// 新建会话, 播放音乐片段
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="onfinished"></param>
    public static void PlayAudioAsync(string fileName, Action onfinished = null)
    {
        Task.Run(() =>
        {
            PlayAudio(fileName);
            onfinished?.Invoke();
        });
    }


    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="outputDevice"></param>
    public static void PlayAudio(string fileName, WaveOutEvent outputDevice)
    {
        FileInfo file = new FileInfo(fileName);
        if (file.Exists == false) return;
        using (var audioFile = new AudioFileReader(fileName))
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
                Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="outputDevice"></param>
    /// <param name="onfinished"></param>
    public static void PlayAudioAsync(string fileName, WaveOutEvent outputDevice, Action onfinished = null)
    {
        Task.Run(() =>
        {
            PlayAudio(fileName, outputDevice);
            onfinished?.Invoke();
        });
    }

    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="ms"></param>
    /// <param name="onfinished"></param>
    public static void PlayAudioStreamAsync(Stream ms, Action onfinished = null)
    {
        Task.Run(() =>
        {
            PlayAudioStream(ms);
            onfinished?.Invoke();
        });
    }

    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="ms"></param>
    public static void PlayAudioStream(Stream ms)
    {
        ms.Position = 0;
        using (WaveStream blockAlignedStream =
            new BlockAlignReductionStream(
                WaveFormatConversionStream.CreatePcmStream(
                    //new WaveFileReader(ms))))
                    new Mp3FileReader(ms))))
        {
            using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
            {
                waveOut.Init(blockAlignedStream);
                waveOut.Play();
                waveOut.Volume = 0.2f;
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }

    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="ms"></param>
    /// <param name="waveOut"></param>
    /// <param name="onfinished"></param>
    public static void PlayAudioStreamAsync(Stream ms, WaveOutEvent waveOut, Action onfinished = null)
    {
        Task.Run(() =>
        {
            PlayAudioStream(ms, waveOut);
            onfinished?.Invoke();
        });
    }

    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="ms"></param>
    /// <param name="waveOut"></param>
    public static void PlayAudioStream(Stream ms, WaveOutEvent waveOut)
    {
        ms.Position = 0;
        using (WaveStream blockAlignedStream = GetWaveStream(ms))
        {
            waveOut.Init(blockAlignedStream);
            waveOut.Play();
            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        }
    }


    /// <summary>
    /// Creates a new BlockAlignReductionStream
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static WaveStream GetWaveStream(Stream stream)
    {
        return new BlockAlignReductionStream(
              WaveFormatConversionStream.CreatePcmStream(
                  new Mp3FileReader(stream)));
    }

}

