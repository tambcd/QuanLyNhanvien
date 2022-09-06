using MISA.Common.Entities;
using MISA.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Common.Exceptions;
using MISA.DAL.Interface;
using MISA.BLogicLayer.Interface;
using System.Text.RegularExpressions;

namespace MISA.BLogicLayer.Services
{
    public class BaseBLL<MISAEntity>:IBaseBLL<MISAEntity> where MISAEntity : class
    {
        /// <summary>
        /// biến check validate custom 
        /// </summary>
        protected bool isValidCustom = true;
        /// <summary>
        /// danh sách lỗi 
        /// </summary>
        protected List<string> listMsgEr = new List<string>();

        /// <summary>
        ///  đối tượng DAL gửi resquest 
        /// </summary>
        IRepository<MISAEntity> repository;
        public BaseBLL(IRepository<MISAEntity> _repository)
        {
            repository = _repository;
        }
          
        public int InsertSevices(MISAEntity entity)
        {
            // check validate chung
            var isValid = Validate(entity);
            // check validate custom
            isValidCustom = ValidateCusrtom(entity);
            if (isValid && isValidCustom)
            {
              var  res = repository.Insert(entity);
              return res;
            }
            else
            {
                throw new MISAException(Common.CommonResource.GetResoureString("InvalidInput"), listMsgEr);
            } 
          
        }      

        public int DeleteSevices(Guid id)
        {
            var listMsgEr = new List<string>();          
           
            // gọi đến DAL gửi resquest 
            var res = repository.Delete(id);
            return res;

        }
       
        public bool Validate(MISAEntity entity){

            var isValid = true;           
            // validate chung 

            // kiểm tra buộc nhập 

            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var value = property.GetValue(entity);
                var arrProNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();


                // nếu dữ liệu trống hoặc bằng null 
                if (property.IsDefined(typeof(MISARequired), false) && (value == null || value.ToString() == String.Empty))
                {
                    isValid = false;
                    propName = (arrProNameDisplay as PropNameDisplay).PropName;
                    listMsgEr.Add($"{propName} {Common.CommonResource.GetResoureString("EmptyCheck")}");
                }
                if(property.IsDefined(typeof(MISAEmail), false))
                {
                    if ((value != null && value.ToString() != String.Empty))
                    {
                        Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        Match matchEmail = regexEmail.Match((string)value);
                        if (!matchEmail.Success)
                        {
                            isValid = false;
                            propName = (arrProNameDisplay as PropNameDisplay).PropName;
                            listMsgEr.Add($"{propName} {Common.CommonResource.GetResoureString("VaildateEmail")}");
                        }
                    }                 

                }               
            }
            return isValid;
        }

        /// <summary>
        /// validate riêng entity 
        /// </summary>
        /// <param name="entity"> đối tượng cần validate</param>
        /// <returns>   true : hợp lệ 
        ///             false ; không hợp lệ 
        /// </returns>       
        ///  cretedby : TVTam(MF1270) (9/8/2022)

        protected virtual bool ValidateCusrtom(MISAEntity entity)
        {

            return true;
        }

        
        public int UpdateSevices(MISAEntity entity)
        {
            // check validate chung
            var isValid = Validate(entity); 
            // check validate custom
            isValidCustom = ValidateCusrtom(entity);
            if (isValid && isValidCustom)
            {
                var res = repository.Update(entity);
                return res;
            }
            else
            {
                throw new MISAException(Common.CommonResource.GetResoureString("InvalidInput"), listMsgEr);
            }
        }

       
    }
}
