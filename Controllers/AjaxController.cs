using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapi.Helper;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace webapi.Controllers {
    [Route ("api/Ajax")]
    public class AjaxController : Controller {
        private readonly Context _dbContext;
        public AjaxController (Context db) {
            _dbContext = db;
        }

        [HttpPost ("Login")]
        public IActionResult Login (string username, string password) {
            if (string.IsNullOrEmpty (username) || string.IsNullOrEmpty (password)) {
                return BadRequest ("用户名或密码不能为空");
            }

            var user = this._dbContext.User.FirstOrDefault (x => x.UserName == username);
            if (user == null) {
                return NotFound ("用户不存在");
            }

            var claims = new [] {
                new Claim ("Id", user.Id.ToString ())
            };
            var claimsIdentity = new ClaimsIdentity (claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimUser = new ClaimsPrincipal (claimsIdentity);
            HttpContext.SignInAsync (
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimUser, new AuthenticationProperties () { IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.AddMinutes (60) }).Wait ();

            return Ok ();
        }

        [HttpPost ("SingOut")]
        public IActionResult SingOut () {
            HttpContext.SignOutAsync().Wait();
            return Ok ();
        }
    }
}