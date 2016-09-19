using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZstd.UserControls
{
    public static class TimeSpan2
    {
        /// <summary> 时间跨度的单位 </summary>
        public enum TimeSpanUnit
        {
            Years = 0,
            Months = 1,
            Days = 2,
            Hours = 3,
            Minites = 4,
        }

        /// <summary> 根据当前指定的时间跨度来对当前时间的增减 </summary>
        /// <param name="originTime">初始时间</param>
        /// <param name="spanValue">时间跨度的数值。正值表示增加时间跨度，负值表示送去时间跨度。</param>
        /// <param name="spanUnit">时间跨度的单位</param>
        /// <returns></returns>
        public static DateTime GetTimeFromTimeSpan(DateTime originTime, double spanValue, TimeSpanUnit spanUnit)
        {
            //
            DateTime modifiedTime = default(DateTime);
            switch (spanUnit)
            {
                case TimeSpanUnit.Years:
                    {
                        modifiedTime = originTime.AddYears((int)spanValue);
                        break;
                    }
                case TimeSpanUnit.Months:
                    {
                        modifiedTime = originTime.AddMonths((int)spanValue);
                        break;
                    }
                case TimeSpanUnit.Days:
                    {
                        modifiedTime = originTime.AddDays(spanValue);
                        break;
                    }
                case TimeSpanUnit.Hours:
                    {
                        modifiedTime = originTime.AddHours(spanValue);
                        break;
                    }
                case TimeSpanUnit.Minites:
                    {
                        modifiedTime = originTime.AddMinutes(spanValue);
                        break;
                    }
            }

            return modifiedTime;
        }
    }
}