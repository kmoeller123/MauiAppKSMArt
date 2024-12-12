using KSMWebApi.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiAppKSMArt.ViewsModels
{
    public class UploadBlobToAzureViewModel : INotifyPropertyChanged
    {
        ArtObject _artObject;
        bool _loadisEnabled = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand PickImagesBlobButtonClick { get; private set; }
        public ICommand UploadImagesBlobButtonClick { get; private set; }

        public UploadBlobToAzureViewModel()
        {
            PickImagesBlobButtonClick = new Command(MpPickClick);
            UploadImagesBlobButtonClick = new Command(MpLoadClick);
           
        }

        private async void MpLoadClick(object obj)
        {
            AzureFilesService afs = new AzureFilesService();
            await afs.UploadFileAsync("");
        }

        private async void MpPickClick()
        {
            var images = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Image",
                FileTypes = FilePickerFileType.Images
               
            });
            if (images == null) return;

            var filePath = images.FullPath.ToString();          
        }

        public ArtObject artObject
        {
            get => _artObject;

            set
            {
                _artObject = value;
                OnPropertyChanged();
            }
        }

        public bool loadIsEnabled
        {
            get => _loadisEnabled;

            set
            {
                _loadisEnabled = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
