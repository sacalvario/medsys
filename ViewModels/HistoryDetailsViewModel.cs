using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ECN.ViewModels
{
    public class HistoryDetailsViewModel : ViewModelBase, INavigationAware
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


        public HistoryDetailsViewModel(IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService)
        {
            Ecn = new Ecn();
            _ecnDataService = ecnDataService;
            _numberPartsDataService = numberPartsDataService;

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
            NumberParts = new ObservableCollection<Numberpart>();
            GetNumberParts();

        }

        private async void GetNumberParts()
        {
            Ecn.EcnNumberparts = await _numberPartsDataService.GetNumberPartsEcnsAsync(Ecn.Id);

            foreach(var item in Ecn.EcnNumberparts)
            {
                var np = await _numberPartsDataService.GetNumberPartAsync(item.ProductId);
                np.NumberPartTypeNavigation = await _numberPartsDataService.GetNumberpartTypeAsync(np.NumberPartType);
                np.Customer = await _numberPartsDataService.GetCustomerAsync(np.CustomerId);
                NumberParts.Add(np);
            }

        }
    }
}
