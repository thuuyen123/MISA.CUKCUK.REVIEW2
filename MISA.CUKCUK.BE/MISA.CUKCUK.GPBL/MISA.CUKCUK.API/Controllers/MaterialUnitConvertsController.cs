using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaterialUnitConvertsController : BaseEntityController<MaterialUnitConvert>
    {

        #region DECLARE

        #endregion

        #region Contructor
        public MaterialUnitConvertsController(IBaseService<MaterialUnitConvert> baseService, IBaseRepository<MaterialUnitConvert> baseRepository, IMaterialUnitConvertService materialUnitConvertService) : base(baseService, baseRepository)
        {
        }
        #endregion

        #region Method


        #endregion

    }

}
