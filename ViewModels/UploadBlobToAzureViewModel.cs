using CommunityToolkit.Maui.Views;
using MauiAppKSMArt.Models;
using MauiAppKSMArt.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace MauiAppKSMArt.ViewsModels
{
    public class UploadBlobToAzureViewModel : INotifyPropertyChanged
    {        
        bool _loadisEnabled = false;

        string _userName;
        string _title;
        string _price;
        

        string _alertButton;
        bool _alertButtonVisible = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand PickImagesBlobButtonClick { get; private set; }  
        public ICommand ValidateAllFields { get; private set; }
        public ICommand ResetForm { get; private set; }

        public UploadBlobToAzureViewModel()
        {
            PickImagesBlobButtonClick = new Command(MpPickClick);
            ValidateAllFields = new Command(ValidateFileds);
            ResetForm = new Command(MpResetForm);

            //artObject = new ArtObject();
            MyUserName = string.Empty;
            MyTitle = string.Empty;
            MyPrice = string.Empty;

            AlertButton = string.Empty;
        }

        private void MpResetForm(object obj)
        {
            AlertButtonVisible = false;
            loadIsEnabled = false;

            MyUserName = string.Empty;
            MyTitle = string.Empty;
            MyPrice = string.Empty;           
        }

        private void ValidateFileds(object obj)
        {
            
            loadIsEnabled = false;
           
            if (string.IsNullOrEmpty(MyUserName)) return;
            if (string.IsNullOrEmpty(MyTitle)) return;
            if (string.IsNullOrEmpty(MyPrice)) return;

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
            var fileName = Path.GetFileName(filePath);
            var strings = fileName.Split(".");

            decimal price = Convert.ToDecimal(MyPrice);

            ArtObject artObject = new ArtObject()
            {
                UserName = MyUserName,
                Title = MyTitle,
                Price = price,
                FileName = strings[0].Trim(),
                FileType = strings[1].Trim(),
                Location = @"https://ksmart123.blob.core.windows.net/ksmfiles123/" + fileName,
                Id = 0,
                LastViewed = null,
                Views = null
            };

            loadIsEnabled = false;

            AlertButton = "Successfully Uploaded,press to reset Form";

            //ArtObjectRestService aors = new ArtObjectRestService();
            //await aors.CreateArtObjectAsync(artObject);

            //AzureFilesService afs = new AzureFilesService();
            //await afs.UploadFileAsync(filePath);

            AlertButton = "Successfully Completed, press to reset Form";
            AlertButtonVisible = true;            
        }

        public string? MyUserName
        {
            get => _userName;

            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string? MyTitle
        {
            get => _title;

            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string? MyPrice
        {
            get => _price;

            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public string AlertButton
        {
            get => _alertButton;

            set
            {
                _alertButton = value;
                OnPropertyChanged();
            }
        }

        public bool AlertButtonVisible
        {
            get => _alertButtonVisible;

            set
            {
                _alertButtonVisible = value;
                OnPropertyChanged();            
            }
        }

        //public ArtObject artObject
        //{
        //    get => _artObject;

        //    set
        //    {
        //        _artObject = value;
        //        OnPropertyChanged();
        //    }
        //}

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
