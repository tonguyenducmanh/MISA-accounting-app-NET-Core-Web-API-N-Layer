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
    public class PositionDL : IPositionDL
    {
        // Danh sách các API liên quan tới việc lấy thông tin 
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ chức vụ
        /// </summary>
        /// <returns>Danh sách chức vụ</returns>
        /// Created by : TNMANH (27/09/2022)
        public IEnumerable<Positions> GetAllPositions()
        {
            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            var sqlConnection = new MySqlConnection(connectionString);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(MISAResource.Proc_GetAll, typeof(Positions).Name.ToLower());


            // thực hiện gọi vào DB
            var positions = sqlConnection.Query<Positions>(
                storeProcedureName
                , commandType: System.Data.CommandType.StoredProcedure
                );
            return positions;
        }

        #endregion

    }
}
