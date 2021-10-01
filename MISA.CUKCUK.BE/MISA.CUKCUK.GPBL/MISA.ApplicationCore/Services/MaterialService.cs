using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace MISA.ApplicationCore.Services
{
    public class MaterialService : BaseService<Material>, IMaterialService
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
        public MaterialService(IMaterialRepository materialRepository) : base(materialRepository)
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
            if(number != null)
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
        #endregion


    }


}
