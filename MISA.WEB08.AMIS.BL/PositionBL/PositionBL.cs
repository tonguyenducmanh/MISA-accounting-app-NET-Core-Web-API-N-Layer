using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class PositionBL : IPositionBL
    {
        #region Field

        private IPositionDL _positionDL;

        #endregion

        #region Constructor

        public PositionBL(IPositionDL positionDL)
        {
            _positionDL = positionDL;
        }

        #endregion

        #region Method

        // Danh sách các API liên quan tới việc lấy thông tin
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ chức vụ
        /// </summary>
        /// <returns>Danh sách chức vụ</returns>
        /// Created by : TNMANH (17/09/2022)
        public IEnumerable<Positions> GetAllPositions()
        {
            return _positionDL.GetAllPositions();
        }

        #endregion

        #endregion
    }
}
