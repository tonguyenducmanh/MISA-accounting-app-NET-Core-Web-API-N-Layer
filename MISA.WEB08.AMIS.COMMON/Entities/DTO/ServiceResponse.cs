using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.COMMON.Entities
{
    /// <summary>
    /// Dữ liệu trả về từ tầng BL
    /// </summary>
    /// Created by : TNMANH (27/09/2022)
    public class ServiceResponse
    {
        /// <summary>
        /// Thành công hay không
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Data response khi thành công hay thất bại
        /// </summary>
        public object Data { get; set; }
    }
}
