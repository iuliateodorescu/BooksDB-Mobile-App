using BooksDB.Models;
using BooksDB.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BooksDB.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            GoToRegisterCommand = new Command(OnRegisterClicked);
        }


        public async Task OnAppearing()
        {
            if(isAuthenticated)
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        private async void OnLoginClicked(object obj)
        {
            var userCredentials = new User { 
                Username = this.Username, 
                Password = this.Password };

            var userDetails = await UserManagement.AuthenticateUserAsync(userCredentials);
            user.UserId = userDetails.UserId;
            user.Username = userDetails.Username;
            user.FirstName = userDetails.FirstName;
            user.LastName = userDetails.LastName;
            user.Email = userDetails.Email;
            user.Password = userDetails.Password;
            await App.Current.MainPage.DisplayAlert("Message", "Authentication successful", "Close");
            isAuthenticated = true;
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnRegisterClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }

        #region Properties
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
        private string username;
        private string password;
        #endregion

        #region Commands
        public Command LoginCommand { get; }
        public Command GoToRegisterCommand { get; }
        #endregion
    }
}
