using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcess.Service.DTO
{
    public class ExtractedDataDTO
    {
        public DataTable ChunkDataTable { get; set; }
        // Skipped Line Infomation 
        public IList<SkippedLineDTO> SkippedLines { get; set; }
        // Total record
        public int ToatalLineRecordsProcessed { get; set; }

    }
}
