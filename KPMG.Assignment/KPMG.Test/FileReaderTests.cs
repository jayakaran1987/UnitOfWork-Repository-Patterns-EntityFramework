using System;
using KPMG.Service.Interface;
using KPMG.Service.Service;
using KPMG.Service.CustomException;
using NUnit.Framework;
using KPMG.Service.DTO;
using KPMG.Web.Models;
using System.IO;

namespace KPMG.Test
{
    [TestFixture]
    public class FileReaderTests
    {
        // Initialize file reader
        private IFileReader fileReader;

        [SetUp]
        public void SetUp()
        {
            this.fileReader = new FileReader();
        }

        // Incompleted Unit Test Coverage
        // Due to the limited time I did not cover all unit test coverage.
        // I have only created FileReaderTests to show that I have knowledge about Unit Testing and TDD.
        // 

        [Test]
        public void FileNotExistThrowsFileReadWriteExceptionInCSV()
        {
            
            try
            {
                var obj = this.fileReader.GetCSVFileData(null);
                foreach (ExtractedDataDTO dto in fileReader.GetExcelFileData(null))
                {

                }
            }
            catch (Exception e)
            {
                Assert.That(new ErrorResponseView(e).code, Is.EqualTo(100));
            }
        }

        [Test]
        public void ReadCSVFileForGivenPathReturnCorrectData()
        {
            string filepath = Path.GetFullPath("SampleCSV_1.csv");

            foreach (ExtractedDataDTO dto in fileReader.GetCSVFileData(filepath))
            {
                Assert.That(dto.ToatalLineRecordsProcessed, Is.EqualTo(1));
            }
        }

        [Test]
        public void FileNotExistThrowsFileReadWriteExceptionInExcel()
        {

            try
            {
                var obj = this.fileReader.GetExcelFileData(null);
                foreach (ExtractedDataDTO dto in fileReader.GetExcelFileData(null))
                {

                }
            }
            catch (Exception e)
            {
                Assert.That(new ErrorResponseView(e).code, Is.EqualTo(100));
            }
        }

        [Test]
        public void ReadExcelFileForGivenPathReturnCorrectData()
        {
            string filepath = Path.GetFullPath("SampleExcel_1.xlsx");

            foreach (ExtractedDataDTO dto in fileReader.GetExcelFileData(filepath))
            {
                Assert.That(dto.ToatalLineRecordsProcessed, Is.EqualTo(1));
            }
        }
    }
}