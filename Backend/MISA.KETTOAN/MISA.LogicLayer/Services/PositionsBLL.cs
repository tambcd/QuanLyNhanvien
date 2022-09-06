using MISA.BLogicLayer.Interface;
using MISA.Common.Entities;
using MISA.Common.Exceptions;
using MISA.DAL.Interface;
using MISA.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISA.BLogicLayer.Services
{
    public class PositionsBLL : BaseBLL<Positions>,IPositionsBLL
    {
        IPositionsRepository _positionsRepository;


        public PositionsBLL(IPositionsRepository positionsRepository) : base(positionsRepository)
        {
            _positionsRepository =  positionsRepository;
        }

        protected override bool ValidateCusrtom(Positions entity)
        {

            return isValidCustom;
        }
    }
}
