using Dapper;
using MISA.WEB08.AMIS.COMMON.CustomAttribute;
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

        /// <summary>
        /// API check trùng mã nhân viên
        /// </summary>
        /// <returns>Records có mã nhân viên trùng</returns>
        /// Created by : TNMANH (25/09/2022)
        public T GetDuplicateCode(string recordCode)
        {
            // Chuẩn bị câu lệnh Query
            string storeProcedureName = string.Format(MISAResource.Proc_GetDupplicateCode, typeof(T).Name.ToLower());

            // Tạo giá trị trả về
            T maxCode;

            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                // Thêm param
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"v_{typeof(T).Name}Code", recordCode);
                // Thực hiện gọi vào Database
                maxCode = sqlConnection.QueryFirstOrDefault<T>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            } ;

            // Trả về kết quả
            return maxCode;
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
            // Tạo ra employeeID bằng guid
            Guid newID = Guid.NewGuid();

            // Truyền tham số vào store procedure
            DynamicParameters parameters = new DynamicParameters();

            // Chèn các giá trị khác vào param cho store procedure
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                // lấy ra tên của properties
                string propName = prop.Name;
                object propValue;
                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(prop, typeof(PrimaryKeyAttribute));
                if(primaryKeyAttribute != null)
                {
                    propValue = newID;
                }
                else
                {
                    propValue = prop.GetValue(record, null);
                }
                parameters.Add($"v_{propName}", propValue);
            }

            int nunmberOfAffectedRows = 0;

            // Tạo connection
            using (var sqlConnection = new MySqlConnection(DataContext.MySQLConnectionString))
            {
                // chuẩn bị câu lệnh MySQL
                string storeProcedureName = string.Format(MISAResource.Proc_InsertOne, typeof(T).Name.ToLower());

                // Thực hiện chèn dữ liệu vào trong database
                nunmberOfAffectedRows = sqlConnection.Execute(
                        storeProcedureName,
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );
            };

            if (nunmberOfAffectedRows > 0)
            {
                return newID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion
    }
}
