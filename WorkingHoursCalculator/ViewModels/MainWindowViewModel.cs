using Prism.Mvvm;

namespace WorkingHoursCalculator.ViewModels
{
    /// <summary>
    /// MainWindowのViewModel
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get { return m_title; }
            private set
            {
                if (!string.IsNullOrEmpty(value)) m_title = value;
            }
        }
        private string m_title = "勤務時間計算機";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {


        }
    }
}
