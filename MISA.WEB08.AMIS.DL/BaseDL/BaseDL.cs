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
    public class BaseDL<T> : IBaseDL<T>
    {
        // Danh sách các API liên quan tới việc lấy thông tin của 1 table
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by : TNMANH (29/09/2022)
        public IEnumerable<T> GetAllRecords()
        {
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(MISAResource.Proc_GetAll, typeof(T).Name.ToLower());

            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                // thực hiện gọi vào DB
                var records = sqlConnection.Query<T>(
                    storeProcedureName
                    , commandType: System.Data.CommandType.StoredProcedure
                    );
                return records;
            };

        }

        #endregion
    }
}
