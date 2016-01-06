
using AccountDataProcess.Service.CustomException;
using AccountDataProcess.Service.DTO;
using AccountDataProcess.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.Service.Service
{
    public class FileReader : IFileReader
    {
        private readonly int chunkRowLimit;

        public FileReader()
        {
            chunkRowLimit = 3;
        }

        // ------------ Public Methods -------------------------------


        //----------- Extract and read Excel xlsx file ------------------//
        public IEnumerable<ExtractedDataDTO> GetExcelFileData(string filePath)
        {
            int chunkRowCount = 0;
            bool firstLineOfChunk = true;

            int lineCount = 0;
            IList<SkippedLineDTO> skippedLines = new List<SkippedLineDTO>();

            DataTable chunkDataTable = null;;

            // Check File Exist in specific Path
            if (File.Exists(filePath))
            {
                //Specify the excel provider for .xlsx file type and the file path which contain the excel file
                string con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + filePath + ";Extended Properties=Excel 12.0 Xml";

                using (OleDbConnection connection = new OleDbConnection(con))
                {
                    //Open the Oledb connection
                    connection.Open();
                    //Specify the command, assume that the sheet name is Sheet1
                    OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (firstLineOfChunk)
                            {
                                firstLineOfChunk = false;
                                chunkDataTable = CreateEmptyDataTable();
                            }

                            lineCount++;
                            AddRow(chunkDataTable, SplitToArray(reader), lineCount, skippedLines);
                            chunkRowCount++;

                            if (chunkRowCount == chunkRowLimit)
                            {
                                firstLineOfChunk = true;
                                chunkRowCount = 0;
                                yield return new ExtractedDataDTO() { ChunkDataTable = chunkDataTable, SkippedLines = skippedLines, ToatalLineRecordsProcessed = lineCount };
                                chunkDataTable = null;
                            }

                        }
                        reader.Close();
                    }
                }
                //return last set of data which less then chunk size
                if (null != chunkDataTable)
                    yield return new ExtractedDataDTO() { ChunkDataTable = chunkDataTable, SkippedLines = skippedLines, ToatalLineRecordsProcessed = lineCount };
            }
            else
            {
                throw new FileReadWriteException("File NOT Exisits", ExceptionCode.FILE_NOT_FOUND);
            }

        }

        //----------- Extract and read Excel csv file ------------------//
        public IEnumerable<ExtractedDataDTO> GetCSVFileData(string filePath)
        {
            int chunkRowCount = 0;
            bool firstLineOfChunk = true;

            int lineCount = 0;
            IList<SkippedLineDTO> skippedLines = new List<SkippedLineDTO>();

            DataTable chunkDataTable = null;

            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath))
                {
                    string line = null;
                    //Read and display lines from the file until the end of the file is reached.                
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (firstLineOfChunk)
                        {
                            firstLineOfChunk = false;
                            chunkDataTable = CreateEmptyDataTable();
                        }

                        lineCount++;
                        AddRow(chunkDataTable, line.Split(new[] { ',' }), lineCount, skippedLines);
                        chunkRowCount++;

                        if (chunkRowCount == chunkRowLimit)
                        {
                            firstLineOfChunk = true;
                            chunkRowCount = 0;
                            yield return new ExtractedDataDTO() { ChunkDataTable = chunkDataTable, SkippedLines = skippedLines, ToatalLineRecordsProcessed = lineCount };
                            chunkDataTable = null;
                        }
                    }
                }
                //return last set of data which less then chunk size
                if (null != chunkDataTable)
                    yield return new ExtractedDataDTO() { ChunkDataTable = chunkDataTable, SkippedLines = skippedLines, ToatalLineRecordsProcessed = lineCount };
            }
            else
            {
                throw new FileReadWriteException("File NOT Exisits", ExceptionCode.FILE_NOT_FOUND);
            }  
        }


        //-----------------------------Private Methods --------------------------//
        private DataTable CreateEmptyDataTable()
        {
            var dataTable = new DataTable("tblData");

            dataTable.Columns.Add("Id", typeof(Guid));
            dataTable.Columns.Add("Account", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("CurrencyCode", typeof(string));
            dataTable.Columns.Add("Amount", typeof(decimal));

            return dataTable;
        }

        private void AddRow(DataTable dataTable, string[] dataArray, int lineNumber, IList<SkippedLineDTO> skippedLines)
        {
            DataRow newRow = dataTable.NewRow();
            var validatedFlag = true;
            // Do Validations 
            // check spilited lines == dataTable.Columns.count - 1 ---ID

            try
            {
                if (dataArray.Count() == dataTable.Columns.Count - 1)
                {
                    for (int dataArrayIndex = 0; dataArrayIndex < dataTable.Columns.Count - 1; dataArrayIndex++)
                    {
                        if (dataArrayIndex == 0 || dataArrayIndex == 1)
                        {
                            //string in not null
                            if (string.IsNullOrWhiteSpace(dataArray[dataArrayIndex]))
                            {
                                //Skip because account name cannot be null
                                skippedLines.Add(new SkippedLineDTO { LineNumber = lineNumber, Message = "Account Name/ Description cannot be Null or Empty" });
                                validatedFlag = false;
                                break;
                            }
                            else
                            {
                                newRow[dataArrayIndex + 1] = dataArray[dataArrayIndex];
                            }
                        }
                        else if (dataArrayIndex == 2)
                        {
                            if (!ISO4217CurrencyValidator(dataArray[dataArrayIndex]))
                            {
                                //Skip because account name cannot be null
                                skippedLines.Add(new SkippedLineDTO { LineNumber = lineNumber, Message = "Curency is not valid ISO 4217" });
                                validatedFlag = false;
                                break;
                            }
                            else
                            {
                                newRow[dataArrayIndex + 1] = dataArray[dataArrayIndex];
                            }
                        }
                        else if (dataArrayIndex == 3)
                        {
                            int value;
                            if (int.TryParse(dataArray[dataArrayIndex], out value))
                            {
                                newRow[dataArrayIndex + 1] = dataArray[dataArrayIndex];
                            }
                            else
                            {
                                //Skip because account name cannot be null
                                skippedLines.Add(new SkippedLineDTO { LineNumber = lineNumber, Message = "Curency is not valid number" });
                                validatedFlag = false;
                                break;
                            }
                        }
                    }

                    if (validatedFlag)
                    {
                        newRow[0] = Guid.NewGuid();
                        dataTable.Rows.Add(newRow);
                    }

                }
                else
                {
                    skippedLines.Add(new SkippedLineDTO { LineNumber = lineNumber, Message = "Line contains more or less than 4 columns" });
                }

            }
            catch (Exception e)
            {
                throw new FileReadWriteException( e.Message, ExceptionCode.UN_HANDLED);
            } 
        }

        private string[] SplitToArray(OleDbDataReader reader)
        {
            //our csv file will be tab delimited
            var returnArray = new string[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                returnArray[i] = reader[i].ToString();
            }
            return returnArray;
        }


        private static bool ISO4217CurrencyValidator(string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                System.Globalization.RegionInfo regionInfo = (from culture in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.InstalledWin32Cultures)
                                                              where culture.Name.Length > 0 && !culture.IsNeutralCulture
                                                              let region = new System.Globalization.RegionInfo(culture.LCID)
                                                              where region.ISOCurrencySymbol == code
                                                              select region).FirstOrDefault();

                return regionInfo != null ? true : false;
            }
            else return false;
        }
   
    }
}
