using MISA.WEB08.AMIS.COMMON.CustomAttribute;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MISA.WEB08.AMIS.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        public PagingData FilterEmployee(string? keyword, int? pageNumber, int? pageSize)
        {
            return _employeeDL.FilterEmployee(keyword, pageNumber, pageSize);
        }

        #endregion

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên mới</param>
        /// <returns>Status 201 created, employeeID</returns>
        /// Created by : TNMANH (17/09/2022)


        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 nhân viên dựa vào employeeID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên định sửa</param>
        /// <param name="employee">Giá trị sửa</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public ServiceResponse UpdateEmployee(Guid employeeID, Employee employee)
        {
            var validateResult = ValidateRequestData(employee);

            if (validateResult != null && validateResult.Success)
            {
                var editedEmployeeID = _employeeDL.UpdateEmployee(employeeID, employee);

                if (editedEmployeeID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = editedEmployeeID
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = new ErrorResult(
                        ErrorCode.UpdateFailed,
                        MISAResource.DevMsg_UpdatedFailed,
                        MISAResource.UserMsg_UpdatedFailed,
                        MISAResource.MoreInfo_UpdatedFailed
                        )
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateResult?.Data
                };
            }
        }

        #endregion

        #endregion
    }
}
