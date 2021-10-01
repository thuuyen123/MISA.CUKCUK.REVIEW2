using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Paging<TEnity> where TEnity : class
    {
        /// <summary>
        /// Tổng số NVL
        /// Created By : TTUyen (29/9/2021)
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Tổng số trang
        /// Created By : TTUyen (29/9/2021)
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Dữ liệu phân trang
        /// Created By : TTUyen (29/9/2021)
        /// </summary>
        public IEnumerable<TEnity> Data { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// Created By : TTUyen (29/9/2021)
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Kích thước trang : số đối tượng trên 1 trang
        /// Created By : TTUyen (29/9/2021)
        /// </summary>
        public int PageSize { get; set; }
    }
}
