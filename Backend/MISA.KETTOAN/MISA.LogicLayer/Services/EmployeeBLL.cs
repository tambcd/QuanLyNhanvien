using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MISA.BLogicLayer.Interface;
using MISA.Common.Entities;
using MISA.DAL.Interface;
using MISA.DAL.Repository;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MISA.BLogicLayer.Services
{

    public class EmployeeBLL: BaseBLL<Employee>,IEmployeeBLL
    {
        IEmployeeRepository _EmployeeRepository;
        public EmployeeBLL(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _EmployeeRepository = employeeRepository;   
        }

        public List<Employee> ImportEmployeeService(IFormFile formFile)
        {
            // kiểu tra tệp đúng dịnh dạng ko 
            if (formFile == null || formFile.Length <= 0)
            {
                listMsgEr.Add(Common.CommonResource.GetResoureString("FileExist"));
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                listMsgEr.Add(Common.CommonResource.GetResoureString("FileNotFormat"));
            }

            var employees = new List<Employee>();

            using (var stream = new MemoryStream())
            {
                 formFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 4; row <= rowCount; row++)
                    {

                        var emp = new Employee()
                        {
                            
                            EmployeeCode =ConvertObjectToString(worksheet.Cells[row, 2].Value),
                            FullName = ConvertObjectToString(worksheet.Cells[row, 3].Value),
                            Gender = ConvertObjectToGender(worksheet.Cells[row, 4].Value),
                            DateOfBirth = ConvertObjectToDate(worksheet.Cells[row, 5].Value),

                        };
                          base.listMsgEr.Clear();
                         base.Validate(emp);
                        ValidateCusrtom(emp);
                        emp.ListerroImport.AddRange(base.listMsgEr);

                        employees.Add(emp);
                    }
                }
            }
            return employees;
        }

        /// <summary>
        /// validate insert cho nhân vien 
        /// </summary>
        /// <param name="entity"> nhân viên</param>
        /// <returns>   
        ///   true : hợp lệ 
        ///   false ; không hợp lệ 
        /// </returns>
        protected override bool ValidateCusrtom(Employee entity)
        {
            // Kiểm tra mã nhân viên đã tồn tại chưa 
            // trường hợp thêm mới 

            var Ischeck = _EmployeeRepository.checkEntityCode(entity.EmployeeCode, entity.EmployeeId);
            if (Ischeck)
            {
                isValidCustom = false;
                listMsgEr.Add(Common.CommonResource.GetResoureString("CodeExist"));
            }
            return isValidCustom;
        }
        /// <summary>
        /// chuển object qua int Gender 
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>int || null</returns>
        /// ceatedBy TVTam(MF1270) _ 11/8/2022
        private int? ConvertObjectToGender(object? value)
        {
            if (value == null)
            {
                return 2;
            }
            else
            {
                if (value.ToString().Trim().Equals("Nam")) return 0;
                else if (value.ToString().Trim().Equals("Nữ")) return 1;
                else return 2;
            }
        }
        /// <summary>
        /// chuyển object sang Int
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>int || null </returns>
        /// ceatedBy TVTam(MF1270) _ 11/8/2022
        private int? ConvertObjectToNumber(object? value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                int Number;
                if (Int32.TryParse(value.ToString(), out Number))
                {
                    return Number;
                }
                else return null;
            }
        }

        /// <summary>
        /// Chuyển Object sang date 
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>date || null</returns>
        /// ceatedBy TVTam(MF1270) _ 11/8/2022
        private DateTime? ConvertObjectToDate(object? value)
        {
            if(value == null)
            {
                return null;
            }
            else
            {
                DateTime date ;
                if(DateTime.TryParse(value.ToString(),out date))
                {
                    return date;
                }
                return null;
            }
        }

        /// <summary>
        /// Chuyển Object sang string 
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>1 chuỗi || null </returns>
        /// ceatedBy TVTam(MF1270) _ 11/8/2022
        private string? ConvertObjectToString(Object? value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return value.ToString();
            }

        }

        public Stream ExportEmployees()
        {
            return GenerateExcelFileAsync((List<Employee>)_EmployeeRepository.GetAll());
        }

        public Stream GenerateExcelFileAsync(List<Employee> employees)
        {
            string excelName = $"DanhSachNhanVien-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";     
            
            MemoryStream stream = new MemoryStream();
            var headers = new List<string>()
            {
                "STT",
                "Mã nhân viên",
                "Tên nhân viên",
                "Giới tính",
                "Ngày sinh",
                "Vị trí /Chưc vụ",
                "Email",
                "Số điện thoại",
                "Mã căn cước công dân",
                "Nơi cấp căn cước công dân",
                "Ngày cấp căn cước công dân",
                "Địa chỉ",
                "Tên đơn vị",
                "Số tài khoản ngân hàng",
                "Tên ngân hàng",
                "Chi nhánh ngân hàng"
            };
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("My Sheet");
                for (int i = 1; i <= headers.Count; i++)
                {
                    sheet.Column(i).AutoFit();
                    sheet.Cells[3, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[3, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    sheet.Cells[3, i].Style.Fill.BackgroundColor.SetColor(color: System.Drawing.Color.FromArgb(30988504));
                    sheet.Cells[3, i].Value = headers[i - 1];
                }

                int row = 4;
                int index = 1;
                foreach (var i in employees)
                {
                    sheet.Cells[row, 1].Value = index;
                    sheet.Cells[row, 2].Value = i.EmployeeCode;
                    sheet.Cells[row, 3].Value = i.FullName;
                    sheet.Cells[row, 4].Value = i.GenderName ?? "";
                    sheet.Cells[row, 5].Value = DateToString(i.DateOfBirth);
                    sheet.Cells[row, 6].Value = i.PositionId != null ? i.PositionName : "";
                    sheet.Cells[row, 7].Value = i.Email ?? "";
                    sheet.Cells[row, 8].Value = i.PhoneNumber ?? "";
                    sheet.Cells[row, 9].Value = i.IdenbtyNumber ?? "";
                    sheet.Cells[row, 10].Value = i.Idenbtyplace ?? "";
                    sheet.Cells[row, 11].Value = DateToString(i.IdenbtyDate);
                    sheet.Cells[row, 12].Value = i.Address ?? "";
                    sheet.Cells[row, 13].Value = i.DepartmentId != null ? i.DepartmentName : "";
                    sheet.Cells[row, 14].Value = i.BankAccount ?? "";
                    sheet.Cells[row, 15].Value = i.BankName ?? "";
                    sheet.Cells[row, 16].Value = i.BankPlace ?? "";
                    index++;
                    row++;
                }

                row = 4;
                // Style
                foreach (var i in employees)
                {
                    sheet.Cells[row, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 16].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

                    row++;
                }
                for (var i = 1; i <= headers.Count; i++)
                {
                    sheet.Column(i).AutoFit();
                }
                package.Save();
            }


            return stream;
        }
        /// <summary>
        /// Chuyển date sang string
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>dd/mm/dd</returns>
        private string DateToString(DateTime? date)
        {
            if(date == null)
            {
                return "";
            }
            else
            {
                return "" + date.Value.Day.ToString()+"/" + date.Value.Month.ToString()+"/"+date.Value.Year.ToString();
            }
        }

        public string GetNewCodeSevice()
        {
            var newcode = _EmployeeRepository.GetNewCodeEmployee();
            if (newcode != null)
            {


               string resultString = Regex.Match(newcode, @"\d+").Value;
                
                return "NV-" + (Int32.Parse(resultString) + 1).ToString();

                
            }
            return "NV-1";
        }
    }
}
