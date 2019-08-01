using System;
using System.Collections.Generic;

namespace HinxCor.VectorTime
{

    public class TimeLine
    {
        public IFrame this[int frameIndex]
        {
            get => Frames.ContainsKey(frameIndex) ? Frames[frameIndex] : null;
            set => Frames[frameIndex] = value;
        }

        public Dictionary<int, IFrame> Frames { get; set; }


        public bool InsertFrame(int index, Frame frame)
        {
            if (Frames.ContainsKey(index)) return false;
            Frames.Add(index, frame);
            return true;
        }

        public bool InsertFrame(int index, KeyFrame frame)
        {
            for (int i = index; i < index + frame.Length; i++)
                if (Frames.ContainsKey(i)) return false;

            Frames.Add(index, frame.Began);
            for (int i = index + 1; i < index + frame.Length - 1; i++)
                Frames.Add(index, null);
            Frames.Add(index + frame.Length - 1, frame.End);
            return true;
        }
    }

}

