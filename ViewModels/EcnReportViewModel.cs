using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows.Media;

namespace ECN.ViewModels
{
    public class EcnReportViewModel : ViewModelBase, INavigationAware
    {
        private Ecn _ecn;

        public Ecn Ecn
        {
            get => _ecn;
            set
            {
                if (_ecn != value)
                {
                    _ecn = value;
                    RaisePropertyChanged("Ecn");
                }
            }
        }

        private ICommand _ExportPDFCommand;
        public ICommand ExportPDFCommand
        {
            get
            {
                if (_ExportPDFCommand == null)
                {
                    _ExportPDFCommand = new RelayCommand<Visual>(ExportECN);
                }
                return _ExportPDFCommand;
            }
        }

        public EcnReportViewModel()
        {

        }
        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Ecn ecn)
            {
                Ecn = ecn;
            }
        }

        private void ExportECN(Visual visual)
        {
        }
    }
}
