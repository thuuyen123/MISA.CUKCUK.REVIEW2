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
    public class MaterialUnitConvertRepository : BaseRepository<MaterialUnitConvert>, IMaterialUnitConvertRepository
    {
        #region Contructor
        public MaterialUnitConvertRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<MaterialUnitConvert> GetMaterialUnitConvertsByMaterialId(Guid materialId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
