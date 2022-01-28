using ECN.Contracts.ViewModels;
using GalaSoft.MvvmLight;

namespace ECN.ViewModels
{
    public class EcnRegistrationViewModel : ViewModelBase, INavigationAware
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
        public EcnRegistrationViewModel()
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
