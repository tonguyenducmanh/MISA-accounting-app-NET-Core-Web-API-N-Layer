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

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        #endregion

        #region PutMethod

        #endregion


    }
}
