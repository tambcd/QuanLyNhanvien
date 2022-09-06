namespace MISA.KETTOAN.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }    
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public int? Gender { get; set; }   
        public string? IdenbtyNumber { get; set; }

        public DateTime? IdenbtyDate { get; set; }

        public string? Idenbtyplace { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? CellPhoneNumber { get; set; }

        public Guid PositionId { get; set; }

        public string? PositionName { get; set; }


        public Guid DeparmentId { get; set; }
        public string? DeparmentName { get; set; }


        public string? BankAccount { get; set; }
        public string? BankName { get; set; }

        public string? BankPlace { get; set; }

        public string? Address { get; set; }

        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        
    }
}
