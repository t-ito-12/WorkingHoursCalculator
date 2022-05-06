using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHoursCalculator.ViewModels
{
    class CalculationViewModel : INotifyPropertyChanged
    {
        private int resultHour;
        public int ResultHour
        {
            get { return resultHour; }
            set
            {
                resultHour = value;
                RaisePropertyChanged("ResultHour");
            }
        }

        private int resultMinute;
        public int ResultMinute
        {
            get { return resultMinute; }
            set
            {
                resultMinute = value;
                RaisePropertyChanged("ResultMinute");
            }
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
            if (ResultMinute < 0) ResultMinute += 24;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}
