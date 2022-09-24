using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WorkingHoursCalculator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string m_title = "勤務時間計算機";
        public string Title
        {
            get { return m_title; }
        }

        public MainWindowViewModel()
        {


        }
    }
}
