using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Common.Entities;
using Dapper;
using MySqlConnector;
using MISA.DAL.Interface;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Microsoft.Extensions.Configuration;

namespace MISA.DAL.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public  PagingRequest GetFilter(int pageNumber, int pageSize, string? txtSearch)
        {
            if (txtSearch == null)
            {
                txtSearch = "";
            }
            // khai bao sqlCommand
            var sqlcmd = $"Proc_Ft__FilterEmployee";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@pageNumber", pageNumber);
            dynamicParams.Add("@pageSize", pageSize);
            dynamicParams.Add("@txtSearch", txtSearch);

            // thực hiện lấy dữ liệu 

             List<Employee> employees = new List<Employee>();
             employees = connection.Query<Employee>(sql: sqlcmd, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure).ToList();
            PagingRequest pagingRequest;
            if (employees.Count() > 0)
            {
                 pagingRequest = new PagingRequest{ 
                TotalPage= (int)Math.Ceiling(((double)employees[0].TotalRecord / (double)pageSize))
                ,TotalRecord = (int)employees[0].TotalRecord, 
                CurrentPage= pageNumber ,
                CurrentPageRecords = pageSize
                ,Data = employees };
            }
            else
            {
                 pagingRequest = new PagingRequest{ 
                TotalPage= 0
                ,TotalRecord = 0, 
                CurrentPage= 0 ,
                CurrentPageRecords = 0
                ,Data = employees };

            }
          

            return pagingRequest;
        }


        public int ImportEmloyee(List<Employee> employees)
        {
            var rowsEffec = 0;
            using (var transaction = connection.BeginTransaction())
            {
                var sqlcmd = $"Proc_InsertEmployee";
                foreach (var employee in employees)
                {
                    rowsEffec+= connection.Execute(sql: sqlcmd, param: employee, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                }
               transaction.Commit();                      

            }
            return rowsEffec;
        }
       

        public string GetNewCodeEmployee()
        {
            var sqlcmd = $"SELECT SUBSTRING(e.EmployeeCode, 4) FROM Employee e WHERE e.EmployeeCode LIKE 'NV-%' ORDER BY CONVERT(SUBSTRING(e.EmployeeCode, 4) , int)  DESC LIMIT 1 ";            
            var data = connection.QueryFirstOrDefault<string>(sql: sqlcmd);
            return data;
        }
    }
}
