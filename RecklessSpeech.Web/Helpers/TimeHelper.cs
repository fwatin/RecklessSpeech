using System;

namespace RecklessSpeech.Web.Helpers
{
    public static class TimeHelper
    {
        public static string ConvertMillisecondsToIso8601(this long milliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
            return timeSpan.TotalMinutes >= 60
              ? $"{(int)timeSpan.TotalHours} h {timeSpan.Minutes} min {timeSpan.Seconds} s"
                : $"{(int)timeSpan.TotalMinutes} min {timeSpan.Seconds} s";
        }
    }
}