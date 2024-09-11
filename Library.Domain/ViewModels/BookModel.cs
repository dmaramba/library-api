using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class BookModel
    {
        /// <summary>
        /// The unique of the book
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The title of the book
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// The author of the book
        /// </summary>
        public required string Author { get; set; }

        /// <summary>
        /// The number of copies of the book
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The number of borrowed copies of the book
        /// </summary>
        public int BorrowedCopies { get; set; }

        /// <summary>
        /// The number of borrowed copies of the book
        /// </summary>
        public int ReservedCopies { get; set; }

        /// <summary>
        /// Indicate whether the book is available on not
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// The next available date based on the early return date
        /// </summary>
        public DateTime? NextAvailableDate { get; set; }
    }
}
