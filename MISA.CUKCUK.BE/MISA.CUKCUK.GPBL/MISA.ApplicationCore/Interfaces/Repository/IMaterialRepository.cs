using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Repository
{
    public interface IMaterialRepository
    {

        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// CreateBy TTUyen ()3/10/2021)
        IEnumerable<Material> Get();

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="material">Thực thể theo kiểu Material</param>
        /// <returns>Số bản ghi được thêm </returns>
        /// CreateBy TTUyen ()3/10/2021)
        int Add(Material material);

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="material">Thực thể theo kiểu Material</param>
        /// <param name="materialId">Khóa chính của thực thể</param>
        /// <returns>Số bản ghi đã sửa</returns>
        /// CreateBy TTUyen ()3/10/2021)
        int Update(Material material, Guid materialId);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="materialId">Id bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// CreateBy TTUyen (03/10/2021)
        int Delete(Guid materialId);

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="materialId">Id bản ghi</param>
        /// <returns>Bản ghi có id tương ứng</returns>
        /// CreateBy TTUyen (03/10/2021)
        Material GetById(Guid materialId);

        /// <summary>
        /// Lọc bản ghi theo thuộc tính
        /// </summary>
        /// <param name="material">Thực thể kiểu Material</param>
        /// <param name="property">Thuộc tính</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// CreateBy TTUyen (03/10/2021)
        IEnumerable<Material> CheckMaterialCodeExist(Material material);

        /// <summary>
        /// Sinh mã mới NVL theo tên của nguyên vật liệu
        /// </summary>
        /// <returns>Mã NVL mới</returns>
        /// Created By : TTUyen (03/10/2021)
        string GetNumberMaterialCodeMax(string materialName);

        /// <summary>
        ///  Lấy danh sáchNVL theo từng điều kiện lọc
        /// </summary>
        /// <param name="filter">Object chứa nội dung lọc</param>
        /// <returns>Danh sáchNVL theo điều kiện lọc</returns>
        /// Created By : TTUyen ()
        Paging<Material> GetMaterialsByPagingFilter(MaterialFilter filter, List<FilterItem> listFilter);

        /// <summary>
        /// Hàm lấy NVL theo id, để NVL chi tiết là 1 thuộc tính của NVL cha
        /// </summary>
        /// <returns>NVL</returns>
        /// Created By: TTUyen (03/10/2021)
        Material GetMaterialDetailById(Guid materialId);


    }
}
