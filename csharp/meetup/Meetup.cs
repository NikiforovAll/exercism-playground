using System;
using System.Collections.Generic;
using System.Linq;

public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}

public class Meetup
{
    private readonly IList<DateTime> _sundays = new List<DateTime>();
    private readonly int month;
    private readonly int year;

    public Meetup(int month, int year)
    {
        this.month = month;
        this.year = year;
        PrepareSundays();

        /// <summary>
        /// Contains all sundays including sundays 
        /// of previous month if month doesn't start from sunday
        /// </summary>
        void PrepareSundays()
        {
            var traversalDate = new DateTime(year, month, 1);
            var stopDate = traversalDate.AddMonths(1);
            while (traversalDate.DayOfWeek != DayOfWeek.Sunday)
            {
                traversalDate = traversalDate.AddDays(-1);
            }
            do
            {
                if (traversalDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    _sundays.Add(traversalDate);
                }
                traversalDate = traversalDate.AddDays(1);
            } while (traversalDate < stopDate);
        }
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        return schedule switch
        {
            Schedule.First => SlideRight(_sundays[0], (date) => IsDayOfWeek(date, month, dayOfWeek)),
            Schedule.Second =>
                SlideRight(SkipSundays(1), (date) => IsDayOfWeek(date, month, dayOfWeek)),
            Schedule.Third =>
                SlideRight(SkipSundays(2), (date) => IsDayOfWeek(date, month, dayOfWeek)),
            Schedule.Fourth =>
                SlideRight(SkipSundays(3), (date) => IsDayOfWeek(date, month, dayOfWeek)),
            Schedule.Teenth =>
                SlideRight(SkipSundays(0), (date) => date.DayOfWeek == dayOfWeek && date.Day > 12),
            Schedule.Last =>
                SlideLeft(_sundays[^1].AddDays(7), (date) => IsDayOfWeek(date, month, dayOfWeek)),
            _ => throw new NotImplementedException(),
        };

        DateTime SkipSundays(int number)
        {
            var cnt = _sundays[0].Month != _sundays[1].Month ? 1 : 0;
            var monthPrefixScan = _sundays[cnt].AddDays(-1);
            cnt += number;

            while (monthPrefixScan.Month == month)
            {
                if (monthPrefixScan.DayOfWeek == dayOfWeek)
                {
                    cnt--;
                    break;
                }
                monthPrefixScan = monthPrefixScan.AddDays(-1);
            }
            return _sundays[cnt];
        }

        static DateTime SlideRight(DateTime start, Predicate<DateTime> pred)
        {
            while (!pred(start))
            {
                start = start.AddDays(1);
            }
            return start;
        }

        static DateTime SlideLeft(DateTime start, Predicate<DateTime> pred)
        {
            while (!pred(start))
            {
                start = start.AddDays(-1);
            }
            return start;
        }

        static bool IsDayOfWeek(DateTime dt, int month, DayOfWeek dayOfWeek) =>
            dt.DayOfWeek == dayOfWeek && dt.Month == month;
    }
}
