using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Entities;
using MISA.Common.Exceptions;
using MISA.DAL.Repository;
using MISA.BLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using MISA.KETTOANAPI.Controllers;
using MISA.DAL.Interface;
using MISA.BLogicLayer.Interface;

namespace MISA.WebAIP.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController :BaseController<Department>
    {

        private IDepartmentRepository _departmentRepository;
        private IDepartmentBLL _departmentBL;
        public DepartmentsController(IDepartmentRepository departmentRepository, IDepartmentBLL departmentBL) : base(departmentRepository, departmentBL)
        {
            _departmentRepository = departmentRepository;
            _departmentBL = departmentBL;
        }
      
        
    }
}
