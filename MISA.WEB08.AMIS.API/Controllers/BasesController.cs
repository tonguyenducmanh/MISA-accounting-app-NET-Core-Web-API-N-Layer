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

        #endregion
    }
}
