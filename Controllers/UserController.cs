using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace webapi.Controllers
{
    [Route ("api/User")]
    public class UserController:Controller
    {
        private readonly Context _dbContext;
        public UserController (Context db) {
            _dbContext = db;
        }

        [HttpGet]
        public IActionResult Get (int Page, string QueryStr) {
            var list = this._dbContext.User.OrderByDescending (x => x.Id).Skip (10 * (Page - 1)).Take (10).AsQueryable ();
            if (!string.IsNullOrEmpty (QueryStr)) { list = list.Where (x => x.UserName.Contains (QueryStr)); }
            var result = JsonConvert.SerializeObject (list);
            return Ok (result);
        }

        [HttpPut ("Update")]
        public IActionResult Put (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<User> (jsonStr);
                var user = this._dbContext.User.Find (args.Id);
                if (user == null) {
                    return NotFound ();
                }
                user.UserName = args.UserName;
                user.NickName = args.NickName;
                user.Password = args.Password;
                user.Description = args.Description;
                user.Email = args.Email;
                user.Git = args.Git;
                user.Twitter = args.Twitter;
                user.Weibo = args.Weibo;
                user.QQ = args.QQ;
                user.Sex = args.Sex;
                this._dbContext.Entry (user).CurrentValues.SetValues (user);
                this._dbContext.SaveChanges ();
                return NoContent ();
            } catch (Exception ex) {
                var log = new Logs ();
                log.Expcetion = ex.Message;
                log.CreateDate = DateTime.Now;
                this._dbContext.Logs.Add (log);
                this._dbContext.SaveChanges ();
                return StatusCode (500);
            }
        }

        [HttpPost ("Add")]
        public IActionResult Post (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<User> (jsonStr);
                var user = new User ();
                user.UserName = args.UserName;
                user.NickName = args.NickName;
                user.Password = args.Password;
                user.Description = args.Description;
                user.Email = args.Email;
                user.Git = args.Git;
                user.Twitter = args.Twitter;
                user.Weibo = args.Weibo;
                user.QQ = args.QQ;
                user.Sex = args.Sex;
                user.Role = args.Role;
                user.RegDate = user.LastDate = new DateTime().Date;
                this._dbContext.User.Add (user);
                this._dbContext.SaveChanges ();
                return Ok ();
            } catch (Exception ex) {
                var log = new Logs ();
                log.Expcetion = ex.Message;
                log.CreateDate = DateTime.Now;
                this._dbContext.Logs.Add (log);
                this._dbContext.SaveChanges ();
                return StatusCode (500);
            }
        }

        [HttpDelete ("Del")]
        public IActionResult Delete (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<User> (jsonStr);
                var id = args.Id;
                var user = this._dbContext.User.Find (id);
                if (user == null) {
                    return NotFound ();
                }
                this._dbContext.User.Remove (user);
                this._dbContext.SaveChanges ();
                return NoContent ();
            } catch (Exception ex) {
                var log = new Logs ();
                log.Expcetion = ex.Message;
                log.CreateDate = DateTime.Now;
                this._dbContext.Logs.Add (log);
                this._dbContext.SaveChanges ();
                return StatusCode (500);
            }
        }
    }
}