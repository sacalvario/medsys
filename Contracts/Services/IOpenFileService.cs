using System;
using System.Collections.Generic;
using System.Text;

namespace ECN.Contracts.Services
{
    public interface IOpenFileService
    {
        string FileName { get; set; }
        string Path { get; set; }
        bool OpenFileDialog();
        bool SaveFileDialog(string filename);
        bool SaveFileExportDialog();
    }
}
