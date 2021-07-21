using BooksDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB.Interfaces
{
    public interface IUserManagement
    {
        Task<UserDetails> RegisterUserAsync(UserDetails user);

        Task<UserDetails> AuthenticateUserAsync(User user);

        Task<bool> AddToLibrary(int userId, LibraryItem item);

        Task<IEnumerable<LibraryItem>> GetUserLibraryAsync(int userId);
    }
}
