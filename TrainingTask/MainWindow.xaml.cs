using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainingTask.ViewModel;

namespace TrainingTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var locator = (ViewModelLocator)Application.Current.Resources["Locator"];
            locator.Navigation.OnNavigateTo += Navigation_OnNavigateTo;
        }

        // Start 
        private void Navigation_OnNavigateTo(string pageName)
        {
            if (pageName == "Home")
            {
                var anim = (Storyboard)this.Resources["ShowSettingsBar"];
                anim.Begin();
            }
            else
            {
                var anim = (Storyboard)this.Resources["HideSettingsBar"];
                anim.Begin();
            }
        }
    }
}
