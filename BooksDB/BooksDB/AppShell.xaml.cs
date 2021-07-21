using BooksDB.ViewModels;
using BooksDB.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BooksDB
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BookDetailsPage), typeof(BookDetailsPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            //await Shell.Current.GoToAsync("//LoginPage");
        }

    }
}
