using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.COMMON
{
    /// <summary>
    /// Thực thể chung chứa các properties mà các bảng khác đều phải có trong database
    /// </summary>
    /// Created by : TNMANH (29/09/2022)
    public class BaseEntity
    {
        #region Properties

        /// <summary>
        /// Ngày tạo đơn vị
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo đơn vị
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }

        #endregion
    }
}
