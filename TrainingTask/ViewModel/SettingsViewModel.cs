using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTask.Helpers;

namespace TrainingTask.ViewModel
{
    public class SettingsViewModel:ViewModelBase
    {

        IFrameNavigationService _navigationService;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="navigationService"></param>
        public SettingsViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string PathToUsers
        {
            get => Properties.Settings.Default.UsersFileName;
            set
            {
                Properties.Settings.Default.UsersFileName = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => PathToUsers);
            }
        }

        public string PathToProjects
        {
            get => Properties.Settings.Default.ProjectsFileName;
            set
            {
                Properties.Settings.Default.ProjectsFileName = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => PathToProjects);
            }
        }

        public string ResoursesPath
        {
            get => Properties.Settings.Default.ResoursesPath;
            set
            {
                Properties.Settings.Default.ResoursesPath = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => ResoursesPath);
            }
        }


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

    }
}
