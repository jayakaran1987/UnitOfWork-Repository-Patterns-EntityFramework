using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.Service.Interface
{
    public interface IFileWriter
    {
       void WriteChunkData(DataTable table, string distinationTable, IList<KeyValuePair<string, string>> mapList);

    }
}
