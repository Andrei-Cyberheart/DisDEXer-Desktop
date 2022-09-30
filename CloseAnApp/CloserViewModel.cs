using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CloseAnApp
{
    public class CloserViewModel : INotifyPropertyChanged
    {
        private string nameApp;
        private Closer closer;

        static DateTime currentTime = DateTime.Now;
        static DateTime targetTime;

        public DateTime CurrentTime
        {
            set
            {
                if (value != currentTime)
                {
                    currentTime = value;
                    OnPropertyChanged("CurrentDateTime");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string BreakTime
        {
            get { return closer.BreakTime.ToString(); }
            set { closer.BreakTime = Convert.ToDouble(value); }
        }

        public string PassedTime
        {
            get { return closer.PassedTime.ToString(); }
            set { closer.PassedTime = Convert.ToDouble(value); }
        }
        public CloserViewModel(string nameApp)
        {
            this.nameApp = nameApp;
            closer = new Closer();
        }

        double StringToDate(string input)
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
