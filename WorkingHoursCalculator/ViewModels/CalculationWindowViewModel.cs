using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WorkingHoursCalculator.ViewModels
{
    /// <summary>
    /// WorkingHoursCalculatorWindowのViewModel
    /// </summary>
    class CalculationWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 勤務開始時間(時)
        /// </summary>
        public int StartHour
        {
            get { return m_startHour; }
            private set { m_startHour = value; }
        }
        private int m_startHour = 0;
        
        /// <summary>
        /// 勤務開始時間(分)
        /// </summary>
        public int StartMinute
        {
            get { return m_startMinute; }
            private set { m_startMinute = value; }
        }
        private int m_startMinute = 0;

        /// <summary>
        /// 勤務終了時間(時)
        /// </summary>
        public int EndHour
        {
            get { return m_endHour; }
            private set { m_endHour = value; }
        }
        private int m_endHour = 0;

        /// <summary>
        /// 勤務終了時間(分)
        /// </summary>
        public int EndMinute
        {
            get { return m_endMinute; }
            private set { m_endMinute = value; }
        }
        private int m_endMinute = 0;

        /// <summary>
        /// 休憩の有無
        /// </summary>
        public bool Rest
        {
            get { return m_rest; }
            private set { m_rest = value; }
        }
        private bool m_rest = true;

        /// <summary>
        /// 勤務時間(時)
        /// </summary>
        public int ResultHour
        {
            get { return m_resultHour; }
            set
            {
                m_resultHour = value;
                RaisePropertyChanged("ResultHour");
            }
        }
        private int m_resultHour;

        /// <summary>
        /// 勤務時間(分)
        /// </summary>
        public int ResultMinute
        {
            get { return m_resultMinute; }
            set
            {
                m_resultMinute = value;
                RaisePropertyChanged("ResultMinute");
            }
        }
        private int m_resultMinute;

        /// <summary>
        /// 現在選択されている日付
        /// </summary>
        public string SelectedDay
        {
            get { return m_selectedDay.ToString("D") + "(" + m_selectedDay.ToString("ddd") + ")";  }
            set 
            {
                if(value is string && !string.IsNullOrEmpty(value))
                {
                    if(m_selectedDay != DateTime.Parse(value))
                    {
                        m_selectedDay = DateTime.Parse(value);
                        RaisePropertyChanged("SelectedDay");
                    }
                }
            }
        }
        private DateTime m_selectedDay = DateTime.Now;

        /// <summary>
        /// PropertyChangedイベントハンドラ
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Viewを初期化するイベントハンドラ
        /// </summary>
        public event EventHandler<EventArgs> InitializeView;

        /// <summary>
        /// Viewを初期化するコマンド
        /// </summary>
        public ICommand InitializeCommand { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CalculationWindowViewModel()
        {
            InitializeCommand = new DelegateCommand(() =>
            {
                if (InitializeView != null) InitializeView(this, EventArgs.Empty);
            });
        }

        /// <summary>
        /// 現在選択中の日付を設定する
        /// </summary>
        /// <param name="_dateTime"></param>
        public void SetSelectedDay(string _dateTime)
        {
            if (!string.IsNullOrEmpty(_dateTime))
            {
                SelectedDay = _dateTime;
            }
        }

        /// <summary>
        /// 現在選択中の日付に指定の日数を加えた日付を、現在選択中の日付に設定する
        /// </summary>
        /// <param name="_addDays"></param>
        public void AddSelectedDay(int _addDays)
        {
            DateTime sd = m_selectedDay;
            SelectedDay = sd.AddDays(_addDays).ToString();
        }

        /// <summary>
        /// 勤務時間と勤務開始・終了時間を設定する
        /// </summary>
        /// <param name="_startHour"></param>
        /// <param name="_startMinute"></param>
        /// <param name="_endHour"></param>
        /// <param name="_endMinute"></param>
        /// <param name="_rest"></param>
        public void SetTime(int _startHour, int _startMinute, int _endHour, int _endMinute, bool _rest)
        {
            StartHour = _startHour; StartMinute = _startMinute; EndHour = _endHour; EndMinute = _endMinute; Rest = _rest;
            Calculate(StartHour, StartMinute, EndHour, EndMinute, Rest);
        }

        /// <summary>
        /// 勤務時間を計算する
        /// </summary>
        /// <param name="_startHour"></param>
        /// <param name="_startMinute"></param>
        /// <param name="_endHour"></param>
        /// <param name="_endMinute"></param>
        /// <param name="_rest"></param>
        private void Calculate(int _startHour, int _startMinute, int _endHour, int _endMinute, bool _rest)
        {
            TimeSpan startTime = TimeSpan.Parse(_startHour + ":" + _startMinute);
            TimeSpan endTime = TimeSpan.Parse(_endHour + ":" + _endMinute);

            TimeSpan Time = endTime - startTime;
            ResultHour = Time.Hours;
            ResultMinute = Time.Minutes;

            if (_rest) ResultHour -= 1;

            if (ResultHour < 0) ResultHour += 24;
            if (ResultMinute < 0) ResultMinute += 60;
        }

        /// <summary>
        /// プロパティが変更されたことをViewに伝える
        /// </summary>
        /// <param name="propertyName"></param>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
