using System;

public class TimeService
{
    public TimeService()
    {
        Time = System.DateTime.Now.ToString("hh:mm:ss");
    }
    public string Time { get; }
}
