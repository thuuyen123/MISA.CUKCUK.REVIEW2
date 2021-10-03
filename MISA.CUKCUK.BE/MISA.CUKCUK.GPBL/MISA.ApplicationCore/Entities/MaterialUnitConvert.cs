using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class MaterialUnitConvert : BaseEntity
    {

        /// <summary>
        /// Khóa chính đơn vị chuyển đổi
        /// </summary>
        /// Created By : TTUyen ( 29/09/2021)
        [DisplayName("Khóa chính")]
        public Guid MaterialUnitConvertId { get; set; }

        /// <summary>
        /// Tỷ lệ chuyển đổi
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [DisplayName("Tỷ lệ chuyển đổi chuyển đổi")]
        public string ConvertRate { get; set; }

        /// <summary>
        /// Id đơn vị tính
        /// </summary>
        /// Created By : TTUyen ( 29/09/2021)
        [DisplayName("Khóa đơn vị tính")]
        [Required]
        public Guid UnitId { get; set; }
        
        /// <summary>
        /// Id NVL
        /// </summary>
        /// Created By : TTUyen ( 29/09/2021)
        [DisplayName("Khóa NVL")]
        [Required]
        public Guid MaterialId { get; set; }
         /// <summary>
        /// Phép tính
        /// </summary>
        /// Created By : TTUyen ( 29/09/2021)
        [DisplayName("Phép tính")]
        public string ConvertRateOperate { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        /// <summary>
        /// Mã NVL 
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Mã NVL")]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Tên NVL
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Tên NVL ")]
        public string MaterialName { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [DisplayName("Tên đơn vị tính")]
        public string UnitName { get; set; }


    }
}
