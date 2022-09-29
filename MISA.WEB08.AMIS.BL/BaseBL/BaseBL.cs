using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        // Danh sách các API liên quan tới việc lấy thông tin của 1 table
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by : TNMANH (29/09/2022)
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// API check trùng mã record
        /// </summary>
        /// <returns>Records có mã trùng</returns>
        /// Created by : TNMANH (29/09/2022)
        public T GetDuplicateCode(string recordCode)
        {
            return _baseDL.GetDuplicateCode(recordCode);
        }

        #endregion


        // Danh sách các API liên quan tới việc thêm mới 1 record vào 1 table

        #region PostMethod


        /// <summary>
        /// API Thêm mới 1 record
        /// </summary>
        /// <param name="record">Thông tin record mới</param>
        /// <returns>Status 201 created, recordID</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid InsertRecord(T record)
        {
            return _baseDL.InsertRecord(record);
        }

        #endregion
    }
}
