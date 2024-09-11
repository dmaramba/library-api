using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class ReserveModel
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


   
    }
}
