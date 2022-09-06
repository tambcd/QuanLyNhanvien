using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Exceptions
{
    public class MISAException:Exception
    {
        string MessageError;
        IDictionary errors;
        /// <summary>
        /// thông báo Exception
        /// </summary>
        /// <param name="msg"> string thông báo </param>
        /// <param name="listMsg"> list string thông báo</param>
        public MISAException(string msg = null ,List<string> listMsg = null) {
                    
            MessageError = msg;
            errors = new Dictionary<string, List<string>>();
            errors.Add("ListValidate", listMsg);

        }
        public override string Message=>this.MessageError;
        public override IDictionary Data => this.errors;
        


    }
}
