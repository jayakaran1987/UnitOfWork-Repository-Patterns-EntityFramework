﻿using KPMG.Service.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.Service.Interface
{
    public interface IFileReader
    {
        IEnumerable<ExtractedDataDTO> GetExcelFileData(string filePath);
        IEnumerable<ExtractedDataDTO> GetCSVFileData(string filePath);
    }
}
