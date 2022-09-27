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
    public class EmployeeBL : IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by : TNMANH (17/09/2022)
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeDL.GetAllEmployees();
        }

        /// <summary>
        /// API check trùng mã nhân viên
        /// </summary>
        /// <returns>Records có mã nhân viên trùng</returns>
        /// Created by : TNMANH (25/09/2022)
        public Employee GetDuplicateCode(string EmployeeCode)
        {
            return _employeeDL.GetDuplicateCode(EmployeeCode);
        }

        /// <summary>
        /// API lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by : TNMANH (17/09/2022)
        public string GetMaxEmployeeCode()
        {
            return _employeeDL.GetMaxEmployeeCode();
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 nhân viên theo ID đầu vào
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Thông tin của nhân viên theo ID</returns>
        /// Created by : TNMANH (17/09/2022)
        public Employee GetEmployeeByID(Guid employeeID)
        {
            return _employeeDL.GetEmployeeByID(employeeID);
        }

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
        public ServiceResponse InsertEmployee(Employee employee)
        {
            var validateResult = ValidateRequestData(employee);
            var checkDuplicateResult = CheckDuplicateEmployeeCode(employee.EmployeeCode);

            if (validateResult != null && validateResult.Success && checkDuplicateResult.Success)
            {
                var newEmployeeID = _employeeDL.InsertEmployee(employee);

                if (newEmployeeID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = newEmployeeID
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = new ErrorResult(
                        ErrorCode.InsertFailed,
                        MISAResource.DevMsg_InsertFailed,
                        MISAResource.UserMsg_Exception,
                        MISAResource.MoreInfo_InsertFailed
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

        public ServiceResponse CheckDuplicateEmployeeCode(string employeeCode)
        {
            // Kiểm tra xem mã có bị trùng chưa
            var testDuplicateCode = GetDuplicateCode(employeeCode);

            if (testDuplicateCode != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = new ErrorResult(
                        ErrorCode.DuplicateCode,
                        MISAResource.DevMsg_DuplicatedCode,
                        MISAResource.UserMsg_DuplicatedCode,
                        MISAResource.MoreInfo_DupplicatedCode
                        )
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = ""
                };
            }
        }

        /// <summary>
        /// Validate dữ liệu truyền lên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần validate</param>
        /// <param name="httpContext">httpContext truyền vào từ request</param>
        /// <returns>Đối tượng ServiceRespone</returns>
        /// Created by : TNMANH (27/09/2022)
        private ServiceResponse ValidateRequestData(Employee employee)
        {
            // Validate dữ liệu đầu vào
            var props = typeof(Employee).GetProperties();
            List<string> validateFailed = new List<string>();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(employee);
                var mustHave = (MustHave?)Attribute.GetCustomAttribute(prop, typeof(MustHave));
                if (mustHave != null && string.IsNullOrEmpty(propValue?.ToString()))
                {
                    validateFailed.Add(mustHave.ErrorMessage);
                }
            }

            // Check xem nếu có lỗi văng ra kết quả luôn khỏi chạy đoạn dưới
            if (validateFailed.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data =
                    new ErrorResult(
                    ErrorCode.EmptyCode,
                    MISAResource.DevMsg_ValidateFailed,
                    MISAResource.UserMsg_ValidateFailed,
                    validateFailed
                    )
                };
            }
            return new ServiceResponse
            {
                Success = true
            };
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
                        MISAResource.DevMsg_InsertFailed,
                        MISAResource.UserMsg_Exception,
                        MISAResource.MoreInfo_InsertFailed
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

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 nhân viên dựa vào ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public ServiceResponse DeleteEmployee(Guid employeeID)
        {
            var deletedEmployeeID = _employeeDL.DeleteEmployee(employeeID);

            if (deletedEmployeeID != Guid.Empty)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = deletedEmployeeID
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = new ErrorResult(
                    ErrorCode.UpdateFailed,
                    MISAResource.DevMsg_InsertFailed,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_InsertFailed
                    )
                };
            }
        }

        #endregion 
        #endregion
    }
}
