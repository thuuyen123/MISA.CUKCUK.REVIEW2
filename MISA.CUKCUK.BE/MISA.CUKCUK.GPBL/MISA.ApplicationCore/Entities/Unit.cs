using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Unit:BaseEntity
    {
        /// <summary>
        /// Khóa chính đơn vị tính
        /// </summary>
        /// Created By : TTUyen ( 29/09/2021)
        [DisplayName("Khóa chính")]
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [Required]
        [MaxLength(25, "Tên đơn vị tính không được quá 25 ký tự")]
        [DisplayName("Tên")]
        [Duplicated]
        public string UnitName { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [DisplayName("Diễn giải")]
        [MaxLength(255, "Diễn giải không được quá 255 ký tự")]
        public string Explanation { get; set; }
    }
}
