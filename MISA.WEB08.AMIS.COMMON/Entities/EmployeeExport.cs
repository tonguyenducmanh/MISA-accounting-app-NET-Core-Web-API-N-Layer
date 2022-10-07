using MISA.WEB08.AMIS.COMMON.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.COMMON.Entities
{
    /// <summary>
    /// Kết quả trả về khi lấy data từ db để xuất ra file excell
    /// </summary>
    /// Created by : TNMANH (06/10/2022)
    public class EmployeeExport
    {
        #region Properties

        /// <summary>
        /// Mã của nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên của nhân viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính của nhân viên
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Ngày sinh của nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng của nhân viên
        /// </summary>
        public long? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        #endregion
    }
}
