using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.Service.DTO
{
    public class SkippedLineDTO
    {
        //Skipped Line Number
        public int LineNumber { get; set; }
        //Skipped reason
        public string Message { get; set; }
    }
}
