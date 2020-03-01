using System;

public enum CaptureType
{
    Video,
    Image
}

[Flags]
public enum State
{
    Idle,
    ImageTracking,
    PlaneDetection,
    Both = ImageTracking | PlaneDetection
}