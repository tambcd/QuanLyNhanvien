using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using MISA.KETTOAN.Entities;


namespace MISA.KETTOAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        MySqlConnection connection;

        public DepartmentsController(IConfiguration configuration)
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
        /// Lấy dữ liệu
        /// </summary>
        /// <returns> danh sách bản ghi</returns>
        /// createBy : TvTam (03/08/2022)

        [HttpGet]
        public IActionResult Get()
        {
            try
            {              

                //thuc thi cong lenh 
                var sqlcmd = "SELECT * FROM Deparment";
                var Deparments = connection.Query<Deparment>(sql: sqlcmd);

                return Ok(Deparments);
            }
            catch (Exception ex)
            {
                return HadleException(ex);

            }

        }
        /// <summary>
        /// Xóa phòng ban 
        /// </summary>
        /// <param name="Id"> mã phòng ban </param>
        /// <returns> 200 thành công</returns>
        /// createBy : TvTam (03/08/2022)
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdDepartment", Id);

                //thuc thi cong lenh 
                var sqlcmd = "DELETE FROM Deparment WHERE DeparmentId = @IdDepartment ";
                var RowDelete = connection.Execute(sql: sqlcmd , param:parameters);

                return Ok(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }


    }
}
