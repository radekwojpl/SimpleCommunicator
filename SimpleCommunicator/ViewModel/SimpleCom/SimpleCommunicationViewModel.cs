using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleCommunicator.Views;
using SimpleCommunicator.Views.LoginScreen;
using SimpleCommunicator.Views.SingInScreen;

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

        #region LoginScreen

        private bool isUserLogIn;

        public bool IsUserLogIn
        {
            get { return isUserLogIn; }
            set { isUserLogIn = value; RaisePropertyChanged("IsUserLogIn"); }
        }


        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; RaisePropertyChanged("Login"); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged("Password"); }
        }


        public ICommand Submit
        {
            get
            {
                return new RelayCommand(ExecuteSubmit, CanExecuteSubmit);
            }
        }

        private bool CanExecuteSubmit()
        {
            return (Password != null || Login != null) ? true : false;

        }

        private void ExecuteSubmit()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-D7LMP5L;Initial Catalog=LoginDB;Integrated Security=True");

            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                    string query = "SELECT COUNT(1) FROM tbUser WHERE UserName=@UserName AND Password=@Password ";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@UserName", Login);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (count == 1)
                    {
                        currentContext.Content = new Communicator();
                        IsUserLogIn = true;
                    }
                    else
                    {
                        MessageBox.Show("Wrong Login or Password");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        #endregion

        #region SignInScreen

        private string signInLogin;
            
        public string SignInLogin
        {
            get { return signInLogin; }
            set { signInLogin = value; RaisePropertyChanged("SignInLogin"); }
        }

        private string signInPassword;

        public string SignInPassword
        {
            get { return signInPassword; }
            set { signInPassword = value; RaisePropertyChanged("SignInPassword"); }
        }

        private string signInPasswordToConfim;

        public string SignInPasswordToConfim
        {
            get { return signInPasswordToConfim; }
            set { signInPasswordToConfim = value; RaisePropertyChanged("SignInPasswordToConfim"); }
        }


        public ICommand SignIn
        {
            get { return new RelayCommand(ExecuteSignIn, CanExecuteSignIn); }
        }

        private void ExecuteSignIn()
        {
            currentContext.Content = new SignInScreen();

        }

        private bool CanExecuteSignIn()
        {
            return true;
        }

        public ICommand CreateAccount
        {
            get { return new RelayCommand(ExecuteCreateAccount, CanExecuteCreateAccount); }
        }

        private void ExecuteCreateAccount()
        {
           
        }

        private bool CanExecuteCreateAccount()
        {
            return true;
        }
        #endregion

    }
}
