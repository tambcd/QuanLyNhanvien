using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.Common.Entities;
using MISA.DAL.Interface;
using Microsoft.Extensions.Configuration;

namespace MISA.DAL.Repository
{
    public class PositionRepository : BaseRepository<Positions>, IPositionsRepository
    {
        public PositionRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
