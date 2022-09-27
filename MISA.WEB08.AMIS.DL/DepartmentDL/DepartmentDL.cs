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
    public class DepartmentDL : IDepartmentDL
    {
        // Danh sách các API liên quan tới việc lấy thông tin 
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created by : TNMANH (27/09/2022)
        public IEnumerable<Department> GetAllDepartments()
        {
            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            var sqlConnection = new MySqlConnection(connectionString);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(MISAResource.Proc_GetAll, typeof(Department).Name.ToLower());


            // thực hiện gọi vào DB
            var departments = sqlConnection.Query<Department>(
                storeProcedureName
                , commandType: System.Data.CommandType.StoredProcedure
                );
            return departments;
        }

        #endregion

    }
}
