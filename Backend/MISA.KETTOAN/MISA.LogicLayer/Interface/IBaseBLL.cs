using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLogicLayer.Interface
{
    public interface IBaseBLL<T>
    {
        /// <summary>
        /// Thêm mới đối tương
        /// </summary>
        /// <param name="entity">đối tượng thêm mới</param>
        /// <returns>201 thành công</returns>
        /// CreatedBy : TVTam(11/08/2022)
        public int InsertSevices(T entity);


        /// <summary>
        /// Sửa đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần sửa</param>
        /// <returns>200 Thành công</returns>
        /// CreatedBy : TVTam(11/08/2022)
        public int UpdateSevices(T entity);

        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id">Khóa chính dôid tượng cần xóa</param>
        /// <returns>200 thành công</returns>
        /// CreatedBy : TVTam(11/08/2022)
        public int DeleteSevices(Guid id);

        /// <summary>
        /// Validate đối tượng 
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <returns>
        /// true : thỏa mán validate
        /// Fasle : không thỏa mãn validate     
        /// </returns>
        /// CreatedBy : TVTam(11/08/2022)
        public bool Validate(T entity);

       
    }
}
