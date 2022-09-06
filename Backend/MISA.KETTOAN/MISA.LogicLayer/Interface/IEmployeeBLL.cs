using Microsoft.AspNetCore.Http;
using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLogicLayer.Interface
{
    public interface IEmployeeBLL:IBaseBLL<Employee>
    {
        /// <summary>
        /// đọc dữ liệu từ tệp excel
        /// </summary>
        /// <param name="formFile">Tệp excel </param>
        /// <returns>số bản ghi thêm mới</returns>

        List<Employee> ImportEmployeeService(IFormFile formFile);
        /// <summary>
        /// Xuất khẩu tất cả nhân viên ra excel
        /// </summary>
        /// <returns>URL tải excle xuống </returns>
        public Stream ExportEmployees();

        /// <summary>
        /// Sinh mã mới cho đối tượng
        /// </summary>
        /// <returns></returns>
        public String GetNewCodeSevice();

    }
}
