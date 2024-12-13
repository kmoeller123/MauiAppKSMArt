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

        public ICommand ValidateAllFields { get; private set; }


        public UploadBlobToAzureViewModel()
        {
            PickImagesBlobButtonClick = new Command(MpPickClick);
            ValidateAllFields = new Command(ValidateFileds);

            _artObject = new ArtObject();
         
        }

        private void ValidateFileds(object obj)
        {
            loadIsEnabled = false;

            if (artObject == null) return;
            if (string.IsNullOrEmpty(artObject.UserName)) return;
            if (string.IsNullOrEmpty(artObject.Title)) return;
            if (artObject.Price is null || artObject.Price <=9 ) return;

            loadIsEnabled = true;          
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

            AzureFilesService afs = new AzureFilesService();
            await afs.UploadFileAsync(filePath);
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
