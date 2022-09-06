using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Entities;
using MISA.Common.Exceptions;
using MISA.DAL.Repository;
using MISA.BLogicLayer.Services;

namespace MISA.WebAIP.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Kiểm tra Exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>statuscode và lỗi </returns>
        private IActionResult HandelException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
                erros = ex.Data["ListValidate"],

            };
            if (ex is MISAException)
            {
                return BadRequest(res);
            }
            return StatusCode(500, res);

        }
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <returns> danh sách phòng ban </returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var departmentRepository = new DepartmentRepository();
                var data = departmentRepository.GetAll();
                return Ok(data);

            } catch (Exception ex)
            {
                return HandelException(ex);
            }
        }
        /// <summary>
        /// Lấy phòng ban theo ID
        /// </summary>
        /// <param name="id"> khóa chính </param>
        /// <returns>1 đối tượng phòng ban </returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var departmentRepository = new DepartmentRepository();
                var data = departmentRepository.GetById(id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        /// <summary>
        /// Thêm phòng ban 
        /// </summary>
        /// <param name="department"> đối tượng phòng ban thêm mới</param>
        /// <returns>201 thành công ||Exception từng trường hợp </returns>
        [HttpPost]
        public IActionResult Post(Department department)
        {
            try
            {
                var departmentServices = new DepartmentBLL();
                var data = departmentServices.InsertSevices(department);
                return StatusCode(201,data);

            }
            catch (Exception ex)
            {
              
                return HandelException(ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {

            try
            {
                var departmentServices = new DepartmentBLL();
                var data = departmentServices.DeleteSevices(id);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return HandelException(ex);
            }
        }
    }
}
