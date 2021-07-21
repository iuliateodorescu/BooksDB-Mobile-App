using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDB.Models
{
    public class BookDetails : Book
    {
        public string Description { get; set; }

        public string EditionYear { get; set; }

        public string Publisher { get; set; }
    }
}
