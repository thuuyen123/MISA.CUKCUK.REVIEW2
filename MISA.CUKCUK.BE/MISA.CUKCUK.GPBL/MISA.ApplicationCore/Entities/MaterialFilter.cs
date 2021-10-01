using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Phân trang và lọc 
    /// Created By : TTUyen (29/9/2021)
    /// </summary>
    public class MaterialFilter
    {


        /// <summary>
        /// Trang hiện tại
        /// Created By : TTUyen (29/9/2021)
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Số lượng bản ghi có trong Page
        /// Created By : TTUyen (29/9/2021)
        /// </summary>
        public int PageSize { get; set; } = 10;



    }
}
