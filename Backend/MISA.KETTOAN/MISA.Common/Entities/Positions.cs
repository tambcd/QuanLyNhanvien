namespace MISA.Common.Entities
{
    public class Positions : BaseEntity
    {
        /// <summary>
        /// id  vị trí
        /// </summary>
       public Guid PositionsId { get; set; }

        [MISARequired]
        [PropNameDisplay("PositionsName")]
        public string PositionsName { get; set; }     


    }
}
