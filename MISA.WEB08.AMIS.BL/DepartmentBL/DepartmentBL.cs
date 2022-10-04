using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {

        /// <summary>
        /// các field được dùng trong class này
        /// </summary>
        /// Created by : TNMANH (29/09/2022)
        #region Field

        private IDepartmentDL _departmentDL;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo để tiêm phụ thuộc vào trong class
        /// </summary>
        /// <param name="departmentDL"></param>
        /// Created by : TNMANH (29/09/2022)
        public DepartmentBL(IDepartmentDL departmentDL): base(departmentDL)
        {
            _departmentDL = departmentDL;
        }

        #endregion

        #region Method

        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        #endregion

        #endregion
    }
}
