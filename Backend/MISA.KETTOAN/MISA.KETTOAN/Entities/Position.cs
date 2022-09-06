namespace MISA.KETTOAN.Entities
{
    public class Position
    {
       public Guid PositionId { get; set; }
       public string PositionName { get; set; }

        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }




    }
}
