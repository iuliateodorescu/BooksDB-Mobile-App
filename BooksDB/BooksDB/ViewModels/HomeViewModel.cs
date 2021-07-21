using BooksDB.Interfaces;
using BooksDB.Models;
using BooksDB.Services;
using BooksDB.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BooksDB.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "Home";
            LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());
            ShowBookDetailsCommand = new Command(async () => await ExecuteShowBookDetailsCommand());
            SearchCommand = new Command( async () => await ExecuteSearchCommand());
            LogOutCommand = new Command( async () => await ExecuteLogOutCommand() );
            AddToLibraryCommand = new Command( async (obj) => await ExecuteAddToLibraryCommand(obj) );
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadBooksCommand()
        {
            IsBusy = true;

            Books.Clear();
            var books = await Bookstore.GetBooksAsync();
            foreach( Book book in books )
            {
                Books.Add(book);
            }

            IsBusy = false;
        }

        async Task ExecuteShowBookDetailsCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?{nameof(BookDetailsViewModel.BookId)}={SelectedBook.BookId}");
        }

        async Task ExecuteSearchCommand()
        {
            var queryResult = await Bookstore.GetBooksByTitleAsync(QueryText);
            Books.Clear();
            foreach(Book book in queryResult)
            {
                Books.Add(book);
            }
        }

        async Task ExecuteLogOutCommand()
        {
            isAuthenticated = false;
            user.ClearUser();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        async Task ExecuteAddToLibraryCommand(object obj)
        {
            var swipedBook = obj as Book;
            var item = new LibraryItem
            {
                BookId = swipedBook.BookId,
            };
            var status = await UserManagement.AddToLibrary(user.UserId, item);
            if (!status)
            {
                await App.Current.MainPage.DisplayAlert("Message", "You already have this book in your library.", "Close");
            }
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

        public string QueryText
        {
            get
            {
                return queryText;
            }
            set
            {
                SetProperty(ref queryText, value);
            }
        }
        #endregion

        #region Fields
        private ObservableCollection<Book> books = new ObservableCollection<Book>();
        private Book selectedBook;
        private string queryText;
        #endregion

        #region Commands
        public Command LoadBooksCommand { get; }
        public Command ShowBookDetailsCommand { get; }
        public Command SearchCommand { get; }
        public Command LogOutCommand { get; }
        public Command AddToLibraryCommand { get; }
        #endregion
    }
}
