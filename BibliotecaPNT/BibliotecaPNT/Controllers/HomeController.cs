﻿using Microsoft.AspNetCore.Mvc;

namespace BibliotecaPNT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index","Biblioteca");
        }
    }
}
