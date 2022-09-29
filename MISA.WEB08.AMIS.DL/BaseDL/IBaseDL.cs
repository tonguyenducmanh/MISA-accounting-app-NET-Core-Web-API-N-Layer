using Dapper;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public interface IBaseDL<T>
    {
        // Danh sách các API liên quan tới việc lấy thông tin của 1 table
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by : TNMANH (29/09/2022)
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// API check trùng mã record
        /// </summary>
        /// <returns>Records có mã trùng</returns>
        /// Created by : TNMANH (29/09/2022)
        public T GetDuplicateCode(string recordCode);

        /// <summary>
        /// API lấy mã record lớn nhất
        /// </summary>
        /// <returns>Mã record lớn nhất</returns>
        /// Created by : TNMANH (29/09/2022)
        public String GetMaxRecordCode();

        /// <summary>
        /// API lấy thông tin chi tiết của 1 record theo ID đầu vào
        /// </summary>
        /// <param name="employeeID">ID của record</param>
        /// <returns>Thông tin của record theo ID</returns>
        /// Created by : TNMANH (29/09/2022)
        public T GetRecordByID(Guid recordID);
        #endregion

        // Danh sách các API liên quan tới việc thêm mới 1 record vào 1 table

        #region PostMethod


        /// <summary>
        /// API Thêm mới 1 record
        /// </summary>
        /// <param name="record">Thông tin record mới</param>
        /// <returns>Status 201 created, recordID</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid InsertRecord(T record);

        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 record dựa vào recordID
        /// </summary>
        /// <param name="recordID">ID của record định sửa</param>
        /// <param name="record">Giá trị record định sửa</param>
        /// <returns>Status 200 OK, recordID / Status 400 badrequest</returns>
        /// Created by : TNMANH (29/09/2022)
        public Guid UpdateRecord(Guid recordID, T record);

        #endregion

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 record dựa vào ID
        /// </summary>
        /// <param name="recordID">ID của record</param>
        /// <returns>Status 200 OK, recordID / Status 400 badrequest</returns>
        /// Created by : TNMANH (29/09/2022)
        public Guid DeleteRecord(Guid recordID);
        #endregion
    }
}
