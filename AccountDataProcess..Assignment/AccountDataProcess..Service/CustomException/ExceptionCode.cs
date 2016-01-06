using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.Service.CustomException
{
    public static class ExceptionCode
    {
        // File not exixts in this file path
        public static int FILE_NOT_FOUND = 100;
        // Out of memory
        public static int OUT_OF_MEMORY = 101;
        // Unhandled Exception
        public static int UN_HANDLED = 102;
    }
}
