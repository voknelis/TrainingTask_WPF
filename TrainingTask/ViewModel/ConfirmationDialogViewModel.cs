using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTask.ViewModel
{
    public class ConfirmationDialogViewModel : ViewModelBase
    {
        public ConfirmationDialogViewModel()
        {
            // Register a message to fill the a text in confirmation view
            Messenger.Default.Register<string>(this,
                (message) => 
                {
                    ChangesValue = message;
                });
        }

        private string _changesValue;

        public string ChangesValue
        {
            get { return _changesValue; }
            set
            {
                _changesValue = value;
                Set(()=> ChangesValue,ref _changesValue, value);
                RaisePropertyChanged(() => ChangesValue);
            }
        }


    }
}
