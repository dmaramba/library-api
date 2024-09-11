using Library.Domain.Models;
using Library.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public class ReserveBookRepository : GenericRepository<ReserveBook>, IReserveBookRepository
    {
        public ReserveBookRepository(LibraryContext dbContext) : base(dbContext)
        {

        }

    }
}
