using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        #region METHOD
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// CreateBy TTUyen (29/09/2021)
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thực thể theo kiểu TEntity</param>
        /// <returns>Số bản ghi được thêm </returns>
        /// CreateBy TTUyen (29/09/2021)
        int Add(TEntity entity);

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entity">Thực thể theo kiểu TEntity</param>
        /// <param name="entityId">Khóa chính của thực thể</param>
        /// <returns>Số bản ghi đã sửa</returns>
        /// CreateBy TTUyen (29/09/2021)
        int Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// CreateBy TTUyen (29/09/2021)
        int Delete(Guid entityId);

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns>Bản ghi có id tương ứng</returns>
        /// CreateBy TTUyen (29/09/2021)
        TEntity GetById(Guid entityId);

        /// <summary>
        /// Lọc bản ghi theo thuộc tính
        /// </summary>
        /// <param name="entity">Thực thể kiểu TEntity</param>
        /// <param name="property">Thuộc tính</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// CreateBy TTUyen (29/09/2021)
        TEntity CheckDuplicateByProp(TEntity entity, PropertyInfo property);
        #endregion
    }
}
