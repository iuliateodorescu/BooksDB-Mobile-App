using BooksDB.Interfaces;
using BooksDB.Models;
using BooksDB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BooksDB.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IBookstore Bookstore = BookstoreService.Instance;
        protected readonly IUserManagement UserManagement = UserManagementService.Instance;

        protected UserDetails user = UserDetails.Instance;

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        #endregion

        #region Fields
        private string title = string.Empty;

        private bool isBusy = false;

        protected static bool isAuthenticated = false;
        #endregion
    }
}
