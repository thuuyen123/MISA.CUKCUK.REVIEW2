using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IMaterialService
    {

        #region METHOD

        /// <summary>
        /// Lấy tất cả dữ liệu 
        /// </summary>
        /// <returns>Danh sách tất cả dữ liệu </returns>
        /// CreateBy TTUyen (27/08/2021)
        IEnumerable<Material> Get();

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="material">Thực thể theo kiểu Material </param>
        /// <returns>Thông tin trả về</returns>
        /// CreateBy TTUyen (27/08/2021)
        ServiceResult Add(Material material);

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="material">Thực thể theo kiểu Material </param>
        /// <param name="materialId">id </param>
        /// <returns>Thông tin trả về </returns>
        /// CreateBy TTUyen (27/08/2021)
        ServiceResult Update(Material material, Guid materialId);

        /// <summary>
        /// Xóa theo id
        /// </summary>
        /// <param name="materialId">id </param>
        /// <returns>Thông tin trả về </returns>
        /// CreateBy TTUyen (27/08/2021)
        /// 
        ServiceResult Delete(Guid materialId);

        /// <summary>
        /// Lấy dữ liệu qua khóa chính
        /// </summary>
        /// <param name="materialId">id </param>
        /// <returns>Bản ghi có id tương ứng </returns>
        /// CreateBy TTUyen (27/08/2021)
        Material GetById(Guid materialId);

        /// <summary>
        /// Sinh mã mới NVL theo tên của nguyên vật liệu
        /// </summary>
        /// <returns>Mã NVL mới</returns>
        /// Created By : TTUyen 
        /// 

        string GetNewMaterialCodeByMaterialName(string materialName);

        /// <summary>
        ///  Lấy danh sáchNVL theo từng điều kiện lọc
        /// </summary>
        /// <param name="filter">Object chứa nội dung lọc</param>
        /// <returns>Danh sáchNVL theo điều kiện lọc</returns>
        /// Created By : TTUyen (03/10/2021)
        Paging<Material> GetMaterialsByPagingFilter(MaterialFilter filter, List<FilterItem> listFilter);
        #endregion

    }
}
