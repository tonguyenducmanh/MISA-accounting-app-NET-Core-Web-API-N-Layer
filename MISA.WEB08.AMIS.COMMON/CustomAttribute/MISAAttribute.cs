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

    /// <summary>
    /// Class khai báo 1 attribute có đúng định dạng email không
    /// </summary>
    /// Created by : TNMANH (13/10/2022)
    public class EmailValidate : Attribute
    {
        #region Filed

        /// <summary>
        /// Message lỗi trả về cho người dùng đọc được
        /// </summary>
        /// Created by : TNMANH (13/10/2022)
        public string ErrorMessage;

        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo tùy chỉnh để trả về errorMessage tương ứng
        /// </summary>
        /// <param name="errorMessage">Đoạn mã báo lỗi</param>
        /// Created by : TNMANH (24/09/2022)
        public EmailValidate(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }

    /// <summary>
    /// Class khai báo 1 attribute bắt buộc phải có độ dài dưới bao nhiêu ký tự
    /// </summary>
    /// Created by : TNMANH (13/10/2022)
    public class MaxLength : Attribute
    {
        #region Filed

        /// <summary>
        /// Message lỗi trả về cho người dùng đọc được,
        /// Ta sẽ viết cái này bên cạnh tên Attribute
        /// </summary>
        /// Created by : TNMANH (24/09/2022)
        public string ErrorMessage;

        /// <summary>
        /// Độ dài tối đa của kí tự được điền vào
        /// </summary>
        public int Length;
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo tùy chỉnh để trả về errorMessage tương ứng
        /// </summary>
        /// <param name="errorMessage">Đoạn mã báo lỗi</param>
        /// <param name="length">Số kí tự tối đa được cho phép</param>
        /// Created by : TNMANH (24/09/2022)
        public MaxLength(string errorMessage, int length)
        {
            ErrorMessage = errorMessage;
            Length = length;
        }

        #endregion
    }
    /// <summary>
    /// Class khai báo 1 attribute có đúng định dạng số không
    /// </summary>
    /// Created by : TNMANH (13/10/2022)
    public class NumberValidate : Attribute
    {
        #region Filed

        /// <summary>
        /// Message lỗi trả về cho người dùng đọc được
        /// </summary>
        /// Created by : TNMANH (13/10/2022)
        public string ErrorMessage;

        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo tùy chỉnh để trả về errorMessage tương ứng
        /// </summary>
        /// <param name="errorMessage">Đoạn mã báo lỗi</param>
        /// Created by : TNMANH (24/09/2022)
        public NumberValidate(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }
}
