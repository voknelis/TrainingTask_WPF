using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TrainingTask.Helpers;
using TrainingTask.Model;

namespace TrainingTask.ViewModel
{
    public class ProjectsAndUsersViewModel:ViewModelBase
    {
        IFrameNavigationService _navigationService;

        private ObservableCollection<Project> projectsSuitable;
        private ObservableCollection<Project> projectsList;
        private ObservableCollection<User> usersList;

        public ObservableCollection<Project> ProjectsList { get => projectsList; set { projectsList = value; RaisePropertyChanged(() => ProjectsList); } }
        public ObservableCollection<User> UsersList { get { return usersList; } set { usersList = value; RaisePropertyChanged(() => UsersList); } }
        public ObservableCollection<Project> ProjectsSuitable { get => projectsSuitable; set { projectsSuitable = value; RaisePropertyChanged(() => ProjectsSuitable); } }

        public User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set { selectedUser = value; RaisePropertyChanged(() => SelectedUser); }
        }

        public ProjectsAndUsersViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
            UsersList = new ObservableCollection<User>();
            ProjectsList = new ObservableCollection<Project>();
        }


        public void LoadUsersData()
        {
            UsersViewModel vm = new UsersViewModel(null, new XmlReader<User>());
            var users = vm.LoadData();

            UsersList = new ObservableCollection<User>(users);
        }


        public void LoadProjectsData()
        {
            ProjectViewModel vm = new ProjectViewModel(null, new JsonReader<Project>());
            var projects = vm.LoadData();

            ProjectsList = new ObservableCollection<Project>(projects);
        }

        // Find all Projects for selected User
        public IEnumerable<Project> FindSuitableProjects(User user)
        {
            var prs = from p in projectsList
                      where p.PossibleRoles.Contains(user.Role)
                      select p;
            return prs;
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

        private RelayCommand _loadWindowsCommand;
        public RelayCommand LoadWindowsCommand
        {
            get
            {
                return _loadWindowsCommand
                    ?? (_loadWindowsCommand = new RelayCommand(
                    () =>
                    {
                        LoadUsersData();
                        LoadProjectsData();
                    }));
            }
        }

        private RelayCommand _suitProjCommand;
        public RelayCommand SuitProjCommand
        {
            get
            {
                return _suitProjCommand
                    ?? (_suitProjCommand = new RelayCommand(
                    () =>
                    {
                        var prs = FindSuitableProjects(SelectedUser);
                        ProjectsSuitable = new ObservableCollection<Project>(prs);
                    }));
            }
        }
    }
}
