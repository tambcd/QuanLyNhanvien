using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DAL.Interface
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Danh sánh đối tương </returns>
        /// createBy TVTam(MF1270) - 11/08/2022
       public IEnumerable<T> GetAll();
        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">khóa chính nhân viên cần lấy </param>
        /// <returns>1 nhân viên</returns>
        /// createBy TVTam(MF1270) - 11/08/2022
        public T GetById(Guid id);       


        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="id">khóa chính nhân viên cần xóa</param>
        /// <returns>200 : thành công </returns>
        /// createBy TVTam(MF1270) - 11/08/2022
        /// 
        public int Delete(Guid id);

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entiy">Bản ghi cần thêm</param>
        /// <returns> 201 nếu đã thêm</returns>
        /// createBy TVTam(MF1270) - 11/08/2022
        public int Insert(T entiy);

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="id">Khóa chính đối tượng cần sửa</param>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <returns>200 nếu chưa update</returns>
        /// createBy TVTam(MF1270) - 11/08/2022
        public int Update(T entity);



        /// <summary>
        /// Kiểu tran mã trùng khi sửa
        /// </summary>
        /// <param name="entityCode">mã đối tượng </param>
        /// <returns>
        ///             true : nếu trùng 
        ///             false ; nếu chưa tồn tại
        /// </returns>
        /// createBy TVTam(MF1270) - 11/08/2022
        bool checkEntityCode(string entityCode, Guid emtityId);


        /// <summary>
        /// Kiểu tran Id tồn tại
        /// </summary>
        /// <param name="entityId">Khóa chính </param>
        /// <returns>
        ///             true : tồn tại trong hệ thống 
        ///             false ; nếu chưa tồn tại
        /// </returns>
        /// createBy TVTam(MF1270) - 11/08/2022
        bool checkEntityId(Guid emtityId);

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="ids">Danh sach khóa chính của các đối tượng cần xóa</param>
        /// <returns></returns>
        /// createBy TVTam(MF1270) - 11/08/2022
        int deleteMany (List<Guid> ids);


      


    }
}
