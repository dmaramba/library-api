using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class BookAddModel
    {
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
    }
}
