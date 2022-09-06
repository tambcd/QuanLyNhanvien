using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using MISA.KETTOAN.Entities;

namespace MISA.KETTOAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public PositionsController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //khai bao thong tin ket noi 
                var connectionString = _configuration.GetConnectionString("EmployeeAppCon"); ;
                var connection = new MySqlConnection(connectionString);

                //thuc thi cong lenh 
                var sqlcmd = "SELECT * FROM Positions";
                var Positions = connection.Query<Position>(sql: sqlcmd);

                return Ok(Positions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }


    }
}
