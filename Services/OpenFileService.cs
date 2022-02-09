using ECN.Contracts.Services;
using Microsoft.Win32;


namespace ECN.Services
{
    public class OpenFileService : IOpenFileService
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() == true)
            {
                FileName = file.SafeFileName;
                Path = file.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog(string filename)
        {
            SaveFileDialog file = new SaveFileDialog
            {
                FileName = filename,
                Title = "Guardar adjunto",
                Filter = "Todos los archivos (*.*)|*.*"
            };

            if (file.ShowDialog() == true)
            {
                Path = file.FileName;
                return true;
            }
            return false;
        }
    }
}
