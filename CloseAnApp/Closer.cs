using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CloseAnApp
{
    internal class Closer : INotifyPropertyChanged
    {
        double breakTime = 0.0;
        double passedTime = 0.0;
        static double lastTime = 0.0;

        public double BreakTime
        {
            get { return breakTime; }
            set 
            { 
                breakTime = value;
                lastTime = breakTime - passedTime;
                RaisePropertyChanged(breakTime.ToString());
                Console.WriteLine(breakTime);
                Console.WriteLine(lastTime);
            }
        }

        public double PassedTime
        {
            get { return passedTime; }
            set 
            { 
                passedTime = value;
                lastTime = breakTime - passedTime;
                RaisePropertyChanged(passedTime.ToString());
                Console.WriteLine(passedTime);
                Console.WriteLine(lastTime);
            }
        }

        public double LastTime
        {
            get { return lastTime; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static void CloseApp(string appName)
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
