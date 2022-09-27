using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.COMMON.Enums;
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
    public class DepartmentsController : ControllerBase
    {

        #region Field

        private IDepartmentBL _departmentBL;

        #endregion

        /// <summary>
        /// Hàm khởi tạo để truyền configuration dùng để get connection string từ file
        /// appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        #region Constructor

        public DepartmentsController(IDepartmentBL departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #endregion

        /// danh sách các API liên quan tới việc lấy thông tin
        #region GETMethod

        /// <summary>
        /// API lấy danh sách toàn bộ phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created by : TNMANH (27/09/2022)
        [HttpGet("")]
        public IActionResult GetAllDepartments()
        {

            try
            {
                // thực hiện gọi vào DB
                var departments = _departmentBL.GetAllDepartments();

                return StatusCode(StatusCodes.Status200OK, departments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                ));
            }
        }

        #endregion
    }
}
