﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RRMApi.Models;

namespace RRMApi.Controllers
{
	public class HomeController : Controller
	{

		public HomeController(  )
		{
		}

		public IActionResult Index() => View();

		public IActionResult Privacy() => View();

		[ResponseCache( Duration = 0 , Location = ResponseCacheLocation.None , NoStore = true )]
		public IActionResult Error() => View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
	}
}
