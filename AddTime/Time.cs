/*
 CISP 405 Summer 2019 Tony Wang SID # 1443145
 
 Assignment 5 Description:
 Create a console app that uses extension methods from the main
 class itself in order to modify the provided Time.cs during 
 programming excution without trying to modify Time.cs itself.
 Extension methods should be contained in a static class and
 every method must refer to the Time.cs in order to carry out
 the necessary modifications during programming excution.
 

 Time.cs
 Providing the framework of Time class, for which this file's 
 purpose in part of the entire C# Console program is to create
 a Time layout that keeps track of hours, minutes, and seconds
 values and display them in this format: HH:MM:SS. Also, the Time
 format uses the 12-hour format (AM or PM), AM is displayed for
 when the hour value is between 0 to 12, while PM is displayed for
 when the hour value is between 13 to 23.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddTime
{
    class Time
    {
        public int Hour { get; set; } // 0 - 23
        public int Minute { get; set; } // 0 - 59
        public int Second { get; set; } // 0 - 59

        // set a new time value using universal time; throw an
        // exception if the hour, minute or second is invalid
        public void SetTime(int hour, int minute, int second)
        {
            // validate hour, minute and second
            if ((hour < 0 || hour > 23) || (minute < 0 || minute > 59) || 
                (second < 0 || second > 59))
            {
                throw new ArgumentOutOfRangeException();
            }

            Hour = hour;
            Minute = minute;
            Second = second;
        }

        // convert to string in universal-time format (HH:MM:SS)
        public string ToUniversalString() =>
            $"{Hour:D2}:{Minute:D2}:{Second:D2}";

        // convert to string in standard-time format (H:MM:SS AM or PM)
        public override string ToString() =>
            $"{((Hour == 0 || Hour == 12) ? 12 : Hour % 12)}:" +
            $"{Minute:D2}:{Second:D2} {(Hour < 12 ? "AM" : "PM")}";
    }
}
