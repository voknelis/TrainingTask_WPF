using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTask.Helpers
{
    public delegate void NavigationEvent(string pageName);

    public interface IFrameNavigationService: INavigationService
    {
        object Parameter { get; }

        event NavigationEvent OnGoBack;

        event NavigationEvent OnNavigateTo;
    }
}
