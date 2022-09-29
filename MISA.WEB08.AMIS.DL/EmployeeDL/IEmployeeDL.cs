using MISA.WEB08.AMIS.COMMON.CustomAttribute;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.COMMON.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        public PagingData FilterEmployee(
             string? keyword,
            int? pageNumber,
             int? pageSize
            );


        #endregion

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên mới</param>
        /// <returns>Status 201 created, employeeID</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid InsertRecord(Employee employee);


        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 nhân viên dựa vào employeeID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên định sửa</param>
        /// <param name="employee">Giá trị sửa</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid UpdateEmployee( Guid employeeID, Employee employee);

        #endregion

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 nhân viên dựa vào ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid DeleteEmployee(Guid employeeID);
        #endregion
    }
}
