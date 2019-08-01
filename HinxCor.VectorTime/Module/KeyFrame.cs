using System;
using System.Collections.Generic;

public class KeyFrame : IFrame
{
    public int Length { get; set; }
    public Frame Began { get; set; }
    public Frame End { get; set; }

}

