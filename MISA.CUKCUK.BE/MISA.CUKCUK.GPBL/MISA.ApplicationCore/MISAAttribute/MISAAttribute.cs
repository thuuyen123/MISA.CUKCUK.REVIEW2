using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.MISAAttribute
{
    /// <summary>
    /// Thuộc tính để check bắt buộc nhập
    /// Created By : TTUyen(29/9/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    /// <summary>
    /// Thuộc tính dùng để check trùng mã
    /// Created By : TTUyen(29/9/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Duplicated : Attribute
    {

    }


    /// <summary>
    /// Thuộc tính để check định dạng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Format: Attribute
    {

    }
    /// <summary>
    /// Thuộc tính dùng để check độ dài
    /// Created By : TTUyen(29/9/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int Length { get; set; }
        public string ErrorMsg { get; set; }
        public MaxLength(int length, string errorMsg)
        {
            Length = length;
            ErrorMsg = errorMsg;
        }
    }

    /// <summary>
    /// Thuộc tính dùng để hiển thị các trường thông tin lên cho người dùng biết là trường đó được gọi là gì
    /// Created By : TTUyen(29/9/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Không lưu vào csdl
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISANotMap : Attribute
    {
        public MISANotMap()
        {

        }
    }
}
