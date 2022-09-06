using Microsoft.AspNetCore.Http;
using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DAL.Interface
{
    public interface IEmployeeRepository: IRepository<Employee>
    {

        /// <summary>
        /// Lọc danh sách đối tượng
        /// </summary>
        /// <param name="pageNumber">số trang</param>
        /// <param name="pageSize">số bảng ghi trên trang </param>
        /// <param name="txtSearch"> từ khóa tìm kiếm </param>
        /// <returns></returns>
        /// created : TVTam(MF1270) - 11/8/2022
        public PagingRequest GetFilter(int pageNumber, int pageSize, string? txtSearch);
        /// <summary>
        /// thực hiện nhập khảu nhân viên từ excel
        /// </summary>
        /// <param name="employees">danh sách nhânh viên </param>
        /// <returns>số bản ghi thêm mới thành công </returns>
        /// created : TVTam(MF1270) - 11/8/2022
        int ImportEmloyee(List<Employee> employees);

      

        /// <summary>
        /// Lấy mã lớn nhất
        /// </summary>
        /// <returns></returns>
        string GetNewCodeEmployee();
    }
}
