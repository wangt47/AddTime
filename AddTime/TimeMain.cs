/*
 CISP 405 Summer 2019 Tony Wang SID # 1443145
 
 Assignment 5 Description:
 Create a console app that uses extension methods from the main
 class itself in order to modify the provided Time.cs during 
 programming excution without trying to modify Time.cs itself.
 Extension methods should be contained in a static class and
 every method must refer to the Time.cs in order to carry out
 the necessary modifications during programming excution.
 

 TimeMain.cs
 Performs the series of programming tasks that is required of this
 assignment. The main program can modify Time.cs by creating a static
 class TimeExtensions, this secondary class holds all additional methods
 that allows this program to modfiy Time.cs without implementing these
 methods into Time.cs itself. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddTime
{
    class TimeMain
    {
        static void Main(string[] args)
        {
            var myTime = new Time();    // declare new Time class to create time
            int myHours, myMinutes, mySeconds, numPrompt; // initial time values set by user

            // prompt the user to enter in values of hours, minutes, & seconds
            Console.Write("Enter the time\n");
            Console.Write("Hours: ");
            myHours = int.Parse(Console.ReadLine());
            Console.Write("Minutes: ");
            myMinutes = int.Parse(Console.ReadLine());
            Console.Write("Seconds: ");
            mySeconds = int.Parse(Console.ReadLine());

            // set the time from the input values
            myTime.SetTime(myHours, myMinutes, mySeconds);

            // prompt the user to make the first choice
            Console.WriteLine("1. Add 1 Second.");
            Console.WriteLine("2. Add 1 Minute.");
            Console.WriteLine("3. Add 1 Hour.");
            Console.WriteLine("4. Add Seconds.");
            Console.WriteLine("5. Exit.");
            Console.WriteLine();

            /*
                Repeat choice prompt and add time calculation
                until user selects 5 to exit application.
             */ 
            do
            {
                // choice prompt
                Console.Write("Choice: ");
                numPrompt = int.Parse(Console.ReadLine());

                /*  
                    Each if statement executes an extended method
                    (or function) that add a value to the time
                    and can only be done once per execution.
                 */
                if (numPrompt == 1)
                    myTime.IncrementSecond();
                if (numPrompt == 2)
                    myTime.IncrementMinute();
                if (numPrompt == 3)
                    myTime.IncrementHour();
                if (numPrompt == 4)
                {
                    // prompt user to enter any positive value
                    // for adding any seconds to the time
                    Console.Write("Enter seconds to tick: ");
                    int myinputSeconds = int.Parse(Console.ReadLine());
                    
                    // Will not execute method if int value is negative.
                    if (myinputSeconds >= 0)
                        myTime.AddSeconds(myinputSeconds);
                }
                 // Exit program upon selecting 5. 
                if (numPrompt == 5)
                {
                    break;
                }

                // Display the int values of Hour, Minute and Second
                Console.WriteLine($"Hour: {myTime.Hour} Minute: {myTime.Minute} Second: {myTime.Second}");
                // Display Universal time in 24-hour format
                Console.Write($"Universal time: {myTime.ToUniversalString()}  ");
                // Display Standard time in 12-hour format, tell whether the time is AM or PM.
                Console.Write("Standard time: ");
                myTime.DisplayTime();

                // Prompt user to make another choice again
                Console.WriteLine("1. Add 1 Second.");
                Console.WriteLine("2. Add 1 Minute.");
                Console.WriteLine("3. Add 1 Hour.");
                Console.WriteLine("4. Add Seconds.");
                Console.WriteLine("5. Exit.");
                Console.WriteLine();
            } while (numPrompt != 5);
        }
    }
    static class TimeExtensions
    {
        // display the Time object in console
        public static void DisplayTime(this Time aTime)
        {
            Console.WriteLine(aTime.ToString());
        }

        // add one hour to the time
        public static void IncrementHour(this Time aTime)
        {
            int increaseHour = aTime.Hour + 1; // increment current hour by 1

            // increment hour if current value is not at max limit yet
            if (increaseHour < 24)
            {
                aTime.SetTime(increaseHour, aTime.Minute, aTime.Second);
            }
            // reset time's hour to 0 for reaching the max limit
            else if (increaseHour == 24)
            {
                aTime.SetTime(0, aTime.Minute, aTime.Second);
            }
        }

        // add one minute to the time
        public static void IncrementMinute(this Time aTime)
        {
            
            int increaseMinute = aTime.Minute + 1; // increment current minute by 1
            
            // increment minute if current value is not at max limit yet
            if (increaseMinute < 60)
            {
                aTime.SetTime(aTime.Hour, increaseMinute, aTime.Second);
            }
            // increment hour if its current value is not at 23 yet
            // and reset time's minute to 0.
            else if (aTime.Hour < 23 && increaseMinute == 60)
            {
                aTime.SetTime(aTime.Hour+1, 0, aTime.Second);
            }
            // reset hour to 0 for incrementing to 24
            // reset minute to 0 for incrementing to 60 
            else if (aTime.Hour == 23 && increaseMinute == 60)
            {
                aTime.SetTime(0, 0, aTime.Second);
            }
        }

        // add one second to the time
        public static void IncrementSecond(this Time aTime)
        {
            int increaseSecond = aTime.Second + 1; // incrementing current second by 1

            // increment second if current value is not at max limit yet
            if (increaseSecond < 60)
            {
                aTime.SetTime(aTime.Hour, aTime.Minute, increaseSecond);
            }
            // increment minute if it's below 59 and reset second to 0
            else if (aTime.Minute < 59 && increaseSecond == 60)
            {
                aTime.SetTime(aTime.Hour, aTime.Minute + 1, 0);
            }
            // increment hour if it's below 23, minute and second are reset to 0
            else if (aTime.Hour < 23 && aTime.Minute == 59 && increaseSecond == 60)
            {
                aTime.SetTime(aTime.Hour + 1, 0, 0);
            }
            // reset hour, minute, and second to 0
            else if (aTime.Hour == 23 && aTime.Minute == 59 && increaseSecond == 60)
            {
                aTime.SetTime(0, 0, 0);
            }
        }

        // add the sepcified number of seconds to the time
        public static void AddSeconds(this Time aTime, int seconds)
        {
            int increaseMinutes, increaseHours; // variables to add minutes and hours if possible
            int newSecond, newMinute, newHour; // variables to set new values
            var newTime = new Time()
            {
                Hour = aTime.Hour,
                Minute = aTime.Minute
            };
            newSecond = (aTime.Second + seconds) % 60; // create new second value to replace
            
            /* 
               if sum of current seconds and added seconds is past the max limit
               add minutes to the current minutes
            */
            if ((aTime.Second + seconds) > 60)
            {
                increaseMinutes = (aTime.Second + seconds) / 60; // create added minutes

                /* 
                    if sum of current minutes and added minutes is past the max limit
                    set new minutes value to replace and set add hours to the current hours
                 */
                if ((aTime.Minute + increaseMinutes) > 60)
                {
                    newMinute = (aTime.Minute + increaseMinutes) % 60; // create new minute value to replace
                    increaseHours = increaseMinutes / 60; // create added hours

                    /* 
                        if sum of current hours and added hours is past the max limit
                        set new hours value to replace
                     */
                    if ((aTime.Hour + increaseHours) > 23)
                    {
                        newHour = (aTime.Hour + increaseHours) % 24; // create new hour value to replace
                        aTime.SetTime(newHour, newMinute, newSecond); // set time with all new hour, minute, and second values
                    }
                    /* 
                        if sum of current hours and added hours is below the max limit (24 hours)
                        add hours to the current hours
                     */
                    else
                    {
                        aTime.SetTime(aTime.Hour + increaseHours, newMinute, newSecond);
                    }
                }
                /* 
                    if sum of current minutes and added minutes is below the max limit (60 minutes)
                    add minutes to the current minutes
                */
                else if ((aTime.Minute + increaseMinutes) < 60)
                {
                    aTime.SetTime(aTime.Hour, aTime.Minute + increaseMinutes, newSecond);
                }
            
                // if sum of current minutes and added minutes is at the max limit (60 minutes)
                else if ((aTime.Minute + increaseMinutes) == 60)
                {
                    // increment hour if below 23 and reset minute to 0
                    if (aTime.Hour < 23)
                        aTime.SetTime(aTime.Hour+1, 0, newSecond);

                    // reset hour and minute to 0
                    if (aTime.Hour == 23)
                        aTime.SetTime(0, 0, newSecond);
                }
            }
            /* 
                if sum of current seconds and added seconds is below the max limit (60 seconds)
                add seconds to the current seconds (or set new second).
            */
            else if ((aTime.Second + seconds) < 60)
            {
                aTime.SetTime(aTime.Hour, aTime.Minute, newSecond);
            }
             
            //  if sum of current seconds and added seconds is at the max limit (60 seconds)
            else if ((aTime.Second + seconds) == 60)
            {
                // increment minute if under 59 and reset second to 0
                if (aTime.Minute < 59)
                {
                    aTime.SetTime(aTime.Hour, aTime.Minute + 1, 0);
                }
                // if minute is 59 and second has reached 60
                else if (aTime.Minute == 59)
                {
                    // increment hour if under 23 and reset minute and second to 0
                    if (aTime.Hour < 23)
                        aTime.SetTime(aTime.Hour + 1, 0, 0);
                    
                    // reset hour, minute, and second to 0 
                    if (aTime.Hour == 23)
                        aTime.SetTime(0, 0, 0);
                }
            }
        }
    }
}
