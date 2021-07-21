using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDB.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }
    }
}
