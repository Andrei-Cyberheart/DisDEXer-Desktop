using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace CloseAnApp
{
    internal class Program
    {
        private static string appName = "siisltd.dex.wwp"; //SIISLtd.DEX.WWP
        private static Timer timer;
        private static int minutes;
        private static int secondes;
        
        public static void Main(string[] args)
        {
            Console.Write("Enter time: ");
            string tempTime = Console.ReadLine();
            string[] times;

            if (tempTime.Contains('.'))
            {
                times = tempTime.ToString().Split('.');
            }
            else
            {
                times = tempTime.ToString().Split(',');
            }

            if (Convert.ToInt32(times[0]) == 0 && Convert.ToInt32(times[1]) == 0)
            {
                Console.WriteLine("Timer is not equal 0 or not negative!");
                return;
            }

            minutes = Convert.ToInt32(times[0]);
            secondes = times.Length > 1 ? Convert.ToInt32($"{times[1]}") : 0;
            secondes = secondes.ToString().Length == 1 ? secondes * 10 : secondes;

            SetTimer();
            Console.ReadLine();
        }

        static void SetTimer ()
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine($"{minutes} : {secondes:d2}");

            if (secondes > 0)
            {
                secondes--;
            }
            else if (secondes == 0 && minutes > 0)
            {
                minutes--;
                secondes = 59;
            }
            else if (secondes == 0 && minutes == 0) 
            {
                timer.Dispose();
                CloseApp(appName);
            }
        }

        static void CloseApp(string appName)
        {
            bool isClosed = false;
            Process[] GetPArray = Process.GetProcesses();
            foreach (Process testProcess in GetPArray)
            {
                string ProcessName = testProcess.ProcessName;

                ProcessName = ProcessName.ToLower();

                if (ProcessName.CompareTo(appName) == 0)
                {
                    testProcess.CloseMainWindow();
                    isClosed = true;
                }
            }

            if (isClosed)
            {
                Console.WriteLine("App closed!");
            }
            else
            {
                Console.WriteLine("App not find!");
            }
        }
    }
}
