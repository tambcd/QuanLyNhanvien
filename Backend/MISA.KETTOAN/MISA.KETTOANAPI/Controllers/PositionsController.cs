using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Exceptions;
using MISA.Common.Entities;
using MISA.DAL.Repository;
using MISA.BLogicLayer.Services;
using MISA.KETTOANAPI.Controllers;
using MISA.DAL.Interface;
using MISA.BLogicLayer.Interface;

namespace MISA.WebAIP.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]


    public class PositionsController : BaseController<Positions>
    {
        private IPositionsRepository _positionsRepository;
        private IPositionsBLL _positionsBLL;
        public PositionsController(IPositionsRepository positionsRepository, IPositionsBLL positionsBLL) : base(positionsRepository, positionsBLL)
        {
            _positionsRepository = positionsRepository;
            _positionsBLL = positionsBLL;
        }
    }
}
