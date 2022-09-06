using MISA.Common.Enum;

namespace MISA.Common.Entities
{
    /// <summary>
    /// Nhân viên 
    /// created : TvTam (3/8/2022)
    /// </summary>
    public class Employee:BaseEntity
    {
        /// <summary>
        /// ID nhân viên 
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên 
        /// </summary>
        [MISARequired]
        [PropNameDisplay("EmployeeCode")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>

        [MISARequired]
        [PropNameDisplay("FullName")]
        public string FullName { get; set; }
        /// <summary>
        /// Ngày sinh nhân viên 
        /// </summary>

        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính nhân viên 
        /// 0 : Nam 
        /// 1: nữ
        /// 2:khác
        /// </summary>
        public int? Gender { get; set; }  
        
        public string? GenderName { get
            {
                switch (this.Gender)
                {
                    case (int?)GenderEnum.Male:
                        return "Nam";
                        break;
                    case (int?)GenderEnum.Female:
                        return "Nữ";
                        break;
                    default: return "khác";
                }
            }
        }
        /// <summary>
        /// Số chứng minh nhân dân 
        /// </summary>
        public string? IdenbtyNumber { get; set; }
        /// <summary>
        /// Ngày cấp CCCD
        /// </summary>

        public DateTime? IdenbtyDate { get; set; }
        /// <summary>
        /// Nới cấp CCCD
        /// </summary>

        public string? Idenbtyplace { get; set; }
        /// <summary>
        /// email nhân viên 
        /// </summary>
        /// 
        [MISAEmail]
        [PropNameDisplay("Email")]
        public string? Email { get; set; }
        
        /// <summary>
        /// số điện thoại 
        /// </summary>

        public string? PhoneNumber { get; set; }
        /// <summary>
        /// số điện thoại di động 
        /// </summary>
        public string? CellPhoneNumber { get; set; }

        /// <summary>
        /// ID vị trí 
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên vị trí  
        /// </summary>

        public string? PositionName { get; set; }

        public string EmployeeTitle { get; set; }
        /// <summary>
        /// Id phòng ban 
        /// </summary>


        [MISARequired]
        [PropNameDisplay("DepartmentId")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng 
        /// </summary>
        public string? BankAccount { get; set; }
        /// <summary>
        /// tên ngân hàng 
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Địa chỉ ngân hàng 
        /// </summary>

        public string? BankPlace { get; set; }

        /// <summary>
        /// địa chỉ nhân viên 
        /// </summary>

        public string? Address { get; set; }

        /// <summary>
        /// lưu trữ lỗi validate
        /// </summary>
       public List<string>? ListerroImport { get; set; } = new List<string>();       

        /// <summary>
        /// Tổng só bản ghi khi paging 
        /// </summary>
        public int? TotalRecord { get; set; }
        
    }
}
