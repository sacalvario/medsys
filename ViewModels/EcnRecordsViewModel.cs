using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

using Excel = Microsoft.Office.Interop.Excel;

namespace ECN.ViewModels
{
    public class EcnRecordsViewModel : ViewModelBase, INavigationAware
    {
        private readonly IEcnDataService _ecnDataService;
        private readonly INavigationService _navigationService;

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
        public EcnRecordsViewModel(IEcnDataService ecnDataService, INavigationService navigationService)
        {
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;

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

        public void ExportToExcel()
        {
            if (Records.Count > 0)
            {
                MessageBox.Show("AAA");
                // Displays a SaveFileDialog so the user can save the Image
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel File|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.FileName = "Mobile List";

                // If the User Clicks the Save Button then the Module gets executed otherwise it skips the scope
                if ((bool)saveFileDialog1.ShowDialog())
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

                        xlWorkSheet.Cells[rowCount, 1] = "Brand";
                        xlWorkSheet.Cells[rowCount, 2] = "OS";

                        rowCount++;

                        formatRange = xlWorkSheet.get_Range("a1"); formatRange.EntireRow.Font.Bold = true;
                        formatRange = xlWorkSheet.Range["a1"]; formatRange.Cells.HorizontalAlignment = HorizontalAlignment.Center;
                        formatRange = xlWorkSheet.get_Range("b1"); formatRange.EntireRow.Font.Bold = true;

                        foreach (var item in Records)
                        {
                            xlWorkSheet.Cells[rowCount, 1] = item.Id;
                            xlWorkSheet.Cells[rowCount, 2] = item.StartDate;

                            rowCount++;
                        }

                        formatRange = xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, rowCount - 1]]; formatRange.AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);

                        xlWorkSheet.Columns.AutoFit();

                        // If the file name is not an empty string open it for saving.
                        if (!String.IsNullOrEmpty(saveFileDialog1.FileName.ToString()) && !string.IsNullOrWhiteSpace(saveFileDialog1.FileName.ToString()))
                        {
                            xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                            xlWorkBook.Close(true, misValue, misValue);
                            xlApp.Quit();

                            releaseObject(xlWorkSheet);
                            releaseObject(xlWorkBook);
                            releaseObject(xlApp);

                            MessageBox.Show("Excel File Exported Successfully", "Export Engine");
                        }

                    }

                }
            }
            else
            {
                MessageBox.Show("Nothing to Export", "Export Engine");
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
