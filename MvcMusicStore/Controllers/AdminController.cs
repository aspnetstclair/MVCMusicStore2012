﻿using MvcMusicStore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    [InitializeSimpleMembership]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "StoreManager");
        }

    }
}
