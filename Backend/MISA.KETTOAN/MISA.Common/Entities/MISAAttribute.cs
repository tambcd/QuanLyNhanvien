using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entities
{
    /// <summary>
    /// thuộc tính buộc nhập 
    /// </summary>
    /// cretedby : TVTam(MF1270) (9/8/2022)
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
    public class MISARequired : Attribute
    {

    }
    /// <summary>
    /// thuộc tính kiểm tra Email 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MISAEmail : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MISADate : Attribute
    {
    } 




    /// <summary>
    /// thuộc tính lấy ra tên trường 
    /// </summary>
    /// cretedby : TVTam(MF1270) (9/8/2022)
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PropNameDisplay : Attribute
    {
        public string PropName { get; set; }
        public PropNameDisplay(string name)
        {
            this.PropName = Common.CommonResource.GetResoureString(name);
        }

    }
}
