using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.COMMON.Entities;
using MISA.WEB08.AMIS.COMMON.Resources;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        // Danh sách các API liên quan tới việc lấy thông tin của 1 table
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by : TNMANH (29/09/2022)
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// API check trùng mã record
        /// </summary>
        /// <returns>Records có mã trùng</returns>
        /// Created by : TNMANH (29/09/2022)
        public T GetDuplicateCode(string recordCode)
        {
            return _baseDL.GetDuplicateCode(recordCode);
        }


        /// <summary>
        /// API lấy mã record lớn nhất
        /// </summary>
        /// <returns>Mã record lớn nhất</returns>
        /// Created by : TNMANH (29/09/2022)
        public string GetMaxRecordCode()
        {
            return _baseDL.GetMaxRecordCode();
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 record theo ID đầu vào
        /// </summary>
        /// <param name="recordID">ID của record</param>
        /// <returns>Thông tin của record theo ID</returns>
        /// Created by : TNMANH (29/09/2022)
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }
        #endregion


        // Danh sách các API liên quan tới việc thêm mới 1 record vào 1 table

        #region PostMethod


        /// <summary>
        /// API Thêm mới 1 record
        /// </summary>
        /// <param name="record">Thông tin record mới</param>
        /// <returns>Status 201 created, recordID</returns>
        /// Created by : TNMANH (29/09/2022)
        public Guid InsertRecord(T record)
        {
            return _baseDL.InsertRecord(record);
        }

        #endregion


        #region DeleteMethod

        /// <summary>
        /// API xóa 1 record dựa vào ID
        /// </summary>
        /// <param name="recordID">ID của record</param>
        /// <returns>Status 200 OK, recordID / Status 400 badrequest</returns>
        /// Created by : TNMANH (29/09/2022)
        public ServiceResponse DeleteRecord(Guid recordID)
        {
            var deletedRecordID = _baseDL.DeleteRecord(recordID);

            if (deletedRecordID != Guid.Empty)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = deletedRecordID
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = new ErrorResult(
                    ErrorCode.DeleteFailed,
                    MISAResource.DevMsg_DeleteFailed,
                    MISAResource.UserMsg_DeleteFailed,
                    MISAResource.MoreInfo_DeleteFailed
                    )
                };
            }
        }


        #endregion
    }
}
