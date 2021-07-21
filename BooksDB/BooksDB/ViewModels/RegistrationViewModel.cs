using BooksDB.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using BooksDB.Models;

namespace BooksDB.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel()
        {
            RegistrationCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            user.Username = Username;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            user.Password = Password;

            await UserManagement.RegisterUserAsync(user);
            await App.Current.MainPage.DisplayAlert("Message", "Registration successful", "Close");

            var userCredentials = new User
            {
                Username = this.Username,
                Password = this.Password
            };

            var userDetails = await UserManagement.AuthenticateUserAsync(userCredentials);
            user.UserId = userDetails.UserId;
            isAuthenticated = true;
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        #region Properties
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                SetProperty(ref firstName, value);
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                SetProperty(ref lastName, value);
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                SetProperty(ref username, value);
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetProperty(ref email, value);
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value);
            }
        }
        #endregion

        #region Fields
        private string firstName;

        private string lastName;

        private string username;

        private string email;

        private string password;
        #endregion

        #region Commands
        public Command RegistrationCommand { get; }
        #endregion
    }
}
