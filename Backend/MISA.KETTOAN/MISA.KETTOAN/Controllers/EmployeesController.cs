using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using MISA.KETTOAN.Entities;
using System;

namespace MISA.KETTOAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        MySqlConnection connection;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
            //khai bao thong tin ket noi 
            var connectionString = _configuration.GetConnectionString("EmployeeAppCon"); ;
            connection = new MySqlConnection(connectionString);

        }
        // <summary> thông báo Exception  </summary>
        // <return>trả về đối tượng khi gặp lỗi </return>
        // CreateBy : TVTam (29/07/2022)

        private IActionResult HadleException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",


            };

            return StatusCode(500, res);
        }
        /// <summary>
        /// Lấy dánh sách nhân viên
        /// </summary>
        /// <returns> Danh sách nhân viên </returns>
        // CreateBy : TVTam (29/07/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {               

                //thuc thi cong lenh  lay all employee
                var sqlcmd = "SELECT * FROM View_GetAllEmployee";
                var Employees = connection.Query<Employee>(sql: sqlcmd);

                return Ok(Employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        /// <summary>
        /// Lấy nhân viên theo Id
        /// </summary>
        /// <param name="id"> IdEmployee</param>
        /// <returns> nhân viên</returns>
       
        // CreateBy : TVTam (29/07/2022)
        [HttpGet("{id}")]
        public IActionResult getById(Guid id)
        {
            try
            {           

                //thuc thi cong lenh 

                var sqlcmd = $"SELECT * FROM View_GetAllEmployee WHERE EmployeeId = @EmployeeId";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@EmployeeId", id);

                var Employees = connection.QueryFirstOrDefault<Employee>(sql: sqlcmd,param:dynamicParams);

                return Ok(Employees);
            }
            catch (Exception ex)
            {
                return HadleException(ex);

            }
        }

        /// <summary>
        /// Lọc nhân viên theo số trang ,và text
        /// </summary>
        /// <param name="pageSize"> số bản ghi trên trang </param>
        /// <param name="pageNumber"> số trang </param>
        /// <param name="employeeFilter"> từ khóa lọc</param>
        /// <returns> dánh sách nhân viên được lọc</returns>
        
        // CreateBy : TVTam (29/07/2022)

        [HttpGet("filter")]
        public IActionResult getFilter(int pageSize, int pageNumber, string? employeeFilter)
        {
            try
            {
                
                //thuc thi cong lenh 
                /*                
                */
                // tim va phan trang
                var sqlcmd = $"SELECT * FROM View_GetAllEmployee WHERE FullName LIKE CONCAT('%',@employeeFilter,'%') LIMIT @pageNumber,@pageSize";

                var parameters = new DynamicParameters();
                parameters.Add("@employeeFilter", $"{employeeFilter}");
                parameters.Add("@pageNumber", pageNumber);
                parameters.Add("@pageSize", pageSize);


                var Employees = connection.Query<Employee>(sql: sqlcmd,param : parameters);
                // dem tong so ban ghi
                var TotalPage = connection.Query<Employee>("SELECT * FROM Employee").Count();
                double v = TotalPage / pageSize;
                var CurrentPageRecords = Math.Ceiling(v);

                var Filter = new { TotalPage, pageSize, pageNumber, CurrentPageRecords, Employees };
                return Ok(Filter);
            }
            catch (Exception ex)
            {
                return HadleException(ex);

            }
        }

        /// <summary>
        /// Thêm 1 nhân viên
        /// </summary>
        /// <param name="employee">thồn tin nhân viên thêm </param>
        /// <returns>201 thành công </returns>
        /// CreateBy : TVTam (29/07/2022)
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
             
                var sqlcmd = $"Proc_InsertEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode",employee);
               
                var RowAdd = connection.Execute(sql: sqlcmd,param : parameters,commandType : System.Data.CommandType.StoredProcedure);
                // dem tong so ban ghi

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return HadleException(ex);

            }
        }

       
        // CreateBy : TVTam (3/08/2022)
        /// <summary>
        /// Sửa nhân viên 
        /// </summary>
        /// <param name="Id">mã nhân viên sửa</param>
        /// <param name="employee"> nhân viên sửa</param>
        /// <returns>200 thành công</returns>
        [HttpPut("{Id}")]
        public IActionResult put(Guid Id,Employee employee)
        {
            try
            {

                var sqlcmd = $"Proc_UpdateEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employee);/*
                parameters.Add("@FullName", employee.FullName);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);*/

                var RowUpdate = connection.Execute(sql: sqlcmd, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                // dem tong so ban ghi

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return HadleException(ex);

            }
        }

        /// <summary>
        /// xóa 1 nhân viên theo Id
        /// </summary>
        /// <param name="Id"> Id nhân viên xóa</param>
        /// <returns>200 thành công</returns>
      
        // CreateBy : TVTam (3/08/2022)
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {

                var sqlcmd = $"DELETE FROM Employee WHERE EmployeeId = @IdEmployeeCode";
                var parameters = new DynamicParameters();
                parameters.Add("@IdEmployeeCode", Id);

                var RowDelete = connection.Execute(sql: sqlcmd, param: parameters);
                // dem tong so ban ghi

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return HadleException(ex);

            }
        }

        /// <summary>
        ///  Xóa nhiều nhân viên
        /// </summary>
        /// <param name="ListId"> tập hợp Id cần xóa</param>
        /// <returns>200 thành công</returns>

        [HttpDelete("MutilDelete")]
        public IActionResult MutilDelete(Guid[] ListId)
        {
            try
            {
                var rowDeletes = 0;
                // mo ket noi
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var sqlcmd = $"DELETE FROM Employee WHERE EmployeeId in @IdEmployeeCode";
                    var parameters = new DynamicParameters();
                    parameters.Add("@IdEmployeeCode", ListId);                  
                    rowDeletes += connection.Execute(sql: sqlcmd, param: parameters, transaction:transaction);                   

                    // dem tong so ban ghi
                    transaction.Commit();

                }
                connection.Close();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return HadleException(ex);

            }
        }





    }


}
