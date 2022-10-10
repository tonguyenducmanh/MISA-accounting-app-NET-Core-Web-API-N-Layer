using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.COMMON.Resources;
using MISA.WEB08.AMIS.BL;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Controler base chứa các api CRUD cơ bản
    /// </summary>
    /// Created by : TNMANH (29/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        /// <summary>
        /// Các field được dùng trong class BaseController
        /// </summary>
        /// Created by: TNMANH (29/09/2022)
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm constructor để thực hiện Dependencies Injection
        /// </summary>
        /// <param name="baseBL">Interface tương ứng</param>
        /// Created by: TNMANH (29/09/2022)
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        /// danh sách các API liên quan tới việc lấy thông tin 1 bảng
        #region GETMethod
        /// <summary>
        /// API lấy danh sách toàn bộ record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by : TNMANH (29/09/2022)
        [HttpGet("")]
        public IActionResult GetAllRecords()
        {
            try
            {
                // thực hiện gọi vào DB
                var records = _baseBL.GetAllRecords();
                // trả về status code và danh sách record
                if(records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // trả về status code và object thông báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                ));
            }
        }

        /// <summary>
        /// API check trùng mã record
        /// <param name="recordCode">Mã của record</param>
        /// </summary>
        /// <returns>Records có mã trùng</returns>
        /// Created by : TNMANH (29/09/2022)
        [HttpGet("duplicate-code")]
        public IActionResult GetDuplicateCode(string recordCode)
        {
            try
            {
                // Thực hiện gọi vào db
                var duplicatedRecord = _baseBL.GetDuplicateCode(recordCode);

                // Trả về Status code và kết quả
                if(duplicatedRecord != null)
                {
                    return StatusCode(StatusCodes.Status200OK, duplicatedRecord);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về Status code và object báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary>
        /// API lấy mã record lớn nhất
        /// </summary>
        /// <returns>Mã record lớn nhất</returns>
        /// Created by : TNMANH (29/09/2022)
        [HttpGet("max-code")]
        public IActionResult GetMaxRecordCode()
        {
            try
            {
                // thực hiện gọi vào db
                var maxCode = _baseBL.GetMaxRecordCode();

                // Trả về Status code và kết quả
                return StatusCode(StatusCodes.Status200OK, maxCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về Status code và object báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 record theo ID đầu vào
        /// </summary>
        /// <param name="recordID">ID của record</param>
        /// <returns>Thông tin của record theo ID</returns>
        /// Created by : TNMANH (29/09/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                // Lấy thông tin chi tiết 1 record
                var record = _baseBL.GetRecordByID(recordID);
                // Trả về status code và kết quả trả về
                if(record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code kèm theo kết quả báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }

        }

        #endregion

        #region PostMethod


        /// <summary>
        /// API Thêm mới 1 record
        /// </summary>
        /// <param name="record">Thông tin nhân record</param>
        /// <returns>Status 201 created, recordID</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] T record)
        {
            try
            {
                // thực hiện gọi vào db
                var result = _baseBL.InsertRecord(record);

                // kiểm tra kết quả trả về từ db và trả về status code kèm data tương ứng
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code kèm theo kết quả báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                     HttpContext.TraceIdentifier
                ));
            }
        }

        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 record dựa vào employeeID
        /// </summary>
        /// <param name="recordID">ID của record định sửa</param>
        /// <param name="record">Giá trị của record sửa</param>
        /// <returns>Status 200 OK, recordID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPut("{recordID}")]
        public IActionResult UpdateRecord([FromRoute] Guid recordID, [FromBody] T record)
        {

            try
            {
                // thực hiện cập nhật dữ liệu trong db
                var result = _baseBL.UpdateRecord(recordID, record);

                // kiểm tra kết quả trả về từ db và trả về status code kèm data tương ứng
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code kèm theo kết quả báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                     HttpContext.TraceIdentifier
                ));
            }
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 record dựa vào ID
        /// </summary>
        /// <param name="recordID">ID của record</param>
        /// <returns>Status 200 OK, recordID / Status 400 badrequest</returns>
        /// Created by : TNMANH (29/09/2022)
        [HttpDelete("{recordID}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordID)
        {
            try
            {
                // thực hiện xóa 1 bản ghi trong db
                var record = _baseBL.DeleteRecord(recordID);

                // trả về status code và kết quả
                if (record.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, record);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // trả về status code và object báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier

                    ));
            }
        }

        #endregion
    }
}
