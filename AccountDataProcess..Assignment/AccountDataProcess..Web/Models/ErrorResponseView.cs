using AccountDataProcess.Service.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountDataProcess.Web.Models
{
    public class ErrorResponseView
    {
        public String message { get; set; }

        public int? code { get; set; }

        public ErrorResponseView(System.Exception e)
        {
            int? c = null;
            if (e is FileReadWriteException) c = (e as FileReadWriteException).code;

            Initialize(e.Message, c);
        }
        protected void Initialize(String message, int? code)
        {
            this.code = code;
            this.message = message;
        }
    }
}