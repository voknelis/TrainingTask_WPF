using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrainingTask.Helpers;
using TrainingTask.Model;
using TrainingTask.View;

namespace TrainingTask.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        IFrameNavigationService _navigationService;
        IReader<Project> reader;

        public bool _lockSaveButton;
        private bool lockInputData;

        private ObservableCollection<Project> projectList;
        private Project selectedProject;

        public bool LockSaveButton
        {
            get => !_lockSaveButton;
            set
            {
                _lockSaveButton = value;
                RaisePropertyChanged(() => LockSaveButton);
            }
        }
        public bool LockInputData
        {
            get { return !lockInputData; }
            set { lockInputData = value; RaisePropertyChanged(() => LockInputData); }
        }

        public ObservableCollection<Project> ProjectList
        {
            get => projectList;
            set
            {
                projectList = value;
                RaisePropertyChanged(() => ProjectList);
            }
        }
        public Project SelectedProject
        {
            get => selectedProject;
            set { selectedProject = value; RaisePropertyChanged(() => SelectedProject); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService"></param>
        public ProjectViewModel(IFrameNavigationService navigationService, IReader<Project> reader)
        {
            _navigationService = navigationService;
            this.reader = reader;
            ProjectList = new ObservableCollection<Project>();

            //reader = new JsonReader<Project>();

            LockSaveButton = true;
            lockInputData = true;
        }

        /// <summary>
        /// Save data 
        /// </summary
        private void SaveData()
        {
            #region Old method
            /*if (SelectedProject.Type == null)
            {
                MessageBox.Show("Project type is mandatory!","Error",MessageBoxButton.OK);
                return;
            }
            if (ProjectList != null)
            {
                var jsonProjs = JsonConvert.SerializeObject((IEnumerable<Project>)ProjectList);

                string filePath = GetFilePath();
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(jsonProjs);
                }
            }*/
            #endregion

            reader.Write(ProjectList);
            LockSaveButton = true;
        }

        /// <summary>
        /// Load data from reader
        /// </summary>
        public IEnumerable<Project> LoadData()
        {
            #region Old method
            /*string filePath = GetFilePath();

            string readedProjects;
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    readedProjects = sr.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }

            LockInputData = false;
            //var _projects = JsonConvert.DeserializeObject<RootJson>(readedProjects).Projects;
            var _projects = JsonConvert.DeserializeObject<List<Project>>(readedProjects);*/
            #endregion

            // disable the fiels
            LockInputData = false;

            var prjs = reader.Read();
            LockSaveButton = false;

            ProjectList = new ObservableCollection<Project>(prjs);
            return ProjectList;
        }


        /// <summary>
        /// Add new element in ProjectList
        /// </summary>
        private void AddElement()
        {
            Project newProj = new Project() { Name = "New Project"};

            ProjectList.Add(newProj);
            SelectedProject = newProj;

            LockInputData = false;

            RaisePropertyChanged(() => ProjectList);

        }


        private string GetFilePath()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string usersFileName = Properties.Settings.Default.ProjectsFileName;
            return Path.Combine(docPath, usersFileName);
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
                    async () =>
                    {
                        if (ProjectList.Count == 0)
                            LoadData();
                        else
                        {
                            // if we have some data, ask before load it
                            ConfDialogHlp dialog = new ConfDialogHlp();
                            var result = await dialog.Show<ConfirmationDialogView, string>("Load new data?", "RootDialog");

                            if ((bool)result)
                                LoadData();

                        }
                    }));
            }
        }


        private RelayCommand _saveWindowsCommand;
        public RelayCommand SaveWindowsCommand
        {
            get
            {
                return _saveWindowsCommand
                    ?? (_saveWindowsCommand = new RelayCommand(
                    async () =>
                    {
                        ConfDialogHlp dialog = new ConfDialogHlp();
                        var result = await dialog.Show<ConfirmationDialogView, string>("Save changes?", "RootDialog");

                        if ((bool)result)
                            SaveData();

                    }));
            }
        }


        private RelayCommand _addElementsCommand;
        public RelayCommand AddElementsCommand
        {
            get
            {
                return _addElementsCommand
                    ?? (_addElementsCommand = new RelayCommand(
                    () =>
                    {
                        AddElement();
                        LockSaveButton = false;
                    }));
            }
        }

        private RelayCommand _selectedProjectTypeCommand;
        public RelayCommand SelectedProjectTypeCommand
        {
            get
            {
                return _selectedProjectTypeCommand
                    ?? (_selectedProjectTypeCommand = new RelayCommand(
                    () =>
                    {
                        RaisePropertyChanged(() => SelectedProject);
                    }));
            }
        }


        private RelayCommand<bool> _selectedVisibleCommand;
        public RelayCommand<bool> SelectedVisibleCommand
        {
            get
            {
                return _selectedVisibleCommand
                    ?? (_selectedVisibleCommand = new RelayCommand<bool>(
                    (isChecked) =>
                    {
                        if (isChecked)
                            SelectedProject.Type = ProjectType.External;
                        else
                        {
                            //SelectedProject.Type = ProjectType.Intern | ProjectType.Unknown;
                            if (SelectedProject.Type != ProjectType.Intern)
                                SelectedProject.Type = ProjectType.Unknown;
                        }

                        RaisePropertyChanged(() => SelectedProject);
                    }));
            }
        }


        private RelayCommand<string> _selectedProjectKnownTypeCommand;
        public RelayCommand<string> SelectedProjectKnownTypeCommand
        {
            get
            {
                return _selectedProjectKnownTypeCommand
                    ?? (_selectedProjectKnownTypeCommand = new RelayCommand<string>(
                    (e) =>
                    {
                        var t = (KnownProjectType)Enum.Parse(typeof(KnownProjectType), e);
                        if (t == KnownProjectType.No)
                            SelectedProject.Type = ProjectType.Unknown;
                        else
                        {
                            // SelectedProject.Type = ProjectType.Intern | ProjectType.External 
                            if (SelectedProject.Type != ProjectType.Intern)
                                SelectedProject.Type = ProjectType.External;
                        }

                        RaisePropertyChanged(() => SelectedProject);
                    }));
            }
        }


        private RelayCommand<string> _selectedPossibleRoleCommand;
        public RelayCommand<string> SelectedPossibleRoleCommand
        {
            get
            {
                return _selectedPossibleRoleCommand
                    ?? (_selectedPossibleRoleCommand = new RelayCommand<string>(
                    (e) =>
                    {
                        var currType = SelectedProject.Type;
                        ProjectType? newType = null;

                        var t = (UserRole)Enum.Parse(typeof(UserRole), e);
                        var r = SelectedProject.PossibleRoles;
                        if (r == null)
                            r = new List<UserRole>();

                        if (r.Contains(t))
                        {
                            // if mark was up
                            r.Remove(t);
                        }
                        else
                        {
                            // if mark settle down
                            r.Add(t);
                        }

                        if (r.Count == 1 && r.Contains(UserRole.Owner))
                            newType = ProjectType.Intern;

                        if (r.Count == 3 && r.Contains(UserRole.Owner) && r.Contains(UserRole.Editor) && r.Contains(UserRole.Reader))
                            newType = ProjectType.External;

                        if (r.Count == 2 && r.Contains(UserRole.Owner) && r.Contains(UserRole.Guest))
                            newType = ProjectType.Unknown;
                        /*if (t == UserRole.Owner)
                        {
                            if (r.Contains(UserRole.Owner))
                            {
                                // if mark was up
                                SelectedProject.Type = null;
                            }
                            else
                            {
                                // if mark settle down
                                if (r.Count == 0)
                                    newType = ProjectType.Intern;
                            }
                        }

                        if (t == UserRole.Editor || t == UserRole.Reader)
                        {
                            if (r.Contains(UserRole.Owner) && r.Contains(UserRole.Editor) && r.Contains(UserRole.Reader))
                            {
                                newType = ProjectType.External;
                            }
                            else
                                newType = null;
                        }

                        if (t == UserRole.Guest)
                        {
                            if (r.Contains(UserRole.Owner))
                                SelectedProject.Type = ProjectType.Unknown;
                            else
                                SelectedProject.Type = null;
                        }*/

                        // update unly if type is changed
                        if (newType != currType)
                        {
                            SelectedProject.Type = newType;
                            RaisePropertyChanged(() => SelectedProject);
                            
                        }
                        

                    }));
            }
        }

    }
}
