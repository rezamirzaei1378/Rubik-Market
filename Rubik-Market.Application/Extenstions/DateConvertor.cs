using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Application.Extenstions
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(date) + "/" +
                   persianCalendar.GetMonth(date).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(date).ToString("00");
        }
        public static string ToShamsiStr(this string? date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(DateTime.Parse(date)) + "/" +
                   persianCalendar.GetMonth(DateTime.Parse(date)).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(DateTime.Parse(date)).ToString("00");
        }
    }
}
