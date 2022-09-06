using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Exceptions;
using MISA.DAL.Repository;
using MISA.BLogicLayer.Services;
using MISA.Common.Entities;
using MISA.KETTOANAPI.Controllers;
using MISA.DAL.Interface;
using MISA.BLogicLayer.Interface;
using OfficeOpenXml;
using System.Text;

namespace MISA.WebAIP.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeBLL _employeeBLL;
        public EmployeesController(IEmployeeRepository employeeRepository,IEmployeeBLL employeeBLL) : base(employeeRepository, employeeBLL)
        {
            _employeeRepository = employeeRepository;
            _employeeBLL = employeeBLL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber">Số trang </param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <param name="txtSearch">từ khóa tìm kiếm</param>
        /// <returns></returns>
        /// createdby : TVTam(MF1270) 11/08/2022
        [HttpGet("Filter")]
        public IActionResult GetFilter(int pageNumber, int pageSize, string? txtSearch)
        {
            try
            {
                var data = _employeeRepository.GetFilter(pageNumber, pageSize, txtSearch);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        /// <summary>
        /// Get mã nhân viên auto 
        /// </summary>
        /// <returns></returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var data = _employeeBLL.GetNewCodeSevice();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }


        [HttpGet("Export")]
        public  IActionResult Export()
        {
            try
            {
                var stream = _employeeBLL.ExportEmployees();
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        [HttpPost("Import")]
        public IActionResult Import(IFormFile formFile)
        {
            try
            {
                var path = _employeeBLL.ImportEmployeeService(formFile);
                return Ok(path);
            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }

        }

        
    }
}
