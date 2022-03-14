using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Services;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ECN.ViewModels
{
    public class DashboardViewModel : ViewModelBase, INavigationAware
    {
        private readonly IEcnDataService _ecnDataService;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public DashboardViewModel(IEcnDataService ecnDataService)
        {
            _ecnDataService = ecnDataService;

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "En espera",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Colors.OrangeRed)
                },
                new PieSeries
                {
                    Title = "Cerrados",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Colors.ForestGreen)
                },
                new PieSeries
                {
                    Title = "Aprobados",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Colors.LimeGreen),
                    Name = "Status"
                },
                new PieSeries
                {
                    Title = "En firma",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Colors.Orange),
                    Name = "Status"
                }
            };

            Labels = new[] { "En espera", "Aprobado", "Cerrado" };
        }

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo(object parameter)
        {

        }
    }
}
