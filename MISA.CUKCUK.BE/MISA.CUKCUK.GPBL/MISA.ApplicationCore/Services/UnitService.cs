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
    public class UnitService :BaseService<Unit>, IUnitService
    {
        #region DECLARE
        private IUnitRepository _unitRepository;
        #endregion

        #region Contructor
        public UnitService(IUnitRepository unitRepository) : base(unitRepository)
        {
            _unitRepository = unitRepository;
        }
        #endregion
    }
}
