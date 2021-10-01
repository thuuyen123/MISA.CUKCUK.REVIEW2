using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        #region Method

        /// <summary>
        /// Lấy tất cả dữ liệu 
        /// </summary>
        /// <returns>Danh sách tất cả dữ liệu </returns>
        /// CreateBy TTUyen (27/08/2021)
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Thực thể theo kiểu TEntity </param>
        /// <returns>Thông tin trả về</returns>
        /// CreateBy TTUyen (27/08/2021)
        ServiceResult Add(TEntity entity);

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Thực thể theo kiểu TEntity </param>
        /// <param name="entityId">id </param>
        /// <returns>Thông tin trả về </returns>
        /// CreateBy TTUyen (27/08/2021)
        ServiceResult Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xóa theo id
        /// </summary>
        /// <param name="entityId">id </param>
        /// <returns>Thông tin trả về </returns>
        /// CreateBy TTUyen (27/08/2021)
        /// 
        ServiceResult Delete(Guid entityId);

        /// <summary>
        /// Lấy dữ liệu qua khóa chính
        /// </summary>
        /// <param name="entityId">id </param>
        /// <returns>Bản ghi có id tương ứng </returns>
        /// CreateBy TTUyen (27/08/2021)
        TEntity GetById(Guid entityId);

        /// <summary>
        /// Láy dữ liệu theo thuộc tính
        /// </summary>
        /// <param name="entity">Thực thể kiểu TEntity</param>
        /// <param name="property">THuộc tính</param>
        /// <returns>Bản ghi có thuộc tính tương ứng</returns>
        /// CreateBy TTUyen (27/08/2021)
        //TEntity GetByProp(TEntity entity, PropertyInfo property);

        #endregion
    }
}
