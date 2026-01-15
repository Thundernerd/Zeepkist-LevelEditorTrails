using System;
using System.Collections.Generic;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class Trail
{
    public DateTimeOffset Stamp = DateTimeOffset.UtcNow;
    public List<TrailFrame> Frames = [];
}