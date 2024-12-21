namespace MauiAppKSMArt
{
    using MauiAppKSMArt.Services;
    using Microsoft.Maui.Controls.Xaml;
    using Microsoft.Maui.Hosting;
    using CommunityToolkit.Maui;
    using MauiAppKSMArt.Models;
    using System.ComponentModel;

    public partial class MainPage : ContentPage
    {
        IDispatcherTimer _myTimerOpen;
        IDispatcherTimer _myTimerClose;
        bool _myOpactityRunning = false;
        bool _MpLoaded = false;

        bool FirstIsLoading = true;

        public MainPage()
        {
            InitializeComponent();
            
            this.Loaded += MainPage_Loaded;
                       
        }

        private void MainPage_Loaded(object? sender, EventArgs e)
        {
            _myTimerOpen = Application.Current.Dispatcher.CreateTimer();
            _myTimerOpen.Interval = TimeSpan.FromSeconds(.1);
            _myTimerOpen.Tick += _myTimer_TickOpen;

            _myTimerClose = Application.Current.Dispatcher.CreateTimer();
            _myTimerClose.Interval = TimeSpan.FromSeconds(.1);
            _myTimerClose.Tick += _myTimer_TickClose;

            _MpLoaded  = true;

            FirstIsLoading = true;
        }

        private void _myTimer_TickClose(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {                
                myImage.Opacity = myImage.Opacity - .15;

                if (myImage.Opacity <= 0)
                {
                    myImage.Opacity = 0;

                    _myTimerClose.Stop();

                    return;
                }
                
            });
        }

        private void _myTimer_TickOpen(object? sender, EventArgs e)
        {
                    
            MainThread.BeginInvokeOnMainThread(() =>
            {

                //if (FirstIsLoading && myImage.Opacity != 0)
                //{                  
                //    myImage.Opacity = myImage.Opacity -.15;

                //    if (myImage.Opacity <= 0 )
                //    {
                //        myImage.Opacity = 0;
                //        _myOpactityRunning = false;

                //        FirstIsLoading = false;                      

                //        return;
                //    }
                //}
                //else 
                //{

                    myImage.Opacity = myImage.Opacity +.15;
                    if (myImage.Opacity >= 1)
                    {
                        myImage.Opacity = 1;                     
                        //FirstIsLoading = true;
                        _myTimerOpen.Stop();
                    }
                //}        

            });

        }

        private void myImage_PropertyChanging(object sender, Microsoft.Maui.Controls.PropertyChangingEventArgs e)
        {
            if (!_MpLoaded) return;

            if (e.PropertyName == "IsLoading" && FirstIsLoading)           
            {
                FirstIsLoading = false;
                return;
            }

            if(e.PropertyName == "Source")
            {
                _myTimerClose.Start();  
            }

            if (e.PropertyName == "IsLoading")
            {
                //myImage.Opacity = 0;

                //_myOpactityRunning = true;

                _myTimerOpen.Start();

                FirstIsLoading = true;

            }
        }
    }

}
