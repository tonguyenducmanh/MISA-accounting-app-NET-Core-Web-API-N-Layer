using Dapper;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;
using MySqlConnector;
using static Dapper.SqlMapper;

namespace MISA.WEB08.AMIS.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        /// Created by : TNMANH (24/09/2022)
        public PagingData FilterEmployee(string? keyword, int? pageNumber, int? pageSize)
        {
            // Chuẩn bị câu lệnh MySQL
            string storeProcedureName = MISAResource.ProcGetEmployeeFilter;

            // Chèn parameter cho procedure
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add(MISAResource.Param_PageNumber, pageNumber);
            parameters.Add(MISAResource.Param_PageSize, pageSize);
            parameters.Add(MISAResource.Param_Search, keyword);

            // Tạo các giá trị trả về
            GridReader employeesFiltered;
            List<Employee> employees;
            int totalRecord;

            // Tạo connection
            string connectionString = DataContext.MySQLConnectionString;
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào trong Database
                    employeesFiltered = sqlConnection.QueryMultiple(
                        storeProcedureName,
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                // Lấy ra các kết quả tương ứng với từng table (prop này có 2 table trả về)
                employees = employeesFiltered.Read<Employee>().ToList();
                totalRecord = (int)employeesFiltered.ReadSingle().TotalCount;

            }

            // Tạo ra số trang
            int? totalPage;
            if(totalRecord == 0)
            {
                totalPage = 1;
            }
            else if(totalRecord % pageSize == 0)
            {
                totalPage = totalRecord/ pageSize;
            }
            else
            {
                totalPage = (int)Math.Ceiling(Convert.ToDecimal(totalRecord / pageSize) + 1);
            }

            // Trả về status code kèm theo object kết quả
            return new PagingData()
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPage = totalPage,
                Data = employees,
                TotalRecord = totalRecord,
            };
        }

        #endregion

        // Danh sách các API liên quan tới xóa nhân viên
        #region DeleteMethod

        /// <summary>
        /// API xóa nhiều nhân viên theo danh sách IDs
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns>True hoặc false, true là xóa thành công, false là xóa không thành công</returns>
        /// Created by : TNMANH (05/10/2022)
        public bool DeleteManyEmployee(Guid[] employeeIDs)
        {
            // Khởi tạo store procedure
            string storeProcedureName = string.Format(MISAResource.Proc_Delete_ManyRecord, typeof(Employee).Name.ToLower());

            // Khởi tạo các parameter truyền vào trong store procedure
            DynamicParameters parameters = new DynamicParameters();

            // Tạo biến số lượng kết quả
            int nunmberOfAffectedRows = 0;
            bool result = false;

            // Xử lý mảng string đầu vào thành chuỗi có dạng '"abc", "cde"'
            string listStringIDs = GenerateParamDeleteMany(employeeIDs);

            // Nếu chuỗi rỗng return false luôn
            if(listStringIDs == string.Empty)
            {
                return result;
            }

            //thêm giá trị vào trong parameters
            parameters.Add(MISAResource.Param_ID, listStringIDs);

            // Tạo connection
            using (var sqlConnection = new MySqlConnection(DataContext.MySQLConnectionString))
            {
                sqlConnection.Open();

                // Khởi tạo transaction
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    try
                    {
                        // thực hiện truy vấn tới database
                        nunmberOfAffectedRows = sqlConnection.Execute(
                        storeProcedureName, 
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure,
                        transaction: sqlTransaction
                        );

                        // commit transaction nếu thành công
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        // Rollback transaction nếu có bất kỳ 1 exception nào
                        sqlTransaction.Rollback();
                    }
                }
            };

            // khi trả về truy vấn thành công thì chuyển result về true
            if (nunmberOfAffectedRows > 0)
            {
                result = true;
            }

            return result;

        }
        #endregion

        #region OtherMethod

        /// <summary>
        /// Method giúp biến mảng guid record id thành chuỗi để truyền vào param của procedure cho method deletemany
        /// </summary>
        /// <param name="recordIDs">mảng guid truyền vào</param>
        /// <returns>chuỗi recordID</returns>
        /// Created by : TNMANH (05/10/2022)
        public string GenerateParamDeleteMany(Guid[] recordIDs)
        {
            try
            {
                // Nếu list rỗng result chuỗi rỗng luôn
                if (recordIDs.Length == 0)
                {
                    return string.Empty;
                }

                // Nối List vào thành 1 chuỗi
                return string.Join(",", recordIDs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Có lỗi luôn return về chuỗi rỗng
                return string.Empty;
            }
        } 
        #endregion

    }
}
