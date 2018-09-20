using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers {
    public class HomeController : Controller {

        [AllowAnonymous]
        public IActionResult Error () {
            return BadRequest ("error");
        }

        public IActionResult Index(){
            return Redirect("https://www.satania.app");
        }
    }
}