using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MauiAppKSMArt.Services;
using MauiAppKSMArt.Models;
using CommunityToolkit.Maui;

namespace MauiAppKSMArt.ViewsModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        ArtObjectRestService src = new ArtObjectRestService();

        public event PropertyChangedEventHandler? PropertyChanged;

        internal int _objectCount = 0;
        internal int _selectedItem = 0;

        internal ImageSource _source = ("dotnet_bot.png");

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

            //MyImageSource = "";
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

            //MyImageSource = "";
            SelectedArtObject = _artObjects[_selectedItem];
            MyImageSource = SelectedArtObject.Location;
        }

    
        internal async void MpLoadClick()
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
