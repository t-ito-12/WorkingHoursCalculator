using System;
using System.Windows;
using System.Windows.Controls;

namespace WorkingHoursCalculator.Views
{
    /// <summary>
    /// WorkingHoursCalculatorWindow.xamlの相互作用ロジック
    /// </summary>
    public partial class WorkingHoursCalculatorWindow : UserControl
    {
        /// <summary>
        /// このViewのViewModel
        /// </summary>
        private ViewModels.CalculationWindowViewModel viewModel = new ViewModels.CalculationWindowViewModel();

        /// <summary>
        /// コンストラクタ
        /// </summary>
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

        /// <summary>
        /// Viewの初期化
        /// 勤務時間と勤務開始・終了時間および休憩の有無を初期値に設定する
        /// </summary>
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

        /// <summary>
        /// コントロールの状態が変更されたときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 削除ボタンが押下されたときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeTime();
        }

        /// <summary>
        /// 左ボタン「＜」が押下されたときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftButtonClick(object sender, RoutedEventArgs e)
        {
            viewModel.AddSelectedDay(-1);
        }

        /// <summary>
        /// 右ボタン「＞」が押下されたときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButtonClick(object sender, RoutedEventArgs e)
        {
            viewModel.AddSelectedDay(1);
        }
    }
}