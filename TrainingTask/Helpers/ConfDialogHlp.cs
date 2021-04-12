using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTask.Helpers
{
    public class ConfDialogHlp
    {
        public async Task<object> Show<T,I>(I parameter, object dialogIdentifyer) where T : class
        {
            var view = CreateInstance<T>(typeof(T));
            // send to the view message into MessageBox
            Messenger.Default.Send<I>(parameter);

            // await for result
            var result = await DialogHost.Show(view, "RootDialog");
            return result;
        }


        private T CreateInstance<T>(Type type) where T : class
        {
            Type t = typeof(T);
            return Activator.CreateInstance(typeof(T)) as T;
        }
    }
}
