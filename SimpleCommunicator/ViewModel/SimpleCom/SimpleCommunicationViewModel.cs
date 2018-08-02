using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleCommunicator.Views;
using SimpleCommunicator.Views.LoginScreen;

namespace SimpleCommunicator
{
    public class SimpleCommunicationViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ContentControl currentContext = new ContentControl();

        public ContentControl CurrentContext
        {
            get { return currentContext; }
            set { currentContext = value; RaisePropertyChanged("CurrentContext"); }
        }

        public SimpleCommunicationViewModel() => SetUp();

        public void SetUp()
        {
            currentContext.Content = new LoginScreen();
        }

      


    }
}
