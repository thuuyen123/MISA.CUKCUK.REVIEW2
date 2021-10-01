using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Material : BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Khóa chính")]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Mã NVL 
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [Duplicated]
        [Required]
        [MaxLength(25, "Mã NVL không được quá 25 ký tự")]
        [DisplayName("Mã NVL")]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Tên NVL
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [Required]
        [MaxLength(128, "Tên NVL không được quá 128 ký tự")]
        [DisplayName("Tên NVL ")]
        public string MaterialName { get; set; }

        /// <summary>
        /// Số lượng tồn tối thiểu
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("SL tồn tối thiểu")]
        public int? MinimumStock { get; set; }


        /// <summary>
        /// Số thời hạn sử dụng
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Số thời hạn sử dụng")]
        public int? ExprityDate { get; set; }

        /// <summary>
        /// Kiểu hạn sử dụng (0-ngày, 1-tháng, 2-năm)
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Kiểu hạn sử dụng")]
        public int? ExprityType { get; set; }

        /// <summary>
        /// Tên Kiểu hạn sử dụng (0-ngày, 1-tháng, 2-năm)
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Tên Kiểu hạn sử dụng")]
        public string ExprityTypeName
        {
            get
            {
                if (ExprityType == 1)
                {
                    return "Tháng";
                }
                else if (ExprityType == 2)
                {
                    return "Năm";
                }
                return "Ngày";
            }
        }

        /// <summary>
        /// Ngừng theo dõi
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Ngừng theo dõi")]
        public int? StopFollowing { get; set; }

        /// <summary>
        /// Tên nhóm NVL
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Tên nhóm NVL")]
        public string MaterialGroupName { get; set; }

        /// <summary>
        /// Tính chất
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Tính chất")]
        public string PropertiesOfMaterialName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        ///  Created By : TTUyen ( 29/09/2021)
        [DisplayName("Mô tả")]
        [MaxLength(255, "Mô tả không được quá 255 ký tự")]
        public string Description { get; set; }
        /// <summary>
        /// Id Kho ngầm định
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Id Kho ngầm định")]
        public Guid? StockId { get; set; }

        /// <summary>
        /// Mã kho ngầm định
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Mã Kho ngầm định")]
        public string StockCode { get; set; }

        /// <summary>
        ///Tên Kho ngầm định
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Tên kho ngầm định")]
        public string StockName { get; set; }

        /// <summary>
        /// Id đơn vị tính
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Id đơn vị tính")]
        public Guid? UnitId { get; set; }


        /// <summary>
        /// Tên đơn vị tínnh
        /// </summary>
        /// Created By : TTUyen (29/9/2021)
        [DisplayName("Tên đơn vị tính")]
        public string UnitName { get; set; }


        #endregion

    }
}
