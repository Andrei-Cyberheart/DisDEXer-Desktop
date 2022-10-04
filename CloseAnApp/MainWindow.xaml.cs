using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CloseAnApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string appName = "siisltd.dex.wwp"; //SIISLtd.DEX.WWP
        private DispatcherTimer timer; //Часы
        private DateTime currentTime; //Текущее время
        private DateTime targetTime; //Тригерное время
        private DateTime lastTime; //Оставшее время
        private bool isRun = false; //Режим запуска
        private double breakTime; //Время перерыва в минутах
        private double passedTime; //Прошедшее время в минутах
        private double difference = 0.0; //Оставшее время

        public MainWindow()
        {
            InitializeComponent();
        }

        public double BreakTime
        {
            get { return breakTime; }
            set 
            { 
                breakTime = value;
                this.textBoxBreakTime.Text = breakTime.ToString();
                difference = breakTime - passedTime;
            }
        }

        public double PassedTime
        {
            get { return passedTime; }
            set 
            { 
                passedTime = value;
                this.textBoxPassedTime.Text = passedTime.ToString();
                difference = breakTime - passedTime;
            }
        }

        private void buttonUpBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxBreakTime.Text) + 1;
            if (value >= 0)
            {
                BreakTime = value;
            }
        }
        
        private void buttonUpDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxBreakTime.Text) + 0.1;
            if (value >= 0)
            {
                BreakTime = value;
            }
        }

        private void buttonDownBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxBreakTime.Text) - 1;
            double passedTime = Convert.ToDouble(this.textBoxPassedTime.Text);
            if (value >= 0 && passedTime < value)
            {
                BreakTime = value;
            }
        }

        private void buttonDownDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxBreakTime.Text) - 0.1;
            double passedTime = Convert.ToDouble(this.textBoxPassedTime.Text);
            if (value >= 0 && passedTime < value)
            {
                BreakTime = value;
            }
        }

        private void buttonUpPassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxPassedTime.Text) + 1;
            double breakTime = Convert.ToDouble(this.textBoxBreakTime.Text);
            if (value >= 0 && breakTime > value)
            {
                PassedTime = value;
            }
        }

        private void buttonUpDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxPassedTime.Text) + 0.1;
            double breakTime = Convert.ToDouble(this.textBoxBreakTime.Text);
            if (value >= 0 && breakTime > value)
            {
                PassedTime = value;
            }
        }

        private void buttonDownPassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxPassedTime.Text) - 1;
            if (value >= 0)
            {
                PassedTime = value;
            }
        }

        private void buttonDownDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(this.textBoxPassedTime.Text) - 0.1;
            if (value >= 0)
            {
                PassedTime = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        void CloseApp(string appName)
        {
            Process[] GetPArray = Process.GetProcesses();
            foreach (Process testProcess in GetPArray)
            {
                string ProcessName = testProcess.ProcessName;

                ProcessName = ProcessName.ToLower();

                if (ProcessName.CompareTo(appName) == 0)
                {
                    testProcess.CloseMainWindow();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTime = DateTime.Now;
            lastTime = DateTime.MinValue.AddMinutes(difference);
            this.textCurrentTime.Text = $"Время: {currentTime.ToLongTimeString()}";
            this.textTargetTime.Text = $"Закрытие: {targetTime.ToLongTimeString()}";
            this.textTimer.Text = $"Таймер: {lastTime.ToLongTimeString()}";

            //Завершение программы
            if (!isRun)
            {
                this.targetTime = currentTime.AddMinutes(difference).AddSeconds(1);
            } 
            else
            {
                if (currentTime >= targetTime)
                {
                    timer.Stop();
                    CloseApp(appName);
                }
            }
        }

        private void buttonRun_Click(object sender, RoutedEventArgs e)
        {
            if (!isRun)
            {
                this.buttonRun.Content = "Stop";
                this.isRun = true;
            }
            else if (!isRun)
            {
                this.buttonRun.Content = "Run";
                this.isRun = false;
            }
           
            
        }
    }
}
