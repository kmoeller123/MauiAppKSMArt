using MauiAppKSMArt.Models;
using MauiAppKSMArt.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiAppKSMArt.ViewsModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        int PropertyChangedCount = 0; 
        ArtObjectRestService src = new ArtObjectRestService();

        public event PropertyChangedEventHandler? PropertyChanged;

        internal int _objectCount = 0;
        internal int _selectedItem = 0;

        internal ImageSource _source;      

        internal List<ArtObject> _artObjects = new List<ArtObject>();
        internal ArtObject _selectedArtObject = new ArtObject();
        internal bool _isEnabled = false;



        public ICommand MainPageGetButtonClick { get; private set; }
        public ICommand MainPagePrevButtonClick { get; private set; }
        public ICommand MainPageNextButtonClick { get; private set; }


        public MainPageViewModel()
        {
            MainPageGetButtonClick = new Command(MpLoadClick);
            MainPagePrevButtonClick = new Command(MpPrevClick);
            MainPageNextButtonClick = new Command(MpNextClick);
        }       

        internal void MpPrevClick()
        {
            if (_artObjects == null || _artObjects.Count == 0) return;

            if (_selectedItem <= 0) _selectedItem = _artObjects.Count -1;
            else
            {
                _selectedItem = _selectedItem - 1;
            }
         
            SelectedArtObject = _artObjects[_selectedItem];
            MyImageSource = SelectedArtObject.Location;         
        }

        internal void MpNextClick()
        {
            if (_artObjects == null || _artObjects.Count == 0) return;

            if (_selectedItem == _objectCount -1) _selectedItem = 0;
            else
            {
                _selectedItem = _selectedItem + 1;
            }

            SelectedArtObject = _artObjects[_selectedItem];
            MyImageSource = SelectedArtObject.Location;           
           
        }
      
        internal async void MpLoadClick(object obj)
        {
           var result = await src.GetDataAsync();
            if (result != null && result.Count >0)
            {               
                SelectedArtObject = result[0];
                _objectCount = result.Count;
                MyArtObjects = result;

                MyImageSource = result[0].Location;
             
                _selectedItem = 0;

                isEnabled = true;
            }
        }

        internal async Task<string> ImageClose(Image img)
        {
            //await img.RotateYTo(360, 1500);
            await img.FadeTo(0, 2000, null);
            //await img.ScaleYTo(0, 2000, null);

            return "ok";
        }

        internal async Task<string> ImageOpen(Image img)
        {
            //await img.RotateYTo(360, 1500);
            await img.FadeTo(1, 2000, null);
            //await img.ScaleYTo(1, 1000, null);

            return "ok";
        }

        public ArtObject SelectedArtObject
        {
            get => _selectedArtObject;
            set
            {
                if (_selectedArtObject != value)
                {
                    _selectedArtObject = value;
                    OnPropertyChanged();
                }
            }
        }

        public ImageSource MyImageSource
        {
            get => _source;
            set
            {
                if (_source != value) 
                {
                    _source = value;
                    OnPropertyChanged();
                }
            }

        }        

        public List<ArtObject> MyArtObjects
        {
            get => _artObjects;
            set
            {
                _artObjects = value;
                OnPropertyChanged();
            }
        }

        public bool isEnabled
        {
            get => _isEnabled;

            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }



        public void OnPropertyChanged([CallerMemberName] string name = "") =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
