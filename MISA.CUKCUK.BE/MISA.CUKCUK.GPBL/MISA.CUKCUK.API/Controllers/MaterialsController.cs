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
    public class MaterialsController : BaseEntityController<Material>
    {
        #region DECLARE
        ServiceResult _serviceResult;
        IMaterialService _materialService;
        IBaseRepository<Material> _baseRepository;
        #endregion


        #region Contructor
        public MaterialsController(
            IBaseRepository<Material> baseRepository,
            IMaterialService materialService) : base(materialService, baseRepository)
        {
            _materialService = materialService;
            _serviceResult = new ServiceResult();
            _baseRepository = baseRepository;

            
        }
        #endregion

        #region Method
        /// <summary>
        /// Sinh mã nguyên vật liệu theo tên NVL
        /// </summary>
        /// <returns>Mã nguyên vật liệu</returns>
        /// Created By : TTUyen (30/9/2021)
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
        /// Created By : TTUyen (30/9/2021)

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
                    userMsg= ApplicationCore.Properties.ResourcesVN.ErrorUserMsgExeption,
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
