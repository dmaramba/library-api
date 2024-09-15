using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class CustomerBook
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
        /// The return date or expiry of book reserve
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}
