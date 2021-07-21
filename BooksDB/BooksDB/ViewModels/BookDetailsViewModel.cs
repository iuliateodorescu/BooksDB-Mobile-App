using BooksDB.Interfaces;
using BooksDB.Models;
using BooksDB.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BooksDB.ViewModels
{
    [QueryProperty(nameof(BookId), nameof(BookId))]
    public class BookDetailsViewModel : BaseViewModel
    {
        public BookDetailsViewModel()
        {
        }


        async void LoadBookDetailsAsync(int bookId)
        {
            var details = await Bookstore.GetDetailsAsync(bookId);
            ImageUrl = details.ImageUrl;
            BookTitle = details.Title;
            Author = details.Author;
            EditionYear = details.EditionYear;
            Publisher = details.Publisher;
            Description = details.Description;
        }

        #region Properties
        public int BookId
        {
            get
            {
                return bookId;
            }
            set
            {
                SetProperty(ref bookId, value);
                LoadBookDetailsAsync(value);
            }
        }

        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set
            {
                SetProperty(ref imageUrl, value);
            }
        }

        public string BookTitle
        {
            get
            {
                return bookTitle;
            }
            set
            {
                SetProperty(ref bookTitle, value);
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                SetProperty(ref author, value);
            }
        }

        public string EditionYear
        {
            get
            {
                return editionYear;
            }
            set
            {
                SetProperty(ref editionYear, value);
            }
        }

        public string Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                SetProperty(ref publisher, value);
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                SetProperty(ref description, value);
            }
        }
        #endregion

        #region Fields
        private int bookId;
        private string imageUrl;
        private string bookTitle;
        private string author;
        private string editionYear;
        private string publisher;
        private string description;
        #endregion
    }
}
