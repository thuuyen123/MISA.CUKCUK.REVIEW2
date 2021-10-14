using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class FilterItem
    {
        // trường dược lọc
        public string DataField { get; set; }

        // kiểu lọc
        public string FilterOption { get; set; }

        // loại lọc: text|combobox
        public string FilterType { get; set; }

        // giá trị lọc
        public string FilterValue { get; set; }

        // có lọc hay không
        public bool Enable { get; set; }

    }
}
