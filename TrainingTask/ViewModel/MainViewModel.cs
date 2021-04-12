using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using TrainingTask.Helpers;
using TrainingTask.View;
using TrainingTask.ViewModel;

namespace TrainingTask.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        IFrameNavigationService _navigationService;

        public MainViewModel(IFrameNavigationService navigationService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            _navigationService = navigationService;
            _navigationService.OnNavigateTo += _navigationService_OnNavigateTo;

            LoadedCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo("Home");
            });
        }

        // event for animation
        private void _navigationService_OnNavigateTo(string pageName)
        {

            if (pageName == "Home")
                // show up settings panel
                return;
            else
                // hide settings panel
                return;
        }

        public RelayCommand LoadedCommand { get; set; }


        private RelayCommand _backWindowsCommand;
        public RelayCommand BackWindowsCommand
        {
            get
            {
                return _backWindowsCommand
                    ?? (_backWindowsCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.GoBack();
                    }));
            }
        }

        private RelayCommand _settingsWindowsCommand;
        public RelayCommand SettingsWindowsCommand
        {
            get
            {
                return _settingsWindowsCommand
                    ?? (_settingsWindowsCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("Settings");
                    }));
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }

    }
}