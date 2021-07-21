using BooksDB.Interfaces;
using BooksDB.Models;
using BooksDB.Services;
using BooksDB.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BooksDB.ViewModels
{
    class LibraryViewModel : BaseViewModel
    {

        public LibraryViewModel()
        {
            Title = "Library";
            LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());
            ShowBookDetailsCommand = new Command(async () => await ExecuteShowBookDetailsCommand());
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadBooksCommand()
        {
            IsBusy = true;

            Books.Clear();
            var items = await UserManagement.GetUserLibraryAsync(user.UserId);
            foreach (LibraryItem item in items)
            {
                var book = await Bookstore.GetDetailsAsync(item.BookId);
                Books.Add(book);
            }

            IsBusy = false;
        }

        async Task ExecuteShowBookDetailsCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?{nameof(BookDetailsViewModel.BookId)}={SelectedBook.BookId}");
        }


        #region Properties
        public ObservableCollection<Book> Books
        {
            get
            {
                return books;
            }
            set
            {
                SetProperty(ref books, value);
            }
        }

        public Book SelectedBook
        {
            get
            {
                return selectedBook;
            }
            set
            {
                SetProperty(ref selectedBook, value);
            }
        }
        #endregion

        #region Fields
        private ObservableCollection<Book> books = new ObservableCollection<Book>();
        private Book selectedBook;
        #endregion

        #region Commands
        public Command LoadBooksCommand { get; }
        public Command ShowBookDetailsCommand { get; }
        #endregion
    }
}
