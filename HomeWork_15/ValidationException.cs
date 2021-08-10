using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_15
{
    public class ValidationException : Exception
    {
        public int ErrorCode { get; set; }
        public ValidationException(string Msg, int Code) : base(Msg)
        {
            ErrorCode = Code;
        }
    }
}
