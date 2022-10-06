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
        /// <summary>
        /// Hàm khởi tạo để tiêm phụ thuộc vào class
        /// </summary>
        /// <param name="employeeDL"></param>
        /// Created by : TNMANH (29/09/2022)
        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// Method giúp trả về file excell danh sách nhân viên
        /// của class EmployeeExport
        /// </summary>
        /// <returns>File excell danh sách nhân viên</returns>
        /// Created by : TNMANH (06/10/2022)
        public IEnumerable<EmployeeExport> GetExportEmployee()
        {
            return _employeeDL.GetExportEmployee();
        }


        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        /// Created by : TNMANH (29/09/2022)
        public PagingData FilterEmployee(string? keyword, int? pageNumber, int? pageSize)
        {
            return _employeeDL.FilterEmployee(keyword, pageNumber, pageSize);
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
        public bool DeleteManyEmployee(Guid[] employeeIDs)
        {
            // giá trị rỗng trả về false luôn k gọi tới database nữa
            if(employeeIDs.Length == 0)
            {
                return false;
            }

            // trả về bool khi xóa nhiều
            return _employeeDL.DeleteManyEmployee(employeeIDs);
        }

        #endregion

        #endregion
    }
}
