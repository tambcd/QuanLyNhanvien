using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.BLogicLayer.Interface;
using MISA.Common.Entities;
using MISA.Common.Exceptions;
using MISA.DAL.Interface;
using MISA.DAL.Repository;


namespace MISA.BLogicLayer.Services
{
    public class DepartmentBLL:BaseBLL<Department>,IDepartmentBLL
    {

        /// <summary>
        ///  đối tượng DAL gửi resquest 
        /// </summary>

        IDepartmentRepository _DepartmentRepository;
        public DepartmentBLL(IDepartmentRepository DepartmentRepository) : base(DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
        }

        /// <summary>
        /// validate dữ liệu phòng ban 
        /// </summary>
        /// <param name="deparment">đối tượng phòng ban</param>
        /// <returns>
        ///              true : hợp lệ 
        ///              false ; không hợp lệ 
        /// </returns>
        ///  cretedby : TVTam(MF1270) (9/8/2022)
        protected override bool ValidateCusrtom(Department deparment)
        {
            var Ischeck = _DepartmentRepository.checkEntityCode(deparment.DepartmentCode, deparment.DepartmentId);
            if (Ischeck)
            {
                isValidCustom = false;
                listMsgEr.Add(Common.CommonResource.GetResoureString("CodeExist"));
            }

            return isValidCustom;
        }
      

    }
}
