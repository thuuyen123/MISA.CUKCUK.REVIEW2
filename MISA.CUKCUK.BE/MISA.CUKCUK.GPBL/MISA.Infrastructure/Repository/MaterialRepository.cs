using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Repository
{

    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {

        #region Contructor

        public MaterialRepository(IConfiguration configuration) : base(configuration)
        {
        }


        #endregion


        #region Method
        public Paging<Material> GetMaterialsByPagingFilter(MaterialFilter filter, List<FilterItem> listFilter)
        {
            try
            {
                using (_dbConnection = new MySqlConnection(_connectionString))
                {
                    Paging<Material> pageNew = new Paging<Material>();

                    var where = $"1=1 ";

                    foreach (var item in listFilter)
                    {
                        if (item.Enable)
                        {
                            if (item.FilterType == "FilterText")
                            {
                                switch (item.FilterOption)
                                {
                                    // chứa
                                    case "1":
                                        where += $" AND vm.{item.DataField} LIKE CONCAT('%', '{item.FilterValue}' , '%') ";
                                        break;
                                    // bằng
                                    case "2":
                                        where += $" AND vm.{item.DataField} LIKE '{item.FilterValue}' ";
                                        break;
                                    // bắt đầu bằng
                                    case "3":
                                        where += $" AND vm.{item.DataField} LIKE CONCAT('{item.FilterValue}' , '%') ";
                                        break;
                                    // kết thúc bằng
                                    case "4":
                                        where += $" AND vm.{item.DataField} LIKE CONCAT('%', '{item.FilterValue}') ";
                                        break;
                                    // không chứa
                                    case "5":
                                        where += $" AND vm.{item.DataField} NOT LIKE CONCAT('%', '{item.FilterValue}' , '%') ";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                where += $"AND vm.{item.DataField} LIKE '{item.FilterValue}'";
                            }
                        }
                    }


                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@$Column", null);

                    parameters.Add("@$Sort", null);

                    parameters.Add("@$Where", where);

                    parameters.Add("@$PageIndex", filter.PageIndex);

                    parameters.Add("@$PageSize", filter.PageSize);

                    parameters.Add("@$TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    parameters.Add("@$TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var sqlCommand = "Proc_GetMaterialsPagingFilter";

                    var materials = _dbConnection.Query<Material>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);

                    var totalPage = parameters.Get<int>("@$TotalPage");

                    var totalRecord = parameters.Get<int>("@$TotalRecord");

                    pageNew = new Paging<Material>
                    {
                        TotalRecord = totalRecord,
                        TotalPage = totalPage,
                        PageIndex = filter.PageIndex,
                        Data = materials,
                        PageSize = filter.PageSize
                    };
                    return pageNew;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public string GetNumberMaterialCodeMax(string materialName)
        {
            try
            {
                using (_dbConnection = new MySqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@$MaterialName", materialName);


                    var number = _dbConnection.QueryFirstOrDefault<string>("Proc_GetNumberMaterialCodeMax", param: parameters, commandType: CommandType.StoredProcedure);

                    return number;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            #endregion
        }
    }
}
