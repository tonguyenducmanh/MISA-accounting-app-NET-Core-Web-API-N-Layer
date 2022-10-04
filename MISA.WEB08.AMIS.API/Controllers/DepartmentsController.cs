using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.API.Controllers;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;

namespace MISA.WEB08.AMIS.API
{
    /// <summary>
    /// Các api liên quan tới việc lấy dữ liệu chức vụ từ bảng department trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {

        /// <summary>
        /// các field dùng trong class DepartmentsController
        /// </summary>
        /// Created by : TNMANH (24/09/2022)
        #region Field

        private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo để truyền configuration dùng để get connection string từ file
        /// appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        public DepartmentsController(IDepartmentBL departmentBL) : base(departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #endregion

        /// danh sách các API liên quan tới việc lấy thông tin
        #region GETMethod

        #endregion
    }
}
