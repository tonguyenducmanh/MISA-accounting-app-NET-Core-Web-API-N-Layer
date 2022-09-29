using Dapper;
using MISA.WEB08.AMIS.COMMON.CustomAttribute;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by : TNMANH (17/09/2022)
        public string GetMaxEmployeeCode()
        {
            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            var sqlConnection = new MySqlConnection(connectionString);

            // Chuẩn bị câu lệnh Query
            string storeProcedureName = MISAResource.Proc_Get_MaxCode;

            // Thực hiện gọi vào Database
            var maxCode = sqlConnection.QueryFirstOrDefault<String>(
                storeProcedureName,
                commandType: System.Data.CommandType.StoredProcedure
                );

            // Trả về kết quả
            return maxCode;
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 nhân viên theo ID đầu vào
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Thông tin của nhân viên theo ID</returns>
        /// Created by : TNMANH (17/09/2022)
        public Employee GetEmployeeByID(Guid employeeID)
        {
            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            var sqlConnection = new MySqlConnection(connectionString);

            // Khai báo procedure name
            string storeProcedureName = MISAResource.Proc_Get_ByID;

            // Khởi tạo các parameter để chèn vào trong storeprocedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_id", employeeID);

            // Thực hiện kết nối tới Database
            var employee = sqlConnection.QueryFirstOrDefault<Employee>(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );

            // Trả về status code và kết quả trả về
            return employee;
        }

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        public PagingData FilterEmployee(string? keyword, int? pageNumber, int? pageSize)
        {
            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            var sqlConnection = new MySqlConnection(connectionString);

            // Chuẩn bị câu lệnh MySQL
            string storeProcedureName = MISAResource.ProcGetEmployeeFilter;

            // Chèn parameter cho procedure
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("v_PageNumber", pageNumber);
            parameters.Add("v_PageSize", pageSize);
            parameters.Add("v_Search", keyword);

            // Thực hiện gọi vào trong Database
            var employeesFiltered = sqlConnection.QueryMultiple(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

            var employees = employeesFiltered.Read<Employee>().ToList();
            var totalRecord = (int)employeesFiltered.ReadSingle().TotalCount;

            // Trả về status code kèm theo object kết quả
            return new PagingData()
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPage = (int)Math.Ceiling(Convert.ToDecimal(totalRecord / pageSize) + 1),
                Data = employees,
                TotalRecord = totalRecord,
            };
        }

        #endregion

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 nhân viên dựa vào employeeID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên định sửa</param>
        /// <param name="employee">Giá trị sửa</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid UpdateEmployee(Guid employeeID, Employee employee)
        {
            // Tạo connection
            var sqlConnection = new MySqlConnection(DataContext.MySQLConnectionString);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = MISAResource.Proc_Put_OneRecord;

            // Truyền tham số vào store procedure
            DynamicParameters parameters = new DynamicParameters();

            // Chèn các giá trị khác vào param cho store procedure
            var props = typeof(Employee).GetProperties();
            foreach (var prop in props)
            {
                // lấy ra tên của properties
                var propName = prop.Name;
                var propValue = prop.GetValue(employee);
                parameters.Add($"v_{propName}", propValue);
            }

            // Thay giá trị employeeID vào ( cái này không được đổi )
            parameters.Add("$v_EmployeeID", employeeID);

            // Thực hiện chèn dữ liệu vào trong database
            var nunmberOfAffectedRows = sqlConnection.Execute(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

            // Trả về kết quả
            if (nunmberOfAffectedRows > 0)
            {
                return employeeID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 nhân viên dựa vào ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid DeleteEmployee(Guid employeeID)
        {
            // Tạo connection
            var sqlConnection = new MySqlConnection(DataContext.MySQLConnectionString);

            // khởi tạo store procedure
            string storeProcedureName = MISAResource.Proc_Delete_OneRecord;

            // khởi tạo các parameter truyền vào trong store procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_id", employeeID);

            // thực hiện truy vấn tới database
            var nunmberOfAffectedRows = sqlConnection.Execute(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );

            // Trả về kết quả
            if (nunmberOfAffectedRows > 0)
            {
                return employeeID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion

    }
}
