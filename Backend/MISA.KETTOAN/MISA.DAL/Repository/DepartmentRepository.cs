using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Common.Entities;
using Dapper;
using MySqlConnector;
using MISA.DAL.Interface;
using Microsoft.Extensions.Configuration;

namespace MISA.DAL.Repository
{
    public class DepartmentRepository : BaseRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Kiểm tra trùng Id phòng ban
        /// </summary>
        /// <param name="departmentId"> Id phòng ban</param>
        /// <returns> true : đã tồn tại  
        ///           false ; nếu chưa tồn tại
        /// </returns>
        public bool checkdepartmentid(Guid departmentId)
        {
            var sqlcmd = "SELECT * FROM Department WHERE DepartmentId = @Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@Id", departmentId);
            var data = connection.QueryFirstOrDefault<Department>(sql: sqlcmd, param: dynamicParams);
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
