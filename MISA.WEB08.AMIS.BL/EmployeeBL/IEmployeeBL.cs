using MISA.WEB08.AMIS.COMMON.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lấy ra danh sách nhân viên theo các trường cố định
        /// của class EmployeeExport
        /// </summary>
        /// <returns>Danh sách nhân viên map theo class EmployeeExport</returns>
        /// Created by : TNMANH (06/10/2022)
        public byte[] GetExportEmployee();

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        /// Created by : TNMANH (29/09/2022)
        public PagingData FilterEmployee(
             string? keyword,
            int? pageNumber,
             int? pageSize
            );


        #endregion

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        #endregion

        // Danh sách các API liên quan tới xóa nhân viên
        #region DeleteMethod

        /// <summary>
        /// API xóa nhiều nhân viên theo danh sách IDs
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns>True hoặc false, true là xóa thành công, false là xóa không thành công</returns>
        /// Created by : TNMANH (05/10/2022)
        public bool DeleteManyEmployee(Guid[] employeeIDs);

        #endregion

    }
}
