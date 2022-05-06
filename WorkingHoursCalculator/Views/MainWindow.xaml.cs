using System;
using System.Windows;
using System.Windows.Controls;

namespace WorkingHoursCalculator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.CalculationViewModel viewModel = new ViewModels.CalculationViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.ResultHour = 0;
            viewModel.ResultMinute = 0;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StartHour != null && StartMinute != null && EndHour != null && EndMinute != null && Rest != null)
            {
                var startHour = StartHour.SelectedItem as ComboBoxItem;
                var startMinute = StartMinute.SelectedItem as ComboBoxItem;
                var endHour = EndHour.SelectedItem as ComboBoxItem;
                var endMinute = EndMinute.SelectedItem as ComboBoxItem;
                var rest = Rest.SelectedItem as ComboBoxItem;
                bool isRest;
                if (rest.Content as string == "あり") isRest = true;
                else isRest = false;

                //if(startHour.Content != null) viewModel.ResultHour = Convert.ToInt32(startHour.Content);
                if (startHour.Content != null && startMinute.Content != null && endHour.Content != null && endMinute.Content != null && rest.Content != null)
                    viewModel.Calculate(Convert.ToInt32(startHour.Content), Convert.ToInt32(startMinute.Content), Convert.ToInt32(endHour.Content), Convert.ToInt32(endMinute.Content), isRest);
            }          
        }
    }
}
