using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrainingTask.View;
using TrainingTask.Helpers;

namespace TrainingTask.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        IFrameNavigationService _navigationService;

        public HomeViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;

        }


        private RelayCommand _openUsersCommand;
        public RelayCommand OpenUsersCommand
        {
            get
            {
                return _openUsersCommand
                    ?? (_openUsersCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("Users");
                    }));
            }
        }


        private RelayCommand _openProjectsCommand;
        public RelayCommand OpenProjectsCommand
        {
            get
            {
                return _openProjectsCommand
                    ?? (_openProjectsCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("Projects");
                    }));
            }
        }


        private RelayCommand _openProjectsUsersCommand;
        public RelayCommand OpenProjectsUsersCommand
        {
            get
            {
                return _openProjectsUsersCommand
                    ?? (_openProjectsUsersCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("ProjectsUsers");
                    }));
            }
        }

    }
}

