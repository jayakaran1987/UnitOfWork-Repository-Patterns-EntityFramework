using BulkDataProcess.Service.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcess.Service.Interface
{
    public interface IFileReader
    {
        IEnumerable<ExtractedDataDTO> GetExcelFileData(string filePath);
        IEnumerable<ExtractedDataDTO> GetCSVFileData(string filePath);
    }
}
