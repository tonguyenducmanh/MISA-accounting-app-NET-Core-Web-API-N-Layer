namespace MISA.WEB08.AMIS.COMMON.CustomAttribute
{
    /// <summary>
    /// Class khai báo 1 attribute là khóa chính
    /// </summary>
    /// Created by : TNMANH (24/09/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {

    }

    /// <summary>
    /// Class khai báo 1 attribute là record Code
    /// </summary>
    /// Created by : TNMANH (29/09/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class RecordCodeAttribute : Attribute
    {

    }

    /// <summary>
    /// Class khai báo 1 attribute bắt buộc phải có giá trị
    /// </summary>
    /// Created by : TNMANH (24/09/2022)
    public class MustHave : Attribute
    {
        #region Field

        /// <summary>
        /// Message lỗi trả về cho người dùng đọc được,
        /// Ta sẽ viết cái này bên cạnh tên Attribute
        /// </summary>
        /// Created by : TNMANH (24/09/2022)
        public string ErrorMessage;

        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo tùy chỉnh để trả về errorMessage tương ứng
        /// </summary>
        /// <param name="errorMessage">Đoạn mã báo lỗi</param>
        /// Created by : TNMANH (24/09/2022)
        public MustHave(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }

}
