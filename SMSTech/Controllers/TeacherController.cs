﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSTech.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult AddEmployee()
        {
            return View();
        }


        public ActionResult EmployeeDetails()
        {
            return View();
        }
	}
}