using System;

public static class DateTimeHelper
{
    public const long SECOND_TIME = 1000;
    public const long MINUTE_TIME = 60000;
    public const long HOUR_TIME = 3600000;
    public const long DAY_TIME = 86400000;

    public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    //public static DateTime FromMillisecondsSinceUnixEpoch(long milliseconds)
    //{
    //    return UnixEpoch.AddMilliseconds(milliseconds);
    //}

    public static DateTime FromMillisecondsLocal(long milliseconds)
    {
        return UnixEpoch.AddMilliseconds(milliseconds).ToLocalTime();
    }

    public static long FromDateTime(DateTime dateTime)
    {
        return (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
    }

    public static long Now()
    {
        return FromDateTime(DateTime.Now);
    }

    public static TimeSpan RemainTime(long milliseconds)
    {
        return TimeSpan.FromMilliseconds(milliseconds);
    }

    public static DateTime RoundToTicks(this DateTime target, long ticks) => new DateTime((target.Ticks + ticks / 2) / ticks * ticks, target.Kind);
    public static DateTime RoundUpToTicks(this DateTime target, long ticks) => new DateTime((target.Ticks + ticks - 1) / ticks * ticks, target.Kind);
    public static DateTime RoundDownToTicks(this DateTime target, long ticks) => new DateTime(target.Ticks / ticks * ticks, target.Kind);

    public static DateTime Round(this DateTime target, TimeSpan round) => RoundToTicks(target, round.Ticks);
    public static DateTime RoundUp(this DateTime target, TimeSpan round) => RoundUpToTicks(target, round.Ticks);
    public static DateTime RoundDown(this DateTime target, TimeSpan round) => RoundDownToTicks(target, round.Ticks);

    public static DateTime RoundToMinutes(this DateTime target, int minutes = 1) => RoundToTicks(target, minutes * TimeSpan.TicksPerMinute);
    public static DateTime RoundUpToMinutes(this DateTime target, int minutes = 1) => RoundUpToTicks(target, minutes * TimeSpan.TicksPerMinute);
    public static DateTime RoundDownToMinutes(this DateTime target, int minutes = 1) => RoundDownToTicks(target, minutes * TimeSpan.TicksPerMinute);

    public static DateTime RoundToHours(this DateTime target, int hours = 1) => RoundToTicks(target, hours * TimeSpan.TicksPerHour);
    public static DateTime RoundUpToHours(this DateTime target, int hours = 1) => RoundUpToTicks(target, hours * TimeSpan.TicksPerHour);
    public static DateTime RoundDownToHours(this DateTime target, int hours = 1) => RoundDownToTicks(target, hours * TimeSpan.TicksPerHour);


    public static DateTime RoundToDays(this DateTime target, int days = 1) => RoundToTicks(target, days * TimeSpan.TicksPerDay);
    public static DateTime RoundUpToDays(this DateTime target, int days = 1) => RoundUpToTicks(target, days * TimeSpan.TicksPerDay);
    public static DateTime RoundDownToDays(this DateTime target, int days = 1) => RoundDownToTicks(target, days * TimeSpan.TicksPerDay);
}