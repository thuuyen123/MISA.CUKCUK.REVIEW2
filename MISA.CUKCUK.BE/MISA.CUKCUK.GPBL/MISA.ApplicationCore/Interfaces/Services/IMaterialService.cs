using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IMaterialService: IBaseService<Material>
    {

        #region METHOD
        /// <summary>
        /// Sinh mã mới NVL theo tên của nguyên vật liệu
        /// </summary>
        /// <returns>Mã NVL mới</returns>
        /// Created By : TTUyen (29/09/2021)
        string GetNewMaterialCodeByMaterialName(string materialName);

        /// <summary>
        ///  Lấy danh sáchNVL theo từng điều kiện lọc
        /// </summary>
        /// <param name="filter">Object chứa nội dung lọc</param>
        /// <returns>Danh sáchNVL theo điều kiện lọc</returns>
        /// Created By : TTUyen (29/09/2021)
        Paging<Material> GetMaterialsByPagingFilter(MaterialFilter filter, List<FilterItem> listFilter);
        #endregion

    }
}
