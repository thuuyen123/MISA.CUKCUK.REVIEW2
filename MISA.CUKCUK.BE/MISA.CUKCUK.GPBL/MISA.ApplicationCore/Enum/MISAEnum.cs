using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{

    /// <summary>
    /// Để xác định trạng thái của việc validate
    /// </summary>
    /// Created By : TTUyen(29/9/2021)
    public enum MISACode
    {
        /// <summary>
        /// Hợp lệ
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        IsValid = 100,

        /// <summary>
        /// Không thỏa mãn
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        NoValid = 900,

        /// <summary>
        /// Thành công
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        Success = 200,
    }
      public enum RequestStatus
    {
        /// <summary>
        /// Thành công
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        Complete = 1,

        /// <summary>
        /// Thất bại
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        Fail = 2,

        /// <summary>
        /// Exception
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        Exception = 3
    }
    /// <summary>
    /// Xác định trạng thái của object
    /// </summary>
  
    public enum EntityState
    {

        /// <summary>
        /// Không thực thi
        /// </summary>
        DEFAULT = 0,
        /// <summary>
        /// Thêm mơi
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        AddNew = 1,

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        Update = 2,

        /// <summary>
        /// Xóa thành công
        /// </summary>
        /// Created By : TTUyen(29/9/2021)
        Delete = 3
    }
}
