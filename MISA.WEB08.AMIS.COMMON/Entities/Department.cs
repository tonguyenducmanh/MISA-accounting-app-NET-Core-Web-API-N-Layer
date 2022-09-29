namespace MISA.WEB08.AMIS.COMMON.Entities
{
    /// <summary>
    /// Thực thể đơn vị map tới bảng Department trong Database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    public class Department : BaseEntity
    {
        #region Properties

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Giới thiệu về đơn vị, phòng ban
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
