using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiAppKSMArt.ViewsModels
{
    public class UploadBlobToAzureViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand UploadImagesBlobButtonClick { get; private set; }

        public UploadBlobToAzureViewModel()
        {
            UploadImagesBlobButtonClick = new Command(MpLoadClick);
           
        }

        private async void MpLoadClick()
        {
            var images = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Barcode/QR Code Image",
                FileTypes = FilePickerFileType.Images
            });
            if (images == null) return;

            var filePath = images.FullPath.ToString();

            AzureFilesService afs = new AzureFilesService();
            await afs.UploadFileAsync(filePath);
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
