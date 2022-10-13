using MISA.WEB08.AMIS.COMMON.CustomAttribute;
using MISA.WEB08.AMIS.COMMON.Enums;

namespace MISA.WEB08.AMIS.COMMON.Entities
{
    /// <summary>
    /// Thực thể nhân viên map tới bảng Employee trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    public class Employee : BaseEntity
    {
        #region Properties

        /// <summary>
        /// ID của nhân viên
        /// </summary>
        [PrimaryKey]
        public Guid? EmployeeID { get; set; }

        /// <summary>
        /// Mã của nhân viên
        /// </summary>
        [MustHave("Mã nhân viên không được để trống.")]
        [MaxLength("Mã nhân viên không được dài quá 8 ký tự.", 8)]
        [RecordCode]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên của nhân viên
        /// </summary>
        [MustHave("Tên nhân viên không được để trống.")]
        [MaxLength("Tên nhân viên không được dài quá 100 ký tự.", 100)]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh của nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính của nhân viên
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Loại nhân viên
        /// </summary>
        public EmployeeType EmployeeType { get; set; }

        /// <summary>
        /// Căn cước công dân / chứng minh thư
        /// </summary>
        [MaxLength("Số CMND không được dài quá 25 ký tự.", 25)]
        [NumberValidate("Số CMND chỉ chứa ký tự từ 0-9.")]
        public string? IdentityCard { get; set; }

        /// <summary>
        /// Ngày cấp Căn cước công dân / chứng minh thư
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân / chứng minh thư
        /// </summary>
        [MaxLength("Nơi cấp CMND không được dài quá 255 ký tự.", 255)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        [MaxLength("Địa chỉ không được dài quá 255 ký tự.", 255)]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại di động của nhân viên
        /// </summary>
        [MaxLength("Số điện thoại di động không được dài quá 50 ký tự.", 50)]
        public string? MobilePhone { get; set; }

        /// <summary>
        /// Số điện thoại cố định của nhân viên
        /// </summary>
        [MaxLength("Số điện thoại cố định không được dài quá 50 ký tự.", 50)]
        public string? Telephone { get; set; }

        /// <summary>
        /// Địa chỉ Email của nhân viên
        /// </summary>
        [MaxLength("Địa chỉ email không được dài quá 100 ký tự.", 100)]
        [EmailValidate("Địa chỉ email không đúng định dạng.")]
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng của nhân viên
        /// </summary>
        [MaxLength("Số tài khoản ngân hàng không được dài quá 25 ký tự.", 25)]
        [NumberValidate("Số tài khoản ngân hàng chỉ chứa ký tự từ 0-9.")]
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MaxLength("Tên ngân hàng không được dài quá 255 ký tự.", 255)]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh của ngân hàng
        /// </summary>
        [MaxLength("Tên chi nhánh không được dài quá 255 ký tự.", 255)]
        public string? BankBranch { get; set; }

        /// <summary>
        /// ID của đơn vị
        /// </summary>
        [MustHave("ID phòng ban không được để trống.")]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        [MaxLength("Tên đơn vị không được dài quá 255 ký tự.", 255)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        [MaxLength("Tên chức danh không được dài quá 255 ký tự.", 255)]
        public string? PositionName { get; set; }

        #endregion
    }
}
