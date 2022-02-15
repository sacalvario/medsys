using ECN.Contracts.ViewModels;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECN.ViewModels
{
    public class EcnSignedViewModel : ViewModelBase, INavigationAware
    {
        private int _ID;
        public int ID
        {
            get => _ID;
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }
        public EcnSignedViewModel()
        {

        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is int id)
            {
                ID = id;
            }
        }

        public void OnNavigatedFrom()
        {

        }
    }
}
