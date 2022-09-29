using MISA.Web08.AMIS.COMMON.Enums;
using MISA.WEB08.AMIS.COMMON.CustomAttribute;
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

        // Danh sách các method liên quan tới việc lấy thông tin của 1 table
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

        // Danh sách các method liên quan tới việc Validate dữ liệu đầu vào
        #region ValidateMethod

        /// <summary>
        /// Lấy ra recordCode từ record
        /// </summary>
        /// <param name="record">record đầu vào</param>
        /// <returns>recordCode trả về</returns>
        /// Created by : TNMANH (29/09/2022)
        public string GetRecordCode(T record)
        {
            // Lấy ra trường record code trong object recor
            var props = typeof(T).GetProperties();
            List<string> validateFailed = new List<string>();
            string recordCode = "";
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(record);
                var mustHave = (RecordCodeAttribute?)Attribute.GetCustomAttribute(prop, typeof(RecordCodeAttribute));
                if (mustHave != null)
                {
                    recordCode = propValue.ToString();
                }
            }
            return recordCode;
        }

        /// <summary>
        /// Check mã trùng dựa vào record
        /// </summary>
        /// <param name="record">record đầu vào</param>
        /// <returns>trả về ServiceResponse</returns>
        /// Created by : TNMANH (29/09/2022)
        public ServiceResponse CheckDuplicateEmployeeCode(T record)
        {

            // Lấy ra trường record code trong object recor
            string recordCode = GetRecordCode(record);
            // Kiểm tra xem mã có bị trùng chưa
            var testDuplicateCode = GetDuplicateCode(recordCode);

            if (testDuplicateCode != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = new ErrorResult(
                        ErrorCode.DuplicateCode,
                        MISAResource.DevMsg_DuplicatedCode,
                        MISAResource.UserMsg_DuplicatedCode,
                        MISAResource.MoreInfo_DupplicatedCode
                        )
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = ""
                };
            }
        }

        /// <summary>
        /// Validate dữ liệu truyền lên
        /// </summary>
        /// <param name="record">Đối tượng record cần validate</param>
        /// <returns>Đối tượng ServiceRespone</returns>
        /// Created by : TNMANH (27/09/2022)
        public ServiceResponse ValidateRequestData(T record)
        {
            // Validate dữ liệu đầu vào
            var props = typeof(T).GetProperties();
            List<string> validateFailed = new List<string>();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(record);
                var mustHave = (MustHave?)Attribute.GetCustomAttribute(prop, typeof(MustHave));
                if (mustHave != null && string.IsNullOrEmpty(propValue?.ToString()))
                {
                    validateFailed.Add(mustHave.ErrorMessage);
                }
            }

            // Check xem nếu có lỗi văng ra kết quả luôn khỏi chạy đoạn dưới
            if (validateFailed.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data =
                    new ErrorResult(
                    ErrorCode.EmptyCode,
                    MISAResource.DevMsg_ValidateFailed,
                    MISAResource.UserMsg_ValidateFailed,
                    validateFailed
                    )
                };
            }
            return new ServiceResponse
            {
                Success = true
            };
        }

        #endregion

        // Danh sách các method liên quan tới việc thêm mới 1 record vào 1 table

        #region PostMethod

        /// <summary>
        /// API Thêm mới 1 record
        /// </summary>
        /// <param name="record">Thông tin record mới</param>
        /// <returns>Status 201 created, recordID</returns>
        /// Created by : TNMANH (29/09/2022)
        public ServiceResponse InsertRecord(T record)
        {
            var validateResult = ValidateRequestData(record);
            var checkDuplicateResult = CheckDuplicateEmployeeCode(record);

            // trả về kết quả mã trùng trước
            if (checkDuplicateResult.Success == false)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = checkDuplicateResult?.Data
                };
            }

            // trả về kết quả validate sau
            if (validateResult != null && validateResult.Success)
            {
                var newRecordID = _baseDL.InsertRecord(record);

                if (newRecordID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = newRecordID
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = new ErrorResult(
                        ErrorCode.InsertFailed,
                        MISAResource.DevMsg_InsertFailed,
                        MISAResource.UserMsg_Exception,
                        MISAResource.MoreInfo_InsertFailed
                        )
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateResult?.Data
                };
            }

        }

        #endregion

        // Danh sách các method liên quan tới việc sửa 1 record có sẵn 1 table
        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 record dựa vào employeeID
        /// </summary>
        /// <param name="recordID">ID của record định sửa</param>
        /// <param name="record">Giá trị của record sửa</param>
        /// <returns>Status 200 OK, recordID / Status 400 badrequest</returns>
        /// Created by : TNMANH (29/09/2022)
        public ServiceResponse UpdateRecord(Guid recordID, T record)
        {

            // check trùng mã, trường hợp mà recordCode đã có sẵn nhưng thuộc record trên đó thì ta loại nó đi,
            // trường hợp mà recordCode khác record có ID trên thì ta xét xem đã trùng chưa
            string originalRecordCode = GetRecordCode(GetRecordByID(recordID));
            string currentRecordCode = GetRecordCode(record);

            if(originalRecordCode != currentRecordCode)
            {
                var checkDuplicateResult = CheckDuplicateEmployeeCode(record);

                // trả về kết quả mã trùng trước
                if (checkDuplicateResult.Success == false)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = checkDuplicateResult?.Data
                    };
                }

            }

            // validate các trường còn lại trong record
            var validateResult = ValidateRequestData(record);

            if (validateResult != null && validateResult.Success)
            {
                var editedRecordID = _baseDL.UpdateRecord(recordID, record);

                if (editedRecordID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = editedRecordID
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = new ErrorResult(
                        ErrorCode.UpdateFailed,
                        MISAResource.DevMsg_UpdatedFailed,
                        MISAResource.UserMsg_UpdatedFailed,
                        MISAResource.MoreInfo_UpdatedFailed
                        )
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateResult?.Data
                };
            }
        }

        #endregion

        // Danh sách các method liên quan tới việc xóa 1 record trong 1 table
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
