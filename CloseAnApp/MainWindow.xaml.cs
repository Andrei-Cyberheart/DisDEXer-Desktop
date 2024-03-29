﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace CloseAnApp
{
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
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromMinutes(5));
            if (value >= TimeSpan.Zero)
            {
                BreakTime = value;
                this.targetTime = currentTime + lastTime;
            }
        }
        
        private void buttonUpDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromMinutes(1));
            if (value >= TimeSpan.Zero)
            {
                BreakTime = value;
                this.targetTime = currentTime + lastTime;
            }
        }

        private void buttonDownBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromMinutes(-5));
            if (value >= TimeSpan.Zero && passedTime < value)
            {
                BreakTime = value;
                this.targetTime = currentTime + lastTime;
            }
        }

        private void buttonDownDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxBreakTime.Text).Add(TimeSpan.FromMinutes(-1));
            if (value >= TimeSpan.Zero && passedTime < value)
            {
                BreakTime = value;
                this.targetTime = currentTime + lastTime;
            }
        }

        private void buttonUpPassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromMinutes(1));
            if (value >= TimeSpan.Zero && breakTime > value)
            {
                PassedTime = value;
                this.targetTime = currentTime + lastTime;
            }
        }

        private void buttonUpDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromSeconds(10));
            if (value >= TimeSpan.Zero && breakTime > value)
            {
                PassedTime = value;
                this.targetTime = currentTime + lastTime;
            }
        }

        private void buttonDownPassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromMinutes(-1));
            if (value >= TimeSpan.Zero)
            {
                PassedTime = value;
                this.targetTime = currentTime + lastTime;
            }
        }

        private void buttonDownDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan value = TimeSpan.Parse(this.textBoxPassedTime.Text).Add(TimeSpan.FromSeconds(-10));
            if (value >= TimeSpan.Zero)
            {
                PassedTime = value;
                this.targetTime = currentTime + lastTime;
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
            this.textTimer.Text = $"Таймер: {lastTime.Hours:00}:{lastTime.Minutes:00}:{lastTime.Seconds:00}";
            

            //Завершение программы
            if (!isRun)
            {
                this.targetTime = currentTime + lastTime;
            } 
            else
            {
                lastTime = targetTime.Subtract(currentTime);
                this.progressBarTimer.Value =  lastTime.TotalSeconds / breakTime.TotalSeconds * 100;
                if (lastTime <= TimeSpan.Zero)
                {
                    timer.Stop();
                    CloseApp(appName);
                    Close();
                }
            }
        }

        private void buttonRun_Click(object sender, RoutedEventArgs e)
        {
            if (!isRun)
            {
                this.buttonRun.Content = "Стоп";
                this.isRun = true;
                taskBarItem.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Indeterminate;
            }
            else
            {
                this.buttonRun.Content = "Пуск";
                this.isRun = false;
                taskBarItem.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;
            }
        }
    }
}
