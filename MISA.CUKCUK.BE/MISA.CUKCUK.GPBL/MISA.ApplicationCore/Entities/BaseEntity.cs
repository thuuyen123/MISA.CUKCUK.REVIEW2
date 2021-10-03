using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class BaseEntity
    {
        #region Property
  
        [MISANotMap]
        public EntityState EntityState { get; set; } = EntityState.AddNew;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        ///Created By : TTUyen(29/9/2021)
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Tạo bởi
        /// </summary>
        ///Created By : TTUyen(29/9/2021)
        [DisplayName("Người tạo")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        ///Created By : TTUyen(29/9/2021)
        [DisplayName("Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Chỉnh sửa bởi
        /// </summary>
        ///Created By : TTUyen(29/9/2021)
        [DisplayName("Người sửa")]
        public string ModifiedBy { get; set; }

        #endregion
    }
}
