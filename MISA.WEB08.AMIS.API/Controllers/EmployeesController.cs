﻿using Microsoft.AspNetCore.Mvc;
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

        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        /// <summary>
        /// Hàm khởi tạo để truyền configuration dùng để get connection string từ file
        /// appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL): base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        /// danh sách các API liên quan tới việc lấy thông tin nhân viên
        #region GETMethod

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


        //// Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên mới</param>
        /// <returns>Status 201 created, employeeID</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                var result = _employeeBL.InsertEmployee(employee);
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
                }
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



        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 nhân viên dựa vào employeeID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên định sửa</param>
        /// <param name="employee">Giá trị sửa</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {

            try
            {
                var result = _employeeBL.UpdateEmployee(employeeID, employee);
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
                }
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
