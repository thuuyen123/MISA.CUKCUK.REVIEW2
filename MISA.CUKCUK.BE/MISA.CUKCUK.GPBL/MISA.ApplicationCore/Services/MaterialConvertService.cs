using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class MaterialUnitConvertService :BaseService<MaterialUnitConvert>, IMaterialUnitConvertService
    {
        #region DECLARE
        private IMaterialUnitConvertRepository _materialUnitConvertRepository;
        #endregion

        #region Contructor
        public MaterialUnitConvertService(IMaterialUnitConvertRepository materialUnitConvertRepository) : base(materialUnitConvertRepository)
        {
            _materialUnitConvertRepository = materialUnitConvertRepository;
        }

        public IEnumerable<MaterialUnitConvert> GetMaterialUnitConvertsByMaterialId(Guid materialId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
