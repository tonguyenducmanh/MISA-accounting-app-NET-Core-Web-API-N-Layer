using MISA.WEB08.AMIS.COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public interface IPositionDL
    {
        // Danh sách các API liên quan tới việc lấy thông tin position

        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ chức vụ
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created by : TNMANH (27/09/2022)
        public IEnumerable<Positions> GetAllPositions();

        #endregion
    }
}
