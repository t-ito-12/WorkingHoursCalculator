using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkingHoursCalculator.ViewModels
{
    class CalculationWindowViewModel : INotifyPropertyChanged
    {
        public int StartHour
        {
            get { return m_startHour; }
            private set { m_startHour = value; }
        }
        private int m_startHour = 0;

        public int StartMinute
        {
            get { return m_startMinute; }
            private set { m_startMinute = value; }
        }
        private int m_startMinute = 0;

        public int EndHour
        {
            get { return m_endHour; }
            private set { m_endHour = value; }
        }
        private int m_endHour = 0;

        public int EndMinute
        {
            get { return m_endMinute; }
            private set { m_endMinute = value; }
        }
        private int m_endMinute = 0;

        public bool Rest
        {
            get { return m_rest; }
            private set { m_rest = value; }
        }
        private bool m_rest = true;

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

        public event EventHandler<EventArgs> InitializeView;

        public ICommand InitializeCommand { get; private set; }

        public CalculationWindowViewModel()
        {
            InitializeCommand = new DelegateCommand(() =>
            {
                if (InitializeView != null) InitializeView(this, EventArgs.Empty);
            });
        }

        public void SetSelectedDay(string _dateTime)
        {
            if (!string.IsNullOrEmpty(_dateTime))
            {
                SelectedDay = _dateTime;
            }
        }

        public void AddSelectedDay(int _addDays)
        {
            DateTime sd = m_selectedDay;
            SelectedDay = sd.AddDays(_addDays).ToString();
        }

        public void SetTime(int _startHour, int _startMinute, int _endHour, int _endMinute, bool _rest)
        {
            StartHour = _startHour; StartMinute = _startMinute; EndHour = _endHour; EndMinute = _endMinute; Rest = _rest;
            Calculate(StartHour, StartMinute, EndHour, EndMinute, Rest);
        }

        public void Calculate(int _startHour, int _startMinute, int _endHour, int _endMinute, bool _rest)
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
