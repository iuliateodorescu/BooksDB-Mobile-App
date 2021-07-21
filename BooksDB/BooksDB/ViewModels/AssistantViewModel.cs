using BooksDB.Models;
using BooksDB.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BooksDB.ViewModels
{
    class AssistantViewModel : BaseViewModel
    {
        public AssistantViewModel()
        {
            Title = "Assistant";
            GetRecommendationsCommand = new Command(async () => await ExecuteGetRecommendationsCommand());
            ShowBookDetailsCommand = new Command(async () => await ExecuteShowBookDetailsCommand());
            AddToLibraryCommand = new Command(async (obj) => await ExecuteAddToLibraryCommand(obj));
            Books = new ObservableCollection<Book>();
        }

        public void OnAppearing()
        {
            
        }

        async Task ExecuteGetRecommendationsCommand()
        {
            var queryResult = await Bookstore.GetRecommendations(StartingTitle);
            Books.Clear();
            foreach (Book book in queryResult)
            {
                Books.Add(book);
            }
        }

        async Task ExecuteShowBookDetailsCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?{nameof(BookDetailsViewModel.BookId)}={SelectedBook.BookId}");
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

        public string StartingTitle 
        {
            get
            {
                return startingTitle;
            }
            set
            {
                SetProperty(ref startingTitle, value);
            }
        }
        #endregion

        #region Fields
        private ObservableCollection<Book> books;
        private Book selectedBook;
        private string startingTitle;
        #endregion

        #region Commands
        public Command GetRecommendationsCommand { get; }
        public Command ShowBookDetailsCommand { get; }
        public Command AddToLibraryCommand { get; }
        #endregion
    }
}
