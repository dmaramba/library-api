using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class CustomAddProfileModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
