﻿using AccountDataProcess.Data;
using AccountDataProcess.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountDataProcess.Web.Controllers.Core
{
    public class BaseController : Controller
    {
        // GET: Base
       protected IUnitOfWork UnitOfWork { get; set; }

        public BaseController()
        {
            UnitOfWork = UowFactory.Create();
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}