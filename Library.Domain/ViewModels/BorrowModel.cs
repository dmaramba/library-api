using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class BorrowModel
    {
        /// <summary>
        /// The customer Id
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// The book id
        /// </summary>
        [Required]
        public int BookId { get; set; }


        /// <summary>
        /// The due date for returning the book
        /// </summary>
        [Required]
        public DateTime DueDate { get; set; }
    }
}
