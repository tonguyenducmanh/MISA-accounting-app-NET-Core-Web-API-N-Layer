using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;

namespace MISA.WEB08.AMIS.API
{
    /// <summary>
    /// Các api liên quan tới việc lấy dữ liệu chức vụ từ bảng positions trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        #region Field

        private IPositionBL _positionBL;

        #endregion

        /// <summary>
        /// Hàm khởi tạo để truyền configuration dùng để get connection string từ file
        /// appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        #region Constructor

        public PositionsController(IPositionBL positionBL)
        {
            _positionBL = positionBL;
        }

        #endregion

        /// danh sách các API liên quan tới việc lấy thông tin
        #region GETMethod

        /// <summary>
        /// API lấy danh sách toàn bộ chức vụ
        /// </summary>
        /// <returns>Danh sách chức vụ</returns>
        /// Created by : TNMANH (27/09/2022)
        [HttpGet("")]
        public IActionResult GetAllPositions()
        {

            try
            {
                // thực hiện gọi vào DB
                var positions = _positionBL.GetAllPositions();

                return StatusCode(StatusCodes.Status200OK, positions);
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

        #endregion
    }
}
