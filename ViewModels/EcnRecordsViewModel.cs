//using ClosedXML.Excel;

using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class EcnRecordsViewModel : ViewModelBase, INavigationAware
    {
        private readonly IEcnDataService _ecnDataService;
        private readonly INavigationService _navigationService;
        private readonly IOpenFileService _openFileService;
        private readonly IWindowManagerService _windowManagerService;

        private ICollectionView collView;

        private ICommand _ExportCommand;
        public ICommand ExportCommand
        {
            get
            {
                if (_ExportCommand == null)
                {
                    _ExportCommand = new RelayCommand(ExportToExcel);
                }
                return _ExportCommand;
            }
        }

        private ICommand _navigateToDetailCommand;
        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Ecn>(NavigateToDetail);
        public EcnRecordsViewModel(IEcnDataService ecnDataService, INavigationService navigationService, IOpenFileService openFileService, IWindowManagerService windowManagerService)
        {
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;
            _openFileService = openFileService;
            _windowManagerService = windowManagerService;

            FilteredList = new ObservableCollection<Ecn>();
        }

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;

                collView.Filter = e =>
                {
                    var item = (Ecn)e;
                    return item != null && item.Id.ToString().Contains(value) || string.IsNullOrWhiteSpace(value) || value.Length == 0;
                };

                collView.Refresh();

                FilteredList = new ObservableCollection<Ecn>(collView.OfType<Ecn>().ToList());

                RaisePropertyChanged("Filter");
            }
        }

        private Ecn _SelectedItem;
        public Ecn SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        private ObservableCollection<Ecn> _Records;
        public ObservableCollection<Ecn> Records
        {
            get => _Records;
            set
            {
                if (_Records != value)
                {
                    _Records = value;
                    RaisePropertyChanged("Records");
                }
            }
        }

        private ObservableCollection<Ecn> _FilteredList;
        public ObservableCollection<Ecn> FilteredList
        {
            get => _FilteredList;
            set
            {
                if (_FilteredList != value)
                {
                    _FilteredList = value;
                    RaisePropertyChanged("FilteredList");
                }
            }
        }

        private async void GetRecords()
        {
            var data = await _ecnDataService.GetEcnRecordsAsync();

            foreach (var item in data)
            {
                item.ChangeType = await _ecnDataService.GetChangeTypeAsync(item.ChangeTypeId);
                item.DocumentType = await _ecnDataService.GetDocumentTypeAsync(item.DocumentTypeId);
                item.Status = await _ecnDataService.GetStatusAsync(item.StatusId);
                item.Employee = await _ecnDataService.GetEmployeeAsync(item.EmployeeId);

                item.ChangeTypeName = item.ChangeType.ChangeTypeName;
                item.DocumentTypeName = item.DocumentType.DocumentTypeName;
                item.EmployeeName = item.Employee.Name;
                item.StatusName = item.Status.StatusName;

                if (Convert.ToBoolean(item.IsEco))
                {
                    item.EcnEco = await _ecnDataService.GetEcnEcoAsync(item.Id);
                }

                Records.Add(item);
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            Records = new ObservableCollection<Ecn>();
            GetRecords();

            FilteredList = new ObservableCollection<Ecn>(Records);
            collView = CollectionViewSource.GetDefaultView(FilteredList);

            SelectedItem = null;
        }

        public void OnNavigatedFrom()
        {

        }

        private void NavigateToDetail(Ecn ecn)
        {
            if (ecn != null)
            {
                _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
            }
        }

        private void ExportToExcel()
        {
            //if (_openFileService.SaveFileExportDialog())
            //{
            //    var workbook = new XLWorkbook();
            //    var worksheet = workbook.Worksheets.Add("Datos");

            //    worksheet.Cell(1, 1).Value = "Folio";
            //    worksheet.Cell(1, 2).Value = "Fecha de inicio";
            //    worksheet.Cell(1, 3).Value = "Fecha de cierre";
            //    worksheet.Cell(1, 4).Value = "Tipo de cambio";
            //    worksheet.Cell(1, 5).Value = "Tipo de documento";
            //    worksheet.Cell(1, 6).Value = "Nombre de documento";
            //    worksheet.Cell(1, 7).Value = "Número de documento";
            //    worksheet.Cell(1, 8).Value = "Nivel de revisión anterior de documento";
            //    worksheet.Cell(1, 9).Value = "Nivel de revisión actual de documento";
            //    worksheet.Cell(1, 10).Value = "Nivel de revisión anterior de dibujo";
            //    worksheet.Cell(1, 11).Value = "Nivel de revisión actual de dibujo";
            //    worksheet.Cell(1, 12).Value = "Generado por";
            //    worksheet.Cell(1, 13).Value = "Descripción del cambio";
            //    worksheet.Cell(1, 14).Value = "Justificación del cambio";
            //    worksheet.Cell(1, 15).Value = "Afectaciones de manufactura";
            //    worksheet.Cell(1, 16).Value = "Estatus";

            //    for (int i = 0; i < 16; i++)
            //    {
            //        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
            //        worksheet.Cell(1, i + 1).Style.Font.FontColor = XLColor.White;
            //        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            //        worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //    }

            //    int rowCount = 2;

            //    foreach (var item in Records)
            //    {
            //        XLAlignmentHorizontalValues align = XLAlignmentHorizontalValues.Center;

            //        worksheet.Cell(rowCount, 1).Value = item.Id;
            //        worksheet.Cell(rowCount, 1).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 2).Value = item.StartDate;
            //        worksheet.Cell(rowCount, 2).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 3).Value = item.EndDate;
            //        worksheet.Cell(rowCount, 3).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 4).Value = item.ChangeTypeName;
            //        worksheet.Cell(rowCount, 4).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 5).Value = item.DocumentTypeName;
            //        worksheet.Cell(rowCount, 5).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 6).Value = item.DocumentName;
            //        worksheet.Cell(rowCount, 6).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 7).Value = item.DocumentNo;
            //        worksheet.Cell(rowCount, 7).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 8).Value = item.OldDocumentLvl;
            //        worksheet.Cell(rowCount, 8).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 9).Value = item.DocumentLvl;
            //        worksheet.Cell(rowCount, 9).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 10).Value = item.OldDrawingLvl;
            //        worksheet.Cell(rowCount, 10).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 11).Value = item.DrawingLvl;
            //        worksheet.Cell(rowCount, 11).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 12).Value = item.EmployeeName;
            //        worksheet.Cell(rowCount, 12).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 13).Value = item.ChangeDescription;
            //        worksheet.Cell(rowCount, 13).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 14).Value = item.ChangeJustification;
            //        worksheet.Cell(rowCount, 14).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 15).Value = item.ManufacturingAffectations;
            //        worksheet.Cell(rowCount, 15).Style.Alignment.Horizontal = align;
            //        worksheet.Cell(rowCount, 16).Value = item.StatusName;
            //        worksheet.Cell(rowCount, 16).Style.Alignment.Horizontal = align;


            //        rowCount++;
            //    }

            //    var rango = worksheet.Range(1, 1, Records.Count + 1, 16);
            //    var table = rango.CreateTable();
            //    table.Theme = XLTableTheme.None;

            //    _ = worksheet.RowsUsed().Style.Alignment.SetWrapText(true);
            //    _ = worksheet.Columns().AdjustToContents();
            //    workbook.SaveAs(_openFileService.Path);
            //    _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Datos exportados correctamente.");
            //}
        }

    }
}
