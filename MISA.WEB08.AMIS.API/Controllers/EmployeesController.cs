using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.API.Controllers;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;

namespace MISA.WEB08.AMIS.API
{
    /// <summary>
    /// Danh sách các API liên quan tới dữ liệu nhân viên của bảng employee trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        /// <summary>
        /// Các field của class EmployeesController
        /// </summary>
        /// Created by : TNMANH (24/09/2022)
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo để truyền configuration dùng để get connection string từ file
        /// appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        public EmployeesController(IEmployeeBL employeeBL): base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        /// danh sách các API liên quan tới việc lấy thông tin nhân viên
        #region GETMethod

        /// <summary>
        /// API lấy ra danh sách nhân viên theo các trường cố định
        /// của class EmployeeExport
        /// </summary>
        /// <returns>Danh sách nhân viên map theo class EmployeeExport</returns>
        /// Created by : TNMANH (06/10/2022)
        [HttpGet("export-all")]
        public  IActionResult GetExportEmployee()
        {
            try
            {
                // Lấy danh sách nhân viên theo filter
                var employeeExportFile =  _employeeBL.GetExportEmployee();

                string excelName = $"Danh sách nhân viên-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";

                // Trả về status code kèm theo object kết quả
                if (employeeExportFile != null)
                {   
                    return StatusCode(StatusCodes.Status200OK, true);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code kèm theo object thông báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }

        }


        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        [HttpGet("filter")]
        public IActionResult FilterEmployee(
            [FromQuery] string? keyword,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize
            )
        {
            try
            {
                // Lấy danh sách nhân viên theo filter
                var employeeFiltered = _employeeBL.FilterEmployee(keyword, pageNumber, pageSize);

                // Trả về status code kèm theo object kết quả
                return StatusCode(StatusCodes.Status200OK, employeeFiltered);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code kèm theo object thông báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }

        }

        #endregion

        // Danh sách các API liên quan tới xóa nhân viên
        #region DeleteMethod

        /// <summary>
        /// API xóa nhiều nhân viên theo danh sách IDs
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns>True hoặc false, true là xóa thành công, false là xóa không thành công</returns>
        /// Created by : TNMANH (05/10/2022)
        [HttpPost("delete-many")]
        public IActionResult DeleteManyEmployee([FromBody]Guid[] employeeIDs)
        {
            try
            {
                // thực hiện xóa 1 bản ghi trong db
                var result = _employeeBL.DeleteManyEmployee(employeeIDs);

                // trả về status code và kết quả
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // trả về status code và object báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
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
