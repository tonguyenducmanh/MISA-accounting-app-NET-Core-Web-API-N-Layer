using MISA.WEB08.AMIS.COMMON.Resources;
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
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người tạo đơn vị
        /// </summary>
        public string CreatedBy { get; set; } = MISAResource.Default_CreatedBy_User;

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; } = MISAResource.Default_ModifiedBy_User;

        #endregion
    }
}
