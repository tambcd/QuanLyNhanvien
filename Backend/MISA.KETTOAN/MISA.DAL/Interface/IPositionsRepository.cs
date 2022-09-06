using Microsoft.AspNetCore.Http;
using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DAL.Interface
{
    public interface IPositionsRepository : IRepository<Positions>
    {
        /// <summary>
        /// Xuất Khẩu data
        /// </summary>
        /// <param name="employees">danh sách nhân viên xuất khẩu </param>
        /// <returns></returns>

    }
}
