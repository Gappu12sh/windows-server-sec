using Customer.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class ResponseResults
    {
        public ResponseResults()
        {
        }

        public ResponseResults(ErrorCodes? errorCodes)
        {
            ErrorCodes = errorCodes;
        }

        public ErrorCodes? ErrorCodes { get; set; }
        public string ErrorDescription { get; set; }

        public bool HasError()
        {
            return ErrorCodes.HasValue;
        }
    }

    public class ResponseResults<T> : ResponseResults
    {
        public T ResponseBody { get; set; }
        public ResponseResults(T body)
        {
            ResponseBody = body;
        }

        public ResponseResults(ErrorCodes errorCode):base(errorCode) { }
    }
}
