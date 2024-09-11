using Library.Domain.Models;
using Library.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public class BorrowBookRepository : GenericRepository<BorrowBook>, IBorrowBookRepository
    {
        public BorrowBookRepository(LibraryContext dbContext) : base(dbContext)
        {

        }

    }
}
