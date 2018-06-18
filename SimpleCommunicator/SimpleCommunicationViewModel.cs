using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleCommunicator.TCP;

namespace SimpleCommunicator
{
    public class SimpleCommunicationViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged("Message");  }
            
        }

        private TCPListener tcpListener;

        



        public SimpleCommunicationViewModel() => SetUp();

        public void SetUp()
        {
            this.tcpListener = new TCPListener();
        }

        public ICommand Connect
        {
            get
            {
                return new RelayCommand(ExecuteConnect);
            }
        }
        private void ExecuteConnect()
        {
            TCPClient.Connect("127.0.0.5", "cos");
        }

        public ICommand Stop
        {
            get
            {
                return new RelayCommand(ExecuteStop);
            }
        }

        private void ExecuteStop()
        {
            tcpListener.Stop();
        }

        public ICommand Listen
        {
            get
            {
                return new RelayCommand(ExecuteListen);
            }
        }

        private void ExecuteListen()
        {

            Thread listen = new Thread(ExecuteListenHelper);
            listen.Start();


        }

        private void ExecuteListenHelper()
        {
            tcpListener.Start("127.0.0.5", 1300);
        }


    }
}
