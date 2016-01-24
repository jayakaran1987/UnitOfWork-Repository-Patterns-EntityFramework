using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcess.Service.CustomException
{
    public class FileReadWriteException : Exception
    {
        /// The error code.
        public int code { get; set; }

        public FileReadWriteException(String message, int code)
            : base(message)
        {
            this.code = code;
        }
    }
}
