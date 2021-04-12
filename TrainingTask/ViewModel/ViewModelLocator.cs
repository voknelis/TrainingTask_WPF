/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TrainingTask"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using TrainingTask.Helpers;
using TrainingTask.Model;
//using Microsoft.Practices.ServiceLocation;

namespace TrainingTask.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            // Registerigng viewmodels
            SimpleIoc.Default.Register<IReader<User>, XmlReader<User>>();
            SimpleIoc.Default.Register<IReader<Project>, JsonReader<Project>>();

            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UsersViewModel>();
            SimpleIoc.Default.Register<ProjectViewModel>();
            SimpleIoc.Default.Register<ProjectsAndUsersViewModel>();
            SimpleIoc.Default.Register<ConfirmationDialogViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SetupNavigation();

        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Home", new Uri("../View/HomeView.xaml", UriKind.Relative));
            navigationService.Configure("Users", new Uri("../View/UsersView.xaml", UriKind.Relative));
            navigationService.Configure("Projects", new Uri("../View/ProjectsView.xaml", UriKind.Relative));
            navigationService.Configure("ProjectsUsers", new Uri("../View/ProjectsUsersView.xaml", UriKind.Relative));
            navigationService.Configure("Settings", new Uri("../View/SettingsView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        public IFrameNavigationService Navigation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IFrameNavigationService>();
            }
        }


        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public UsersViewModel Users
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UsersViewModel>();
            }
        }

        public ProjectViewModel Projects
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectViewModel>();
            }
        }

        public ProjectsAndUsersViewModel ProjectsUsers
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectsAndUsersViewModel>();
            }
        }

        public ConfirmationDialogViewModel ConfDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfirmationDialogViewModel>();
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}