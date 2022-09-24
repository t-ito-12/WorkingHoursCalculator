using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WorkingHoursCalculator.Views
{
    public partial class WorkingHoursCalculatorWindow : UserControl
    {
        private ViewModels.CalculationWindowViewModel viewModel = new ViewModels.CalculationWindowViewModel();

        public WorkingHoursCalculatorWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            if (viewModel == null) throw new NullReferenceException("CalculationWindowViewModel is Null");

            viewModel.InitializeView += (s, e) => 
            {
                InitializeTime();
            };
        }

        public void InitializeTime()
        {
            if (StartHour != null && StartMinute != null && EndHour != null && EndMinute != null && Rest != null)
            {
                StartHour.SelectedIndex = 0;
                StartMinute.SelectedIndex = 0;
                EndHour.SelectedIndex = 0;
                EndMinute.SelectedIndex = 0;
                Rest.SelectedIndex = 0;

                var startHour = StartHour.SelectedItem as ComboBoxItem;
                var startMinute = StartMinute.SelectedItem as ComboBoxItem;
                var endHour = EndHour.SelectedItem as ComboBoxItem;
                var endMinute = EndMinute.SelectedItem as ComboBoxItem;
                var rest = Rest.SelectedItem as ComboBoxItem;
                bool isRest;
                if (rest != null && rest.Content != null && rest.Content as string == "あり") isRest = true;
                else isRest = false;
                viewModel.SetTime(Convert.ToInt32(startHour.Content), Convert.ToInt32(startMinute.Content), Convert.ToInt32(endHour.Content), Convert.ToInt32(endMinute.Content), isRest);
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartHour != null && StartMinute != null && EndHour != null && EndMinute != null && Rest != null)
            {
                var startHour = StartHour.SelectedItem as ComboBoxItem;
                var startMinute = StartMinute.SelectedItem as ComboBoxItem;
                var endHour = EndHour.SelectedItem as ComboBoxItem;
                var endMinute = EndMinute.SelectedItem as ComboBoxItem;
                var rest = Rest.SelectedItem as ComboBoxItem;
                bool isRest;
                if (rest != null && rest.Content!= null && rest.Content as string == "あり") isRest = true;
                else isRest = false;

                if(startHour != null && startMinute != null && endHour != null && endMinute != null && rest != null)
                {
                    if (startHour.Content != null && startMinute.Content != null && endHour.Content != null && endMinute.Content != null && rest.Content != null)
                    {
                        viewModel.SetTime(Convert.ToInt32(startHour.Content), Convert.ToInt32(startMinute.Content), Convert.ToInt32(endHour.Content), Convert.ToInt32(endMinute.Content), isRest);

                    }
                }
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeTime();
        }

        private void LeftButtonClick(object sender, RoutedEventArgs e)
        {
            viewModel.AddSelectedDay(-1);
        }
        private void RightButtonClick(object sender, RoutedEventArgs e)
        {
            viewModel.AddSelectedDay(1);
        }
    }
}