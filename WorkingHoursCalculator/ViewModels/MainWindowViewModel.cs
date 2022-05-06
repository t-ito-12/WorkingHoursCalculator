using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WorkingHoursCalculator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "勤務時間計算機";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {


        }
    }
}
