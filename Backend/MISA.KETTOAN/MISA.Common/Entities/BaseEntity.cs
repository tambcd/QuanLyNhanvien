using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo 
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// người tạo 
        /// </summary>
        public string? CreateBy { get; set; }
        /// <summary>
        /// ngày thay đổi 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// người thay đổi
        /// </summary>

        public string? ModifiedBy { get; set; }
    }
}
