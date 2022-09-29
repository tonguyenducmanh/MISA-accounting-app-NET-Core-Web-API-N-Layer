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

        #endregion
    }
}
