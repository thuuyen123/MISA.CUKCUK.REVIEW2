using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Repository
{


    public class MaterialRepository : IMaterialRepository, IDisposable
    {

        /// <summary>
        /// Kết nối tới DB
        /// </summary>
        protected IDbConnection _dbConnection;

        /// <summary>
        /// Thông tin kết nối 
        /// </summary>
        public readonly string _connectionString;

        #region Contructor

        public MaterialRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CukCukDatabase");
        }


        #endregion


        #region Method

        public int Add(Material material)
        {
            try
            {

                Guid materialId = Guid.NewGuid();

                _dbConnection = new MySqlConnection(_connectionString);

                _dbConnection.Open();

                var count = 0;

                var row = 0;

                var parameters = new DynamicParameters();

                var rowEffects = 0;

                var transaction = _dbConnection.BeginTransaction();

                var properties = material.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var propertyName = property.Name;

                    var notMapPro = property.GetCustomAttributes(typeof(MISANotMap), true);

                    if (notMapPro.Length == 0)
                    {

                        var propertyValue = property.GetValue(material);

                        if (propertyName == "MaterialId")
                        {
                            propertyValue = Guid.NewGuid();

                            materialId = (Guid)propertyValue;
                        }

                        parameters.Add($"${propertyName}", propertyValue);
                    }
                }

                rowEffects = _dbConnection.Execute($"Proc_InsertMaterial", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                row++;

                if (material?.MaterialDetails?.Count > 0)
                {
                    List<Material> materialDetails = material.MaterialDetails;

                    for (int i = 0; i < materialDetails.Count; i++)
                    {
                        var proDetails = materialDetails[i].GetType().GetProperties();

                        foreach (var property in proDetails)
                        {
                            var propertyName = property.Name;

                            var notMapPro = property.GetCustomAttributes(typeof(MISANotMap), true);

                            if (notMapPro.Length == 0)
                            {

                                var propertyValue = property.GetValue(materialDetails[i]);

                                var propertyType = property.PropertyType;

                                if (propertyName == "MaterialId")
                                {
                                    propertyValue = Guid.NewGuid();
                                }

                                if (propertyName == "ParentId")
                                {
                                    propertyValue = materialId;
                                }
                                parameters.Add($"${propertyName}", propertyValue);

                            }

                        }
                        rowEffects = _dbConnection.Execute($"Proc_InsertMaterial", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                        row++;
                        count++;
                    }
                }

                if (row == count + 1)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                return rowEffects;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        /// <summary>
        /// Xóa bản ghi     
        /// </summary>
        /// <param name="materialId">id bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        ///  CreateBy: TTUyen
        public int Delete(Guid materialId)
        {
            try
            {
                _dbConnection = new MySqlConnection(_connectionString);

                _dbConnection.Open();

                var rowEffects = 0;

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();

                        parameters.Add($"@$MaterialId", materialId);

                        rowEffects = _dbConnection.Execute("Proc_DeleteMaterialById", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                return rowEffects;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }

        }

        /// <summary>
        /// Hàm get ra danh sách
        /// </summary>
        /// <returns>List danh sách</returns>
        ///  CreateBy: TTUyen
        public IEnumerable<Material> Get()
        {
            try
            {
                _dbConnection = new MySqlConnection(_connectionString);

                _dbConnection.Open();

                var sql = $"Proc_GetMaterials";

                var entities = _dbConnection.Query<Material>(sql, commandType: CommandType.StoredProcedure);

                return entities;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// Lọc theo id bản ghi
        /// </summary>
        /// <param name="materialId">id bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// CreateBy: TTUyen
        public Material GetById(Guid materialId)
        {
            try
            {
                _dbConnection = new MySqlConnection(_connectionString);

                _dbConnection.Open();

                var parameters = new DynamicParameters();

                parameters.Add($"@$MaterialId", materialId);

                var res = _dbConnection.Query<Material>($"Proc_GetMaterialById", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// Cập nhật bản ghih
        /// </summary>
        /// <param name="material"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public int Update(Material material, Guid materialId)
        {
            try
            {
                int rowEffects = 0;
                int count = 0;
                var res = 0;
                _dbConnection = new MySqlConnection(_connectionString);
                _dbConnection.Open();
                var transaction = _dbConnection.BeginTransaction();
                var dynamicParam = new DynamicParameters();
                var dynamicParamDetail = new DynamicParameters();
                var parametersDelete = new DynamicParameters();
                var properties = material.GetType().GetProperties();

                //var parameters = MappingDbType(material);
                foreach (var prop in properties)
                {
                    // kiểm tra có thuộc tính nào là not map không
                    var notMapProp = prop.GetCustomAttributes(typeof(MISANotMap), true);

                    // các thuộc tính là not map sẽ ko dc thêm vào database
                    if (notMapProp.Length == 0)
                    {
                        //Lấy tên thuộc tính
                        var propName = prop.Name;
                        //Lấy giá trị thuộc tính
                        var propValue = prop.GetValue(material);

                        dynamicParam.Add($"${propName}", propValue);
                    }
                }

                res = _dbConnection.Execute($"Proc_UpdateMaterial", param: dynamicParam, commandType: CommandType.StoredProcedure, transaction: transaction);

                rowEffects++;

                int status = -1;
                if (material?.MaterialDetails?.Count > 0)
                {
                    List<Material> materialDetails = material.MaterialDetails;
                    for (int i = 0; i < materialDetails.Count; i++)
                    {
                        var materialDetail = materialDetails[i].GetType().GetProperties();
                        foreach (var propDetail in materialDetail)
                        {

                            var notMapPropDetail = propDetail.GetCustomAttributes(typeof(MISANotMap), true);

                            var displayNameDetail = propDetail.GetCustomAttributes(typeof(DisplayName), true);

                            var name = string.Empty;

                            if (displayNameDetail.Length > 0)
                                name = (displayNameDetail[0] as DisplayName).Name;

                            if (notMapPropDetail.Length == 0)
                            {

                                var propName = propDetail.Name;

                                var propValue = propDetail.GetValue(materialDetails[i]);

                                dynamicParamDetail.Add($"${propName}", propValue);

                                if (propName == "MaterialId")
                                {
                                    parametersDelete.Add("$MaterialId", propValue);
                                }
                            }
                            else
                            {

                                if (name == "Trạng thái")
                                {
                                    status = (int)propDetail.GetValue(materialDetails[i]);
                                }
                            }
                        }

                        if (status == 1)
                        {
                            dynamicParamDetail.Add("$MaterialId", Guid.NewGuid());
                            dynamicParamDetail.Add("$ParentId", materialId);
                            res = _dbConnection.Execute("Proc_InsertMaterial", param: dynamicParamDetail, transaction: transaction, commandType: CommandType.StoredProcedure);
                            rowEffects++;
                            count++;
                        }
                        else if (status == 2)
                        {
                            res = _dbConnection.Execute("Proc_UpdateMaterial", param: dynamicParamDetail, transaction: transaction, commandType: CommandType.StoredProcedure);
                            rowEffects++;
                            count++;
                        }
                        else if (status == 3)
                        {
                            res = _dbConnection.Execute("Proc_DeleteMaterialUnitConvertById", param: parametersDelete, transaction: transaction, commandType: CommandType.StoredProcedure);
                            rowEffects++;
                            count++;
                        }

                    }
                }

                if (rowEffects == count + 1)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        /// <summary>
        /// Map dữ liệu của 1 material sang thành dynamic parameters dùng cho truy vấn SQL
        /// </summary>
        /// <typeparam name="Material"></typeparam>
        /// <param name="material"></param>
        /// <returns>dynamic parameters đã được format đúng</returns>
        protected DynamicParameters MappingDbType(Material material)
        {

            try
            {
                var properties = material.GetType().GetProperties();

                var parameters = new DynamicParameters();

                foreach (var property in properties)
                {
                    var propertyName = property.Name;

                    var notMapPro = property.GetCustomAttributes(typeof(MISANotMap), true);

                    if (notMapPro.Length == 0)
                    {

                        var propertyValue = property.GetValue(material);

                        var propertyType = property.PropertyType;

                        if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                        {
                            parameters.Add($"@${propertyName}", propertyValue, DbType.String);
                        }
                        else
                        {
                            parameters.Add($"@${propertyName}", propertyValue);
                        }
                    }
                }

                return parameters;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// Đóng kết nối
        /// </summary>
        ///  CreateBy: TTUyen
        public void Dispose()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }

        public IEnumerable<Material> CheckMaterialCodeExist(Material material)
        {

            try
            {
                _dbConnection = new MySqlConnection(_connectionString);

                _dbConnection.Open();

                var parameters = new DynamicParameters();

                var state = -1;
                
                if(material.EntityState == EntityState.AddNew)
                {
                    state = 0;
                }
                else
                {
                    state = 1;
                }

                parameters.Add("@$MaterialId", material.MaterialId);

                parameters.Add("@$MaterialCode", material.MaterialCode);

                parameters.Add("@$State", state);

                var res = _dbConnection.Query<Material>("Proc_CheckMaterialCodeExist", param: parameters, commandType: CommandType.StoredProcedure);

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

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
                                        if (item.FilterValue != ""){
                                            where += $" AND vm.{item.DataField} NOT LIKE CONCAT('%', '{item.FilterValue}' , '%') ";
                                        }
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

        public Material GetMaterialDetailById(Guid materialId)
        {
            try
            {
                _dbConnection = new MySqlConnection(_connectionString);

                _dbConnection.Open();

                Material material = null;

                IDataReader reader = null;

                var dynamicParam = new DynamicParameters();

                dynamicParam.Add("$MaterialId", materialId);

                reader = _dbConnection.ExecuteReader("Proc_GetMaterialDetailById", param: dynamicParam, commandType: CommandType.StoredProcedure);

                var dataset = ConvertDataReaderToDataSet(reader);
                if (dataset?.Tables?.Count > 0)
                {
                    var dtMaterial = dataset.Tables[0];
                    var dtMaterialDetails = dataset.Tables[1];
                    material = ConvertDataTableToList<Material>(dtMaterial).SingleOrDefault();
                    var materialDetails = ConvertDataTableToList<Material>(dtMaterialDetails).ToList();
                    if (material != null)
                    {
                        material.MaterialDetails = materialDetails;
                    }
                }
                return material;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private IEnumerable<T> ConvertDataTableToList<T>(DataTable dataTable)
        {
            var result = new List<T>();
            if (dataTable?.Rows?.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    T item = GetItem<T>(row);
                    result.Add(item);
                }
            }
            return result;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        private DataSet ConvertDataReaderToDataSet(IDataReader data)
        {
            DataSet ds = new DataSet();
            int i = 0;
            while (!data.IsClosed)
            {
                ds.Tables.Add("Table" + (i + 1));
                ds.EnforceConstraints = false;
                ds.Tables[i].Load(data);
                i++;
            }
            return ds;
        }
    }
}
