using System;
using System.Collections.Generic;
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

namespace CloseAnApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string appName = "siisltd.dex.wwp"; //SIISLtd.DEX.WWP
        private CloserViewModel closerViewModel;

        public MainWindow()
        {
            closerViewModel = new CloserViewModel(appName);
            this.DataContext = closerViewModel;
            InitializeComponent();
        }

        private void buttonUpBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.BreakTime) + 1;
            if (value >= 0)
            {
                this.textBoxBreakTime.Text = value.ToString();
                this.closerViewModel.BreakTime = value.ToString();
            }
        }

        private void buttonUpDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.BreakTime) + 0.1;
            if (value >= 0)
            {
                this.textBoxBreakTime.Text = value.ToString();
                this.closerViewModel.BreakTime = value.ToString();
            }
        }

        private void buttonDownBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.BreakTime) - 1;
            double passedTime = Convert.ToDouble(closerViewModel.PassedTime);
            if (value >= 0 && passedTime < value)
            {
                this.textBoxBreakTime.Text = value.ToString();
                this.closerViewModel.BreakTime = value.ToString();
            }
        }

        private void buttonDownDoubleBreakTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.BreakTime) - 0.1;
            double passedTime = Convert.ToDouble(closerViewModel.PassedTime);
            if (value >= 0 && passedTime < value)
            {
                this.textBoxBreakTime.Text = value.ToString();
                this.closerViewModel.BreakTime = value.ToString();
            }
        }

        private void buttonUpPassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.PassedTime) + 1;
            double breakTime = Convert.ToDouble(closerViewModel.BreakTime);
            if (value >= 0 && breakTime > value)
            {
                this.textBoxPassedTime.Text = value.ToString();
                this.closerViewModel.PassedTime = value.ToString();
            }
        }

        private void buttonUpDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.PassedTime) + 0.1;
            double breakTime = Convert.ToDouble(closerViewModel.BreakTime);
            if (value >= 0 && breakTime > value)
            {
                this.textBoxPassedTime.Text = value.ToString();
                this.closerViewModel.PassedTime = value.ToString();
            }
        }

        private void buttonDownPassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.PassedTime) - 1;
            if (value >= 0)
            {
                this.textBoxPassedTime.Text = value.ToString();
                this.closerViewModel.PassedTime = value.ToString();
            }
        }

        private void buttonDownDoublePassedTime_Click(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(closerViewModel.PassedTime) - 0.1;
            if (value >= 0)
            {
                this.textBoxPassedTime.Text = value.ToString();
                this.closerViewModel.PassedTime = value.ToString();
            }
        }
    }
}
