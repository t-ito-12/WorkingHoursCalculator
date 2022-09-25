using Prism.Commands;
using System;
using System.Collections.Generic;
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
        /// 時間
        /// </summary>
        public Dictionary<int, string> Hour { get; } = new Dictionary<int, string>();

        /// <summary>
        /// 分
        /// </summary>
        public Dictionary<int, string> Minute { get; } = new Dictionary<int, string>();

        /// <summary>
        /// 休憩の有無
        /// </summary>
        public Dictionary<bool, string> Rest { get; } = new Dictionary<bool, string>();

        /// <summary>
        /// 勤務開始時間(時)
        /// </summary>
        public int StartHour
        {
            get
            {
                return m_startHour;
            }
            set
            {
                if (m_startHour != value)
                {
                    m_startHour = value;
                    RaisePropertyChanged("StartHour");
                }
            }
        }
        private int m_startHour;

        /// <summary>
        /// 勤務開始時間(分)
        /// </summary>
        public int StartMinute
        {
            get
            {
                return m_startMinute;
            }
            set
            {
                if (m_startMinute != value)
                {
                    m_startMinute = value;
                    RaisePropertyChanged("StartMinute");
                }
            }
        }
        private int m_startMinute;

        /// <summary>
        /// 勤務終了時間(時)
        /// </summary>
        public int EndHour
        {
            get
            {
                return m_endHour;
            }
            set
            {
                if (m_endHour != value)
                {
                    m_endHour = value;
                    RaisePropertyChanged("EndHour");
                }
            }
        }
        private int m_endHour;

        /// <summary>
        /// 勤務終了時間(分)
        /// </summary>
        public int EndMinute
        {
            get
            {
                return m_endMinute;
            }
            set
            {
                if (m_endMinute != value)
                {
                    m_endMinute = value;
                    RaisePropertyChanged("EndMinute");
                }
            }
        }
        private int m_endMinute;

        /// <summary>
        /// 休憩の有無
        /// </summary>
        public bool IsRest
        {
            get
            {
                return m_isRest;
            }
            set
            {
                if (m_isRest != value)
                {
                    m_isRest = value;
                    RaisePropertyChanged("IsRest");
                }
            }
        }
        private bool m_isRest;

        /// <summary>
        /// 勤務時間(時)
        /// </summary>
        public int ResultHour
        {
            get { return m_resultHour; }
            set
            {
                if (m_resultHour != value)
                {
                    m_resultHour = value;
                    RaisePropertyChanged("ResultHour");
                }
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
                if (m_resultMinute != value)
                {
                    m_resultMinute = value;
                    RaisePropertyChanged("ResultMinute");
                }
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
                if (value is string && !string.IsNullOrEmpty(value))
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
        /// Viewを初期化するコマンド
        /// </summary>
        public ICommand InitializeCommand { get; }

        /// <summary>
        /// 設定時間が変更されたときに実行されるコマンド
        /// </summary>
        public ICommand SelectionChangedCommand { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CalculationWindowViewModel()
        {
            for (int i = 0; i < 24; i++)
            {
                Hour.Add(i, i.ToString());
            }

            for (int i = 0; i < 60; i++)
            {
                Minute.Add(i, i.ToString());
            }

            Rest.Add(true, "あり");
            Rest.Add(false, "なし");

            InitializeTimes();
            CalculateWorkTime();

            InitializeCommand = new DelegateCommand(() =>
            {
                InitializeTimes();
            });

            SelectionChangedCommand = new DelegateCommand(() =>
            {
                CalculateWorkTime();
            });
        }

        /// <summary>
        /// 設定時間を初期値に戻す
        /// </summary>
        public void InitializeTimes()
        {
            StartHour = 0;
            StartMinute = 0;
            EndHour = 0;
            EndMinute = 0;
            IsRest = true;
        }

        /// <summary>
        /// 現在選択中の日付に指定の日数を加えた日付を、現在選択中の日付に設定する
        /// </summary>
        /// <param name="_addDays">追加する日数</param>
        public void AddSelectedDay(int _addDays)
        {
            DateTime sd = m_selectedDay;
            SelectedDay = sd.AddDays(_addDays).ToString();
        }

        /// <summary>
        /// 勤務時間を計算する
        /// </summary>
        private void CalculateWorkTime()
        {
            TimeSpan startTime = TimeSpan.Parse(StartHour + ":" + StartMinute);
            TimeSpan endTime = TimeSpan.Parse(EndHour + ":" + EndMinute);

            TimeSpan Time = endTime - startTime;
            ResultHour = Time.Hours;
            ResultMinute = Time.Minutes;

            if (IsRest) ResultHour -= 1;

            if (ResultHour < 0) ResultHour += 24;
            if (ResultMinute < 0) ResultMinute += 24;
        }

        /// <summary>
        /// プロパティが変更されたことをViewに伝える
        /// </summary>
        /// <param name="propertyName"></param>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
