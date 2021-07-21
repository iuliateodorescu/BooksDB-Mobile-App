using BooksDB.Services;
using BooksDB.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksDB
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockBookStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
