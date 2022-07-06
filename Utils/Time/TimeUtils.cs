using System.Collections.Generic;
using UnityEngine;

public class TimeUtils {

    public static string TimeSpanFormatSeconds(int seconds)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(seconds);


        if (seconds > 3599)
        {
            return string.Format("{0:D2}h{1:D2}m", t.Hours, t.Minutes);
        }
        else if (seconds > 59)
        {
            return string.Format("{0:D2}m{1:D2}s", t.Minutes, t.Seconds);
        }
        else
        {
            return string.Format("{0:D2}s", t.Seconds);
        }
    }
}
