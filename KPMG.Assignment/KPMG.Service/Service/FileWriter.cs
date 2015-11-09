using KPMG.Service.CustomException;
using KPMG.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.Service.Service
{
    public class FileWriter : IFileWriter
    {
        private readonly string _connectionString;

        public FileWriter(string connectionString)
        {
            //TODO:read from config file
            _connectionString = connectionString;
        }
        public void WriteChunkData(DataTable table, string distinationTable, IList<KeyValuePair<string, string>> mapList)
        {
            try
            {
                using (var bulkCopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.Default))
                {
                    bulkCopy.BulkCopyTimeout = 0;//unlimited
                    bulkCopy.DestinationTableName = distinationTable;
                    foreach (KeyValuePair<string, string> map in mapList)
                    {
                        bulkCopy.ColumnMappings.Add(map.Key, map.Value);
                    }
                    bulkCopy.WriteToServer(table, DataRowState.Added);
                }

            }
            catch (Exception e)
            {
                throw new FileReadWriteException(e.Message, ExceptionCode.UN_HANDLED);
            }
            
        }
    }
}
