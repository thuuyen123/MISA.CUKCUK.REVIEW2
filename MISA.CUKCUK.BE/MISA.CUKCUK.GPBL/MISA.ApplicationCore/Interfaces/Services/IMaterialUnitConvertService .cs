using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IMaterialUnitConvertService: IBaseService<MaterialUnitConvert>
    {
        /// <summary>
        /// Lấy danh sách đơn vị chuyển đổi theo mã NVL
        /// </summary>
        /// <param name="materialId">Id NVL</param>
        /// <returns>Danh sách đơn vị chuyển đổi</returns>
        /// CreateBy: TTUyen (29/09/2021)
        IEnumerable<MaterialUnitConvert> GetMaterialUnitConvertsByMaterialId(Guid materialId);
    }
}
