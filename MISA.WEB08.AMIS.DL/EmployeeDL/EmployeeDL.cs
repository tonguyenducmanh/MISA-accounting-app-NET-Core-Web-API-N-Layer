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
    public class EmployeeDL : IEmployeeDL
    {
        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by : TNMANH (17/09/2022)
        public IEnumerable<Employee> GetAllEmployees()
        {
            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            var sqlConnection = new MySqlConnection(connectionString);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(MISAResource.Proc_GetAll, typeof(Employee).Name.ToLower());


            // thực hiện gọi vào DB
            var employees = sqlConnection.Query<Employee>(
                storeProcedureName
                , commandType: System.Data.CommandType.StoredProcedure
                );
            return employees;
        }

        /// <summary>
        /// API check trùng mã nhân viên
        /// </summary>
        /// <returns>Records có mã nhân viên trùng</returns>
        /// Created by : TNMANH (25/09/2022)
        public Employee GetDuplicateCode(string EmployeeCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// API lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by : TNMANH (17/09/2022)
        public string GetMaxEmployeeCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 nhân viên theo ID đầu vào
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Thông tin của nhân viên theo ID</returns>
        /// Created by : TNMANH (17/09/2022)
        public Employee GetEmployeeByID(Guid employeeID)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên mới</param>
        /// <returns>ID của nhân viên vừa thêm, nếu insert thất bại thì return guid rỗng</returns>
        /// Created by : TNMANH (17/09/2022)
        public Guid InsertEmployee(Employee employee)
        {
            // Tạo connection
            var sqlConnection = new MySqlConnection(DataContext.MySQLConnectionString);

            // Tạo ra employeeID bằng guid
            Guid newID = Guid.NewGuid();

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(MISAResource.Proc_InsertOne, typeof(Employee).Name.ToLower());

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

            parameters.Add($"v_{typeof(Employee).Name}ID", newID);

            // Thực hiện chèn dữ liệu vào trong database
            var nunmberOfAffectedRows = sqlConnection.Execute(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );
            if(nunmberOfAffectedRows > 0)
            {
                return newID;
            }
            else
            {
                return Guid.Empty;
            }
        }


        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 nhân viên dựa vào employeeID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên định sửa</param>
        /// <param name="employee">Giá trị sửa</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public int UpdateEmployee(Guid employeeID, Employee employee)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 nhân viên dựa vào ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        public int DeleteEmployee(Guid employeeID)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
