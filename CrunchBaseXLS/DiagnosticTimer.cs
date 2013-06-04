using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrunchBaseXLS
{
    public class DiagTimer
    {
        System.DateTime start;
        System.DateTime end = DateTime.MaxValue;
        public DiagTimer(bool starttimer)
        {
            if (starttimer) start = DateTime.Now;
        }
        public DiagTimer()
        {
            start = DateTime.Now;
        }
        public void Start()
        {
            start = DateTime.Now;
        }
        public void Stop()
        {
            end = DateTime.Now;
        }
        public TimeSpan Duration
        {
            get
            {
                if (end > DateTime.Now)
                {
                    return DateTime.Now - start;
                }
                else
                { //timer has already been stopped
                    return end - start;
                }
            }
        }
        public string GetDiagString()
        {
            string msg = "Start: " + start.ToShortTimeString() + " End: ";
            if (end > DateTime.Now)
            {
                msg += "RUNNING";
            }
            else
            {
                msg += end.ToShortTimeString() + " ";
            }
            msg += "Duration: " + Duration.ToString(@"dd\.hh\:mm\:ss");
            return (msg);
        }


    } // class DiagTimer
    public static class DiagnosticTimer
    {
        /// <summary>
        /// Internal class with info for each timer
        /// </summary>


        public static Dictionary<string, DiagTimer> TimerDictionary = new Dictionary<string, DiagTimer>();

        public static void StartTimer(string name)
        {
            DiagTimer t = new DiagTimer();
            if (!TimerDictionary.ContainsKey(name))
            {
                TimerDictionary.Add(name, t);
            }
            else // reset the named timer
            {
                TimerDictionary.Remove(name);
                TimerDictionary.Add(name, t);
            }
        }

        public static bool StopTimer(string name)
        {
            DiagTimer t = TimerDictionary[name];
            if (t != null)
            {
                t.Stop();
                return true;
            }
            else return false;
        }
        public static string GetTimer(string name)
        {
            if(TimerDictionary.ContainsKey(name))
            {

            DiagTimer t = TimerDictionary[name];
                string msg = "Timer: " + name + " " + t.GetDiagString();
                return (msg);
            }
            else
            {
                return ("Error: " + name + " Not Found");
            }
        }
        public static string StopAndGetTimer(string name)
        {
            DiagTimer t=TimerDictionary[name];
            if (t != null)
            {
                t.Stop();
                return GetTimer(name);
            }
            else {
                return ("Error: " + name + " Not Found");
            }
        }


        internal static void WriteAllTimersDiag()
        {
            foreach (KeyValuePair<string,DiagTimer> t in TimerDictionary)
            {
                System.Diagnostics.Debug.WriteLine("{0} : {1}", t.Key, GetTimer(t.Key));
            }

        }

        internal static TimeSpan GetTimerTimeSpan(string name)
        {
            if (TimerDictionary.ContainsKey(name))
            {
                DiagTimer t = TimerDictionary[name];
                return (t.Duration);
            }
            else return new TimeSpan(0);
            
        }
    } // static class DiagnosticTimer
  } // namespace
