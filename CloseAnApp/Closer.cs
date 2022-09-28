using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CloseAnApp
{
    internal class Closer
    {
        private static string appName = "siisltd.dex.wwp"; //SIISLtd.DEX.WWP
        public static readonly DependencyProperty TitleProperty;

        public static void Run()
        {
            DateTime currentTime;
            DateTime targetTime;

            double breakTime;
            double passedTime;
            double lastTime;

            do
            {
                Console.Write("Перерыв: ");
                breakTime = StringToDate(Console.ReadLine());
                Console.Write("Прошло времени: ");
                passedTime = StringToDate(Console.ReadLine());

                if (passedTime > breakTime)
                    Console.WriteLine("Вы ввели болье времени, чем перерывю.\n" +
                        "Попробуйте еще раз.");
            } while (passedTime > breakTime);

            lastTime = breakTime - passedTime;

            //Установка времен
            currentTime = DateTime.Now;
            targetTime = currentTime.AddMinutes(lastTime);

            //ViewController
            while (true)
            {
                currentTime = DateTime.Now;
                Console.WriteLine($"Current time: {currentTime}");
                Console.WriteLine($"Target Time: {targetTime}");
                Console.WriteLine(targetTime - currentTime);

                if (currentTime.CompareTo(targetTime) >= 0)
                {
                    Console.WriteLine("Finish!!!");
                    break;
                }
                System.Threading.Thread.Sleep(200);
                Console.Clear();
            }
            CloseApp(appName);
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
        static double StringToDate(string input)
        {
            string[] times;
            double minutes;
            double secondes;

            if (input.Contains('.'))
            {
                times = input.ToString().Split('.');
            }
            else
            {
                times = input.ToString().Split(',');
            }

            if (Convert.ToInt32(times[0]) == 0 && Convert.ToInt32(times[1]) == 0)
            {
                Console.WriteLine("Timer is not equal 0 or not negative!");
                return 0.0;
            }

            //Парсинг и проверки введенного времени
            minutes = Convert.ToInt32(times[0]);
            secondes = times.Length > 1 ? Convert.ToInt32($"{times[1]}") : 0;
            secondes = secondes.ToString().Length == 1 ? secondes * 10 : secondes;
            secondes = secondes / 60;
            minutes = minutes + secondes;

            return minutes;
        }
    }
}
