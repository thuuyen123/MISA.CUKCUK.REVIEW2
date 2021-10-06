using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Services;
using MySqlConnector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        #region DECLARE
        ServiceResult _serviceResult;
        IMaterialService _materialService;
        IMaterialRepository _materialRepository;
        #endregion


        #region Contructor
        public MaterialsController(
            IMaterialService materialService,
        IMaterialRepository materialRepository)
        {
            _materialService = materialService;
            _serviceResult = new ServiceResult();
            _materialRepository = materialRepository;
        }
        #endregion



        #region Method
        /// <summary>
        /// Hiển thị danh sách 
        /// </summary>
        /// <returns>StatusCode</returns>
        ///Created By : TTUyen 
        ///
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entities = _materialRepository.Get();
                _serviceResult.Data = entities;
                _serviceResult.Status = RequestStatus.Complete;
                return StatusCode(200, _serviceResult);
            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsgVN = ex.Message,
                    userMsgVN = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                    errorCode = "misa-001",
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;

                return StatusCode(500, _serviceResult);
            }
        }

        /// <summary>
        /// API hiển thị đối tượng theo id
        /// </summary>
        /// <param name="materialId">Khóa chính bản ghi</param>
        /// <returns>StatusCode</returns>
        /// Created By : TTUyen (31/09/2021)
        [HttpGet("{materialId}")]
        public IActionResult GetById(Guid materialId)
        {

            try
            {
                var material = _materialService.GetById(materialId);

                if (material == null)
                {
                    _serviceResult.Data = material;
                    _serviceResult.Messager = ApplicationCore.Properties.ResourcesVN.DataEmpty;
                    _serviceResult.Status = RequestStatus.Fail;
                    return StatusCode(204, _serviceResult);

                };
                _serviceResult.Data = material;
                _serviceResult.Status = RequestStatus.Complete;
                var response = StatusCode(200, _serviceResult);
                return response;
            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsgVN = ex.Message,
                    userMsgVN = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;
                return StatusCode(500, _serviceResult);
            }
        }

        /// <summary>
        /// API thêm mới 1 bản ghi
        /// </summary>
        /// <param name="material">Object cần thêm</param>
        /// <returns>StatusCode</returns>
        /// Created By : TTUyen (01/10/2021)
        [HttpPost]
        public IActionResult Insert(Material material)
        {
            try
            {
                var result = _materialService.Add(material);
                if (result.ErrorCode == MISACode.IsValid)
                {
                    result.Status = RequestStatus.Complete;
                    return StatusCode(201, result);
                }
                else if (result.ErrorCode == MISACode.NoValid)
                {
                    result.Status = RequestStatus.Fail;
                    return BadRequest(result);
                }
                else
                {
                    result.Status = RequestStatus.Fail;
                    return StatusCode(500, result);
                }

            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsgVN = ex.Message,
                    userMsgVN = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                    errorCode = "misa-001",
                    tradeId = "",
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;
                return StatusCode(500, _serviceResult);
            }
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="materialId">Khóa chính bản ghi cần xóa</param>
        /// <returns></returns>
        /// Created By : TTUyen (01/10/2021)
        [HttpDelete("{materialId}")]
        public IActionResult Delete(Guid materialId)
        {
            try
            {
                var materialById = _materialService.GetById(materialId);
                if (materialById == null)
                {
                    var errorMsg = new
                    {
                        userMgs = ApplicationCore.Properties.ResourcesVN.MsgIsValid,
                        data = "materialId",
                        errorCode = "misa-005",
                    };
                    _serviceResult.ErrorCode = MISACode.NoValid;
                    _serviceResult.Data = errorMsg;
                    return BadRequest(_serviceResult);
                }
                var result = _materialService.Delete(materialId);
                if (result.ErrorCode == MISACode.IsValid)
                {
                    result.Status = RequestStatus.Complete;
                    return StatusCode(200, result);
                }
                else
                {
                    result.Status = RequestStatus.Fail;
                    return StatusCode(204, result);
                }

            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsgVN = ex.Message,
                    userMsgVN = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;
                return StatusCode(500, _serviceResult);
            }
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// Created By : TTUyen (01/10/2021)
        [HttpPut("{materialId}")]
        public IActionResult Update(Material material, Guid materialId)
        {
            try
            {
                var result = _materialService.Update(material, materialId);
                if (result.ErrorCode == MISACode.IsValid)
                {
                    result.Status = RequestStatus.Complete;
                    return StatusCode(200, result);
                }
                else if (result.ErrorCode == MISACode.NoValid)
                {
                    result.Status = RequestStatus.Fail;
                    return BadRequest(result);
                }
                else
                {
                    result.Status = RequestStatus.Fail;
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsgVN = ex.Message,
                    userMsgVN = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                    errorCode = "misa-001",
                    tradeId = "",
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;
                return StatusCode(500, _serviceResult);
            }

        }

        /// <summary>
        /// Lấy danh sách chi tiết NVL
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpGet("Detail/{materialId}")]
        public IActionResult GetMaterialDetailById(Guid materialId)
        {
            try
            {
                var material = _materialRepository.GetMaterialDetailById(materialId);

                if (material == null)
                {
                    _serviceResult.Data = material;
                    _serviceResult.Messager = ApplicationCore.Properties.ResourcesVN.DataEmpty;
                    _serviceResult.Status = RequestStatus.Fail;
                    return StatusCode(204, _serviceResult);

                };
                _serviceResult.Data = material;
                _serviceResult.Status = RequestStatus.Complete;
                var response = StatusCode(200, _serviceResult);
                return response;
            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsgVN = ex.Message,
                    userMsgVN = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;
                return StatusCode(500, _serviceResult);
            }
        }

        /// <summary>
        /// Sinh mã nguyên vật liệu theo tên NVL
        /// </summary>
        /// <returns>Mã nguyên vật liệu</returns>
        /// Created By : TTUyen (01/10/2021)
        [HttpGet("NewMaterialCode")]
        public IActionResult GetNewMaterialCode([FromQuery] string materialName)
        {
            try
            {

                var materialCode = _materialService.GetNewMaterialCodeByMaterialName(materialName);
                _serviceResult.Data = materialCode;
                _serviceResult.Status = RequestStatus.Complete;
                return StatusCode(200, _serviceResult);
            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsg = ex.Message,
                    userMsg = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;
                return StatusCode(500, _serviceResult);
            }
        }


        /// <summary>
        /// Lấy danh sách nhân viên theo từng điều kiện lọc
        /// </summary>
        /// HTTPStatusCode - 200 : có dữ liệu trả về
        /// HTTPStatusCode - 204 : không có dữ liệu
        /// <returns>Danh sách nhân viên theo điều kiện lọc</returns>
        /// Created By : TTUyen (01/10/2021)

        [HttpPost("Filter")]
        public IActionResult GetEmployeesByPaging([FromQuery] MaterialFilter materialFilter, [FromBody] List<FilterItem> listFilter)
        {
            try
            {
                var paging = _materialService.GetMaterialsByPagingFilter(materialFilter, listFilter);

                // Xử lý kết quả trả về cho client.
                if (paging.Data.Any())
                {
                    return StatusCode(200, paging);
                }

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                var msgError = new
                {
                    devMsg = ex.Message,
                    userMsg = ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
                };
                _serviceResult.Messager = ex.Message;
                _serviceResult.Data = msgError;
                _serviceResult.Status = RequestStatus.Exception;
                return StatusCode(500, _serviceResult);
            }

        }
        #endregion


    }

}
