

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

using Excel = Microsoft.Office.Interop.Excel;

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
                    return item != null && (item.Id.ToString().Contains(value) || string.IsNullOrWhiteSpace(value) || value.Length == 0);
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

        public void ExportToExcel()
        {
            if (_openFileService.SaveFileExportDialog())
            {
                Excel.Application xlApp = new Excel.Application();
                if (xlApp != null)
                {
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Cells[1, 1] = "";


                    Excel.Range formatRange;

                    int rowCount = 1;

                    xlWorkSheet.Cells[rowCount, 1] = "Folio";
                    xlWorkSheet.Cells[rowCount, 2] = "Fecha de inicio";
                    xlWorkSheet.Cells[rowCount, 3] = "Fecha de cierre";
                    xlWorkSheet.Cells[rowCount, 4] = "Tipo de cambio";
                    xlWorkSheet.Cells[rowCount, 5] = "Tipo de documento";
                    xlWorkSheet.Cells[rowCount, 6] = "Nombre de documento";
                    xlWorkSheet.Cells[rowCount, 7] = "Número de documento";
                    xlWorkSheet.Cells[rowCount, 8] = "Nivel de revisión anterior del documento";
                    xlWorkSheet.Cells[rowCount, 9] = "Nivel de revisión actual del documento";
                    xlWorkSheet.Cells[rowCount, 10] = "Nivel de revisión anterior del dibujo";
                    xlWorkSheet.Cells[rowCount, 11] = "Nivel de revisión actual del dibujo";
                    xlWorkSheet.Cells[rowCount, 12] = "Generado por";
                    xlWorkSheet.Cells[rowCount, 13] = "Descripción del cambio";
                    xlWorkSheet.Cells[rowCount, 14] = "Justificación del cambio";
                    xlWorkSheet.Cells[rowCount, 15] = "Afectaciones de manufactura";
                    xlWorkSheet.Cells[rowCount, 16] = "Estatus";

                    rowCount++;

                    formatRange = xlWorkSheet.get_Range("A1", "P1");
                    formatRange.Font.Bold = true;
                    formatRange.Font.Color = Excel.XlRgbColor.rgbWhite;
                    formatRange.Interior.Color = Excel.XlRgbColor.rgbRed;

                    foreach (var item in Records)
                    {
                        xlWorkSheet.Cells[rowCount, 1] = item.Id;
                        xlWorkSheet.Cells[rowCount, 2] = item.StartDate;
                        xlWorkSheet.Cells[rowCount, 3] = item.EndDate;
                        xlWorkSheet.Cells[rowCount, 4] = item.ChangeTypeName;
                        xlWorkSheet.Cells[rowCount, 5] = item.DocumentTypeName;
                        xlWorkSheet.Cells[rowCount, 6] = item.DocumentName;
                        xlWorkSheet.Cells[rowCount, 7] = item.DocumentNo;
                        xlWorkSheet.Cells[rowCount, 8] = item.OldDocumentLvl;
                        xlWorkSheet.Cells[rowCount, 9] = item.DocumentLvl;
                        xlWorkSheet.Cells[rowCount, 10] = item.OldDrawingLvl;
                        xlWorkSheet.Cells[rowCount, 11] = item.DrawingLvl;
                        xlWorkSheet.Cells[rowCount, 12] = item.EmployeeName;
                        xlWorkSheet.Cells[rowCount, 13] = item.ChangeDescription;
                        xlWorkSheet.Cells[rowCount, 14] = item.ChangeJustification;
                        xlWorkSheet.Cells[rowCount, 15] = item.ManufacturingAffectations;
                        xlWorkSheet.Cells[rowCount, 16] = item.StatusName;

                        rowCount++;
                    }

                    formatRange = xlWorkSheet.get_Range("A1", "P" + Records.Count + 1);
                    formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    formatRange = xlWorkSheet.get_Range("A1", "P" + Records.Count);
                    formatRange.AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);

                    xlWorkSheet.Columns.AutoFit();

                    if (!string.IsNullOrEmpty(_openFileService.Path.ToString()))
                    {
                        xlWorkBook.SaveAs(_openFileService.Path, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();

                        ReleaseObject(xlWorkSheet);
                        ReleaseObject(xlWorkBook);
                        ReleaseObject(xlApp);

                        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Datos exportados correctamente");
                    }
                }

            }
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                _ = System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
