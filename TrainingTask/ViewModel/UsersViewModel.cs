using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using TrainingTask.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using TrainingTask.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using TrainingTask.Helpers;
using System.Threading;

namespace TrainingTask.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        IFrameNavigationService _navigationService;
        IReader<User> reader;

        private ObservableCollection<User> usersList;
        private Visibility fileNotFoundError;
        private bool _lockSaveButton;
        private bool _isInProcess;
        private bool _isInProcessTemp ;

        public ObservableCollection<User> UsersList { get { return usersList; } set { usersList = value; RaisePropertyChanged(() => UsersList); } }

        public Visibility FileNotFoundError { get => fileNotFoundError; set { fileNotFoundError = value; RaisePropertyChanged(() => FileNotFoundError); } }
        public bool LockSaveButton { get => !_lockSaveButton; set { _lockSaveButton = value; RaisePropertyChanged(() => LockSaveButton); } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService"></param>
        public UsersViewModel(IFrameNavigationService navigationService, IReader<User> reader)
        {
            _navigationService = navigationService;
            this.reader = reader;
            //reader = new XmlReader<User>(new System.Xml.Serialization.XmlRootAttribute("Users"));*/

            UsersList = new ObservableCollection<User>();

            FileNotFoundError = Visibility.Collapsed;
            LockSaveButton = true;
        }

        /// <summary>
        /// Load data from file
        /// </summary>
        public IEnumerable<User> LoadData()
        {
            #region Old method
            /*string filePath = GetFilePath();

            try
            {
                xDoc = XDocument.Load(filePath);
                FileNotFoundError = Visibility.Collapsed;
            }
            catch (FileNotFoundException)
            {
                UsersList = new ObservableCollection<User>();
                FileNotFoundError = Visibility.Visible;
                return null;
            }


            var items = from xe in xDoc.Element("Users").Elements("User")
                        let u = GetNewUser(xe)
                        where u != null
                        select u;
                        */
            #endregion

            var items = reader.Read();
            UsersList = new ObservableCollection<User>(items);

            return UsersList;
        }


        /// <summary>
        /// Save data in the file
        /// </summary>
        private void SaveData()
        {
            #region Old method
            /*string filePath = GetFilePath();

            xDoc.Element("Users").RemoveAll();
            foreach (var item in UsersList)
            {
                xDoc.Element("Users").Add(new XElement("User",
                    new XElement("Name", item.Name),
                    new XElement("Age", item.Age),
                    new XElement("Role", item.Role)));
            }

            xDoc.Save(filePath);*/
            #endregion

            reader.Write(UsersList);

            LockSaveButton = true;
        }

        #region Old non-usable methods
        /*
        private string GetFilePath()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string usersFileName = Properties.Settings.Default.UsersFileName;
            return Path.Combine(docPath, usersFileName);
        }

        private User GetNewUser(XElement xe)
        {
            string name = string.Empty;
            uint age = default;
            UserRole role = default;
            try
            {
                name = xe.Element("Name").Value;
                age = Convert.ToUInt32(xe.Element("Age").Value);
                role = (UserRole)Enum.Parse(typeof(UserRole), xe.Element("Role").Value);
            }
            catch (Exception)
            {
                return null;
            }

            return new User() { Name = name, Age = age, Role = role };
        }
        */
        #endregion

        private void CellEditEnding()
        {
            // unlock Save button
            LockSaveButton = false;

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
                        if (UsersList.Count == 0)
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

        // Event on editing DataGrid
        private RelayCommand _cellEditEndingCommand;
        public RelayCommand CellEditEndingCommand
        {
            get
            {
                return _cellEditEndingCommand
                    ?? (_cellEditEndingCommand = new RelayCommand(
                    () =>
                    {
                        CellEditEnding();
                    }));
            }
        }
    }
}
