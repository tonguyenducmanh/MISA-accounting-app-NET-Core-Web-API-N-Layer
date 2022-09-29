using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.COMMON.Resources;
using MISA.WEB08.AMIS.BL;

namespace MISA.WEB08.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

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


                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                return StatusCode(StatusCodes.Status200OK, duplicatedRecord);
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
                return StatusCode(StatusCodes.Status200OK, record);

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
                var record = _baseBL.DeleteRecord(recordID);
                // trả về status code và kết quả
                return StatusCode(StatusCodes.Status200OK, record);
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
