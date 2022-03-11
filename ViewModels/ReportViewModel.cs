
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleWPFReporting;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace ECN.ViewModels
{
    public class ReportViewModel : ViewModelBase
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

        private ObservableCollection<Numberpart> _NumberParts;
        public ObservableCollection<Numberpart> NumberParts
        {
            get => _NumberParts;
            set
            {
                if (_NumberParts != value)
                {
                    _NumberParts = value;
                    RaisePropertyChanged("NumberParts");
                }
            }
        }

        public ReportViewModel(Ecn ecn)
        {
            Ecn = ecn;
        }

        private void ExportECN(Visual visual)
        {
            Report.ExportVisualAsPdf(visual);
        }
    }
}
