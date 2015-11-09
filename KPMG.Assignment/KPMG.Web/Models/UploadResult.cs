using KPMG.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPMG.Web.Models
{
    public class UploadResult
    {
        public int ToatalLineRecordsProcessed { get; set; }
        public IList<SkippedLineDTO> SkippedLines { get; set; }
    }
}