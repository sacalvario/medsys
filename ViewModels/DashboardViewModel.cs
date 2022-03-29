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
        public SeriesCollection SeriesCollectionLineChart { get; set; }
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
                    Fill = new SolidColorBrush(Color.FromRgb(251, 100, 45))
                },
                new PieSeries
                {
                    Title = "Cerrados",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Color.FromRgb(0, 172, 0))
                },
                new PieSeries
                {
                    Title = "En firma",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Colors.DarkOrange),
                    Name = "Status"
                },
                new PieSeries
                {
                    Title = "Cancelado",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Colors.Red),
                    Name = "Status"
                },
                new PieSeries
                {
                    Title = "Aprobados",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(5) },
                    DataLabels = true,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    FontSize = 18,
                    Fill = new SolidColorBrush(Color.FromRgb(100, 184, 0)),
                    Name = "Status"
                }
            };

            SeriesCollectionLineChart = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 },
                    PointGeometry = DefaultGeometries.Diamond,
                    PointGeometrySize = 15,
                    Fill = new SolidColorBrush(Colors.Transparent),
                    StrokeThickness = 3
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    Fill = new SolidColorBrush(Colors.Transparent),
                    StrokeThickness = 3 
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    Fill = new SolidColorBrush(Colors.Transparent),
                    StrokeThickness = 3
                }
            };

            Labels = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo" };
        }

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo(object parameter)
        {

        }
    }
}
