using AccountDataProcess.Model.Entity;
using AccountDataProcess.Service.DTO;
using AccountDataProcess.Service.Interface;
using AccountDataProcess.Service.Service;
using AccountDataProcess.Web.Controllers.Core;
using AccountDataProcess.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountDataProcess.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            // UnitOfWork call for code first approach to create database in server
            var list = UnitOfWork.AccountDatas.GetAll().ToList();
            return View();
        }

        //-----------------DATA UPLOAD ------------------//
        
        [HttpPost]
        public ActionResult UploadAccountData(IEnumerable<HttpPostedFileBase> files)
        {
            // Only One file should be passed to server side--- validated in client upload
            // File upload control has validated to take single file and file types are .csv and .xlsx

            var returnResult = new UploadResult();

            try 
            {
                // The Name of the Upload component is "files"
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var extension = Path.GetExtension(file.FileName);

                        if (extension == ".xlsx")
                        {
                            // Save files on appropriate path
                            var directory = Server.MapPath("~/Files/Uploaded/Excel/");
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                            var physicalPath = Path.Combine(directory, file.FileName);
                            file.SaveAs(physicalPath);

                            //Read and Save using SQL BULK Copy 
                            returnResult = ReadAndSaveAccountData(physicalPath, extension);
                        }
                        else if (extension == ".csv")
                        {
                            // Save files on appropriate path
                            var directory = Server.MapPath("~/Files/Uploaded/CSV/");
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                            var physicalPath = Path.Combine(directory, file.FileName);
                            file.SaveAs(physicalPath);

                            //Read and Save using SQL BULK Copy 
                            returnResult = ReadAndSaveAccountData(physicalPath, extension);
                        }
                    }
                }

                // Return an empty string to signify success
                return Content(JsonConvert.SerializeObject(returnResult), "application/json");
            }
            catch (Exception e)
            {
                return Content(JsonConvert.SerializeObject(new ErrorResponseView(e)), "application/json");
            }
        }



        //----------- DATA MANAGEMENT---------------//

        public virtual JsonResult GetAccountData()
        {
            var returnList = UnitOfWork.AccountDatas.GetAll();
            return Json(returnList);
        }

        public virtual JsonResult UpdateAccountData(List<AccountData> model)
        {
            foreach (var accountData in model)
            {
                UnitOfWork.AccountDatas.Update(accountData);
                UnitOfWork.Commit();
            }
            var returnList = UnitOfWork.AccountDatas.GetAll();
            return Json(returnList);
        }

        public virtual JsonResult DeleteAccountData(List<AccountData> model)
        {
            foreach (var accountData in model)
            {
                UnitOfWork.AccountDatas.Delete(accountData);
                UnitOfWork.Commit();
            }

            var returnList = UnitOfWork.AccountDatas.GetAll();
            return Json(returnList);
        }

        //--------------------------------------------//




        //------------------Private methods ------------------//


        private UploadResult ReadAndSaveAccountData(string physicalPath, string extension)
        {
            // Initialize reader and writer
            IFileReader fileReader = new FileReader();
            IFileWriter filewriter = new FileWriter(ConfigurationManager.ConnectionStrings["AccountDataProcessDbConnection"].ConnectionString);

            var returnResult = new UploadResult();

            if (extension == ".xlsx")
            {
                foreach (ExtractedDataDTO dto in fileReader.GetExcelFileData(physicalPath))
                {
                    //fileWriter.WriteChunkData(tbl, destinationTableName, Map());
                    filewriter.WriteChunkData(dto.ChunkDataTable, "AccountDatas", Map());
                    returnResult.SkippedLines = dto.SkippedLines;
                    returnResult.ToatalLineRecordsProcessed = dto.ToatalLineRecordsProcessed;
                }
            }
            else if (extension == ".csv")
            {
                foreach (ExtractedDataDTO dto in fileReader.GetCSVFileData(physicalPath))
                {
                    //fileWriter.WriteChunkData(tbl, destinationTableName, Map());
                    filewriter.WriteChunkData(dto.ChunkDataTable, "AccountDatas", Map());
                    returnResult.SkippedLines = dto.SkippedLines;
                    returnResult.ToatalLineRecordsProcessed = dto.ToatalLineRecordsProcessed;
                }
            }

            return returnResult;   
        }

        [DebuggerStepThrough]
        private static IList<KeyValuePair<string, string>> Map()
        {
            return new List<KeyValuePair<string, string>> 
            { 
                {new KeyValuePair<string, string>("Id", "Id")},
                {new KeyValuePair<string, string>("Account", "Account")},
                {new KeyValuePair<string, string>("Description", "Description")},
                {new KeyValuePair<string, string>("CurrencyCode", "CurrencyCode")},
                {new KeyValuePair<string, string>("Amount", "Amount")},
            };
        } 
    }
}