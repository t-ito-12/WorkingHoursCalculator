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
        private ViewModels.CalculationWindowViewModel ViewModel = new ViewModels.CalculationWindowViewModel();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorkingHoursCalculatorWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
            if (ViewModel == null) { throw new NullReferenceException("CalculationWindowViewModel is Null"); }
        }

        /// <summary>
        /// 削除ボタンが押下されたときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.InitializeTimes();
        }

        /// <summary>
        /// 左ボタン「＜」が押下されたときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.AddSelectedDay(-1);
        }

        /// <summary>
        /// 右ボタン「＞」が押下されたときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.AddSelectedDay(1);
        }
    }
}