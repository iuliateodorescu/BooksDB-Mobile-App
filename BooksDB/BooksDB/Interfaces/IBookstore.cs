using BooksDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB.Interfaces
{
    public interface IBookstore
    {
        Task<IEnumerable<Book>> GetBooksAsync();

        Task<BookDetails> GetDetailsAsync(int bookId);

        Task<Book> GetBookAsync(int bookId);

        Task<IEnumerable<Book>> GetBooksByTitleAsync(string text);

        Task<IEnumerable<Book>> GetRecommendations(string title);
    }
}
