
using ECN.Contracts.Services;
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

        private IEcnDataService _ecnDataService;
        private INumberPartsDataService _numberPartsDataService;
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

        private ObservableCollection<Attachment> _Attachments;
        public ObservableCollection<Attachment> Attachments
        {
            get => _Attachments;
            set
            {
                if (_Attachments != value)
                {
                    _Attachments = value;
                    RaisePropertyChanged("Attachments");
                }
            }
        }

        private ObservableCollection<EcnRevision> _Revisions;
        public ObservableCollection<EcnRevision> Revisions
        {
            get => _Revisions;
            set
            {
                if (_Revisions != value)
                {
                    _Revisions = value;
                    RaisePropertyChanged("Revisions");

                    // Se te olvido que tenias novio y hoy vienes a hablar
                    // Como si nada a rogarme pero yo ya
                    // Hoy tengo lo que quiero
                    // Mujeres y dinero
                    // No quiero nada en serio

                    // Siento por dentro que mi corazon dañado esta
                    // Me siento grave al pensar que estas con alguien mas
                    // Pero le haras los mismo
                    // Por que el no es tu tipo
                    // Lo engañaras conmigo

                    // Arriba del mercedez benz
                    // Impregnada dior quedo
                    // Cuando lo haciamos en el
                    // Asiento de atras y yo
                    // Haciendote venir
                    // Mojados los pantys
                    // Moncler que yo te di
                    // Para irnos a madrid

                    // Llevo dos dias bien enfiestado y no voy a parar
                    // Y estoy seguro que tu tambien vas hacerlo igual
                    // Pero me da lo mismo
                    // Que andes con esos tipos
                    // Ellos no son tu tipo

                    // Cierra los ojos y en tu mente mi cara veras
                    // Recordaras aquellos tiempos cuando te daba
                    // Fumabas a escondidas
                    // Conmigo te perdias
                    // Con nadie mas querias

                    // Arriba del mercedez benz
                    // Impregnada dior quedo
                    // Cuando lo haciamos en el
                    // Asiento de atras y yo
                    // Haciendote venir
                    // Mojados los pantys
                    // Moncler que yo te di
                    // Para irnos a madrid


                }
            }
        }

        private ObservableCollection<EcnDocumenttype> _Documents;
        public ObservableCollection<EcnDocumenttype> Documents
        {
            get => _Documents;
            set
            {
                if (_Documents != value)
                {
                    _Documents = value;
                    RaisePropertyChanged("Documents");
                }
            }
        }

        public ReportViewModel(Ecn ecn, IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService)
        {
            Ecn = ecn;
            _ecnDataService = ecnDataService;
            _numberPartsDataService = numberPartsDataService;

            Attachments = new ObservableCollection<Attachment>();

            foreach (var item in Ecn.EcnAttachments)
            {
                Attachments.Add(item.Attachment);
            }

            Revisions = new ObservableCollection<EcnRevision>();

            foreach (var item in Ecn.EcnRevisions)
            {
                Revisions.Add(item);
            }
        }

        private void ExportECN(Visual visual)
        {
            Report.ExportVisualAsPdf(visual);
        }
    }
}
