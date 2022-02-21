using ECN.Contracts.ViewModels;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECN.ViewModels
{
    public class EcnSignedViewModel : ViewModelBase, INavigationAware
    {
        private string _Message;
        public string Message
        {
            get => _Message;
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }
        public EcnSignedViewModel()
        {

        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is string message)
            {
                Message = message;
            }
        }

        public void OnNavigatedFrom()
        {

        }
    }
}
