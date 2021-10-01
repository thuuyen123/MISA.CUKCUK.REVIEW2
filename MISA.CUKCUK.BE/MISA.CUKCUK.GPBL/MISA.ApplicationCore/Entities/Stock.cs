using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Stock:BaseEntity
    {

        /// <summary>
        /// Khóa chính kho ngầm định
        /// </summary>
        /// Created By : TTUyen ( 29/09/2021)
        [DisplayName("Khóa chính")]
        public Guid StockId { get; set; }

        /// <summary>
        /// Mã kho ngầm định
        /// </summary>
        /// Created By : TTUyen ( 29/09/2021)
        [Required]
        [Duplicated]
        [MaxLength(25,"Mã kho ngầm định không được quá 25 ký tự")]
        [DisplayName("Mã")]
        public string StockCode { get; set; }

        /// <summary>
        /// Tên kho ngầm định
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [Required]
        [MaxLength(128, "Tên kho ngầm định không được quá 128 ký tự")]
        [DisplayName("Tên")]
        public string StockName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [DisplayName("Địa chỉ")]
        [MaxLength(255, "Địa chỉ không được quá 255 ký tự")]
        public string Address { get; set; }
    }
}
