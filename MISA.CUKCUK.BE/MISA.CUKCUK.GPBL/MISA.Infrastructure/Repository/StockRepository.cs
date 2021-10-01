using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Repository
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        #region Contructor
        public StockRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion
    }
}
