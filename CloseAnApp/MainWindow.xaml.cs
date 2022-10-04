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
        private TimeSpan breakTime; //Время перерыва в минутах
        private TimeSpan passedTime; //Прошедшее время в минутах
        private TimeSpan lastTime; //Оставшее время
        private bool isRun = false; //Режим запуска
        
        public MainWindow()
        {
            InitializeComponent();
        }

        //Время перерыва
        public TimeSpan BreakTime
        {
            get { return breakTime; }
            set 
            { 
                breakTime = value;
                this.textBoxBreakTime.Text = breakTime.ToString();
                lastTime = breakTime - passedTime;
            }
        }
        //Время прошедшее
        public TimeSpan PassedTime
        {
            get { return passedTime; }
            set 
            { 
                passedTime = value;
                this.textBoxPassedTime.Text = passedTime.ToString();
                lastTime = breakTime - passedTime;
            }
        }

        private void buttonUpBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromMinutes(1));
            if (value >= TimeSpan.Zero)
            {
                BreakTime = value;
            }
        }
        
        private void buttonUpDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromSeconds(10));
            if (value >= TimeSpan.Zero)
            {
                BreakTime = value;
            }
        }

        private void buttonDownBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromMinutes(-1));
            if (value >= TimeSpan.Zero && passedTime < value)
            {
                BreakTime = value;
            }
        }

        private void buttonDownDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromSeconds(-10));
            if (value >= TimeSpan.Zero && passedTime < value)
            {
                BreakTime = value;
            }
        }

        private void buttonUpPassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromMinutes(1));
            if (value >= TimeSpan.Zero && breakTime > value)
            {
                PassedTime = value;
            }
        }

        private void buttonUpDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromSeconds(10));
            if (value >= TimeSpan.Zero && breakTime > value)
            {
                PassedTime = value;
            }
        }

        private void buttonDownPassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromMinutes(-1));
            if (value >= TimeSpan.Zero)
            {
                PassedTime = value;
            }
        }

        private void buttonDownDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromSeconds(-10));
            if (value >= TimeSpan.Zero)
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
            
            this.textCurrentTime.Text = $"Время: {currentTime.ToLongTimeString()}";
            this.textTargetTime.Text = $"Закрытие: {targetTime.ToLongTimeString()}";
            //this.textTimer.Text = $"Таймер: {lastTime.ToLongTimeString()}";
            

            //Завершение программы
            if (!isRun)
            {
                this.targetTime = currentTime + lastTime;
            } 
            else
            {
                lastTime = targetTime.Subtract(currentTime);
                if (lastTime <= TimeSpan.Zero)
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
            else
            {
                this.buttonRun.Content = "Run";
                this.isRun = false;
            }
           
            
        }
    }
}
