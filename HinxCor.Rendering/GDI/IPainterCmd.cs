using System;
using System.Collections.Generic;
using System.Drawing;

public interface IPainterCmd
{
    PaintType Type { get; }
    Pen Pen { get; set; }
}

