using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public static class NAudioUtility
{

    public static void PlayMusic(string fileName)
    {
        FileInfo file = new FileInfo(fileName);
        if (file.Exists == false) return;
        using (var audioFile = new AudioFileReader(fileName))
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
                Thread.Sleep(1000);
        }

    }

}

