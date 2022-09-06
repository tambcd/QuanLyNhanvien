namespace MISA.Common.Entities
{
    public class Department :BaseEntity
    {

        /// <summary>
        /// id  phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }   

        /// <summary>
        /// mã phòng ban 
        /// </summary>
        [MISARequired]
        [PropNameDisplay("DepartmentCode")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// tên phòng ban 
        /// </summary>
        [MISARequired]
        [PropNameDisplay("DepartmentName")]
        public string DepartmentName { get; set; }
        
    }
}
