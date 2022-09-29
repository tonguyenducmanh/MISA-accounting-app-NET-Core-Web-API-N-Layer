﻿namespace MISA.WEB08.AMIS.COMMON.Entities
{
    /// <summary>
    /// Thực thể chức danh map với bảng Positions trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    public class Positions : BaseEntity
    {
        #region Properties

        /// <summary>
        /// ID chức danh
        /// </summary>
        public Guid PositionID { get; set; }

        /// <summary>
        /// Mã chức danh
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Giới thiệu về chức danh
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
