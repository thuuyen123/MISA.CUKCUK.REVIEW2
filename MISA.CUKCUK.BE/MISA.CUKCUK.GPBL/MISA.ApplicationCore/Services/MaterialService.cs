using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Services;
using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;

namespace MISA.ApplicationCore.Services
{
    public class MaterialService : IMaterialService
    {
        #region DECLARE

        private IMaterialRepository _materialRepository;
        ServiceResult _serviceResult;

        #endregion

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo Materialservice
        /// </summary>
        /// <param name="materialRepository"></param>
        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
            _serviceResult = new ServiceResult();
        }



        #endregion

        #region Method

        public Paging<Material> GetMaterialsByPagingFilter(MaterialFilter filter, List<FilterItem> listFilter)
        {
            return _materialRepository.GetMaterialsByPagingFilter(filter, listFilter);
        }

        public string GetNewMaterialCodeByMaterialName(string materialName)
        {
            var newCode = "";

            string[] words = materialName.Split(' ');

            foreach (var word in words)
            {

                if (!Char.IsNumber(word[0]))
                {
                    newCode = string.Concat(newCode, word[0]);
                }
                else
                {
                    switch (word[0])
                    {
                        case '0':
                            newCode += "Zero";
                            break;
                        case '1':
                            newCode += "One";
                            break;
                        case '2':
                            newCode += "Two";
                            break;
                        case '3':
                            newCode += "Three";
                            break;
                        case '4':
                            newCode += "Four";
                            break;
                        case '5':
                            newCode += "Five";
                            break;
                        case '6':
                            newCode += "Six";
                            break;
                        case '7':
                            newCode += "Seven";
                            break;
                        case '8':
                            newCode += "Eight";
                            break;
                        case '9':
                            newCode += "Nice";
                            break;
                    }
                }

            }

            newCode = newCode.ToUpper();


            var number = GetNumberMaterialCodeMax(newCode);
            int num = 0;
            if (number != null)
            {
                num = Int32.Parse(number) + 1;
            }
            else
            {
                num = 1;
            }

            newCode += num;

            return newCode;
        }


        public string GetNumberMaterialCodeMax(string materialName)
        {
            return _materialRepository.GetNumberMaterialCodeMax(materialName);
        }

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="material">Object bản ghi</param>
        /// <returns>Kết quả</returns>
        /// Created By : TTUyen ( 28/08/2021)
        public ServiceResult Add(Material material)
        {
            //Đánh đấu đang ở trạng thái thêm mới
            material.EntityState = EntityState.AddNew;
            var isValid = ValidateData(material);
            if (isValid == true)
            {
                //Số bản ghi thêm thành công
                var res = _materialRepository.Add(material);
                if (res != 0)
                {
                    _serviceResult.Messager = Properties.ResourcesVN.AddComplete;
                    _serviceResult.ErrorCode = MISACode.IsValid;
                    _serviceResult.Data = res;
                }
                else
                {
                    _serviceResult.Messager = Properties.ResourcesVN.NoAddInDataBase;
                    _serviceResult.Data = res;
                }
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="materialId">Id bản ghi</param>
        /// <returns>ServiceResulf: object kết quả</returns>
        /// Created By : TTUyen ( 28/08/2021)
        public ServiceResult Delete(Guid materialId)
        {
           
            var res = _materialRepository.Delete(materialId);

            if(res > 0)
            {
                _serviceResult.Data = res;
                _serviceResult.ErrorCode = MISACode.IsValid;
            }
            else
            {
                _serviceResult.Data = res;
                _serviceResult.ErrorCode = MISACode.NoValid;
            }
            return _serviceResult;
        }

        /// <summary>
        /// Danh sách bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created By : TTUyen ( 28/08/2021)
        public IEnumerable<Material> Get()
        {
            return _materialRepository.Get();
        }



        /// <summary>
        /// Lọc bản ghi theo id
        /// </summary>
        /// <param name="materialId">id bản ghi</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// Created By : TTUyen ( 28/08/2021)
        public Material GetById(Guid materialId)
        {
            return _materialRepository.GetById(materialId);
        }

        /// <summary>
        /// Cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="material">Object cập nhật</param>
        /// <param name="materialId">id</param>
        /// <returns>Kết quả ServiceResult</returns>
        /// Created By : TTUyen ( 28/08/2021)
        public ServiceResult Update(Material material, Guid materialId)
        {
            material.EntityState = EntityState.Update;
            var isValid = ValidateData(material);
            if (isValid == true)
            {
                // số bản ghi cập nhật thành công
                var rowEffect = _materialRepository.Update(material, materialId);
                if (rowEffect > 0)
                {
                    _serviceResult.Messager = Properties.ResourcesVN.UpdateComplete;
                    _serviceResult.ErrorCode = MISACode.IsValid;
                    _serviceResult.Data = rowEffect;
                }
                else
                {
                    _serviceResult.Messager = Properties.ResourcesVN.NoUpdateInDataBase;
                    _serviceResult.Data = rowEffect;
                }
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }

        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="material"></param>
        /// <param name="formMode">Để kiểm tra xem nó là add hay update</param>
        /// <returns>True/False</returns>
        /// Created By : TTUyen ( 28/08/2021)
        private bool ValidateData(Material material)
        {
            var isValidated = true;

            var mesError = new List<string>();
            //Lấy ra tất cả các property của class
            var properties = typeof(Material).GetProperties();

            var devMsg = new List<String>();

            var userMsg = new List<String>();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(material);

                var propName = property.Name;

                var displayName = property.GetCustomAttributes(typeof(DisplayName), true);

                var name = string.Empty;

                if (displayName.Length > 0)
                    name = (displayName[0] as DisplayName).Name;

                //Kiểm tra các thuộc tính bắt buộc

                //Check bắt buộc nhập
                if (property.IsDefined(typeof(Required), true))
                {
                    if (string.IsNullOrEmpty(propertyValue.ToString()))
                    {

                        devMsg.Add(Properties.ResourcesVN.ErrorDevMsgRequire);

                        userMsg.Add(Properties.ResourcesVN.ErrorUserMsgRequire);

                        mesError.Add(string.Format(Properties.ResourcesVN.ErrorDevMsgRequire, name));

                        _serviceResult.ErrorCode = MISACode.NoValid;

                        isValidated = false;
                    }
                }
                //Check trùng
                if (property.IsDefined(typeof(Duplicated), true))
                {

                    var count = _materialRepository.CheckMaterialCodeExist(propertyValue.ToString());

                    if (count != 0)
                    {

                        if ( material.EntityState == EntityState.AddNew)
                        {
                            var nameValue = material.GetType().GetProperty($"MaterialName").GetValue(material);

                            devMsg.Add(string.Format(Properties.ResourcesVN.ErrorDevMsgDuplicate, propertyValue.ToString()));

                            userMsg.Add(string.Format(Properties.ResourcesVN.ErrorUserMsgDuplicateMaterialCode, propertyValue.ToString(), nameValue));

                            mesError.Add(string.Format(Properties.ResourcesVN.ErrorDevMsgDuplicate, propertyValue.ToString(), nameValue));

                            _serviceResult.ErrorCode = MISACode.NoValid;

                            isValidated = false;
                        }

                    }

                }
                //Check độ dài
                if (property.IsDefined(typeof(MaxLength), false))
                {

                    var attributeMaxLength = property.GetCustomAttributes(typeof(MaxLength), true)[0];

                    var length = (attributeMaxLength as MaxLength).Length;

                    var msg = (attributeMaxLength as MaxLength).ErrorMsg;

                    if (propertyValue != null && propertyValue.ToString().Trim().Length > length)
                    {
                        isValidated = false;

                        mesError.Add(msg);

                        _serviceResult.ErrorCode = MISACode.NoValid;
                    }
                }

            }
            var data = new
            {
                devMsg = devMsg,

                userMsg = userMsg,
            };
            _serviceResult.Data = data;

            _serviceResult.Messager = mesError;

            return isValidated;
        }

        #endregion


    }


}
